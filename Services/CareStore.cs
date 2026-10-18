using System.Text.Json;
using Microsoft.JSInterop;

namespace CareHeath.Services;

public sealed class CareState
{
    public string ReminderTime { get; set; } = "20:00";
    public string? LastCheckInDay { get; set; }
    public string? AlertDay { get; set; }
    public List<CareEvent> Events { get; set; } = [];
}
public sealed record CareEvent(DateTimeOffset At, string Message);

// Replace this adapter with authenticated cloud storage when AWS is approved.
public interface ICareStore
{
    Task<CareState> LoadAsync();
    Task SaveAsync(CareState state);
}
public sealed class BrowserCareStore(IJSRuntime js) : ICareStore
{
    public async Task<CareState> LoadAsync()
    {
        var json = await js.InvokeAsync<string?>("careLink.load");
        if (json is null) return new();
        var state = JsonSerializer.Deserialize<CareState>(json) ?? throw new InvalidOperationException("Dữ liệu lưu không hợp lệ.");
        if (!TimeOnly.TryParseExact(state.ReminderTime, "HH:mm", out _) || state.Events is null)
            throw new InvalidOperationException("Dữ liệu lưu không hợp lệ.");
        return state;
    }
    public Task SaveAsync(CareState state) => js.InvokeVoidAsync("careLink.save", JsonSerializer.Serialize(state)).AsTask();
}
public static class CheckInRules
{
    public static DateTimeOffset VietnamNow => DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(7));
    public static bool IsDue(CareState state, DateTimeOffset now)
    {
        var local = now.ToOffset(TimeSpan.FromHours(7));
        return state.LastCheckInDay != local.ToString("yyyy-MM-dd")
            && TimeOnly.FromDateTime(local.DateTime) >= TimeOnly.ParseExact(state.ReminderTime, "HH:mm");
    }
}
