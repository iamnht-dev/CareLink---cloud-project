# CareLink

CareLink hỗ trợ người sống một mình tự xác nhận tình trạng mỗi ngày. Định hướng dài hạn là một thiết bị đeo kết nối ứng dụng và cloud; phiên bản hiện tại là **web mô phỏng chạy cục bộ** bằng C# / Blazor.

## Phiên bản hiện tại

- Dashboard nhịp tim **giả lập** được cập nhật khoảng 3 giây một lần khi kết nối máy chủ.
- Check-in hằng ngày bằng nút **Tôi vẫn ổn**.
- Cài giờ nhắc theo múi giờ Việt Nam (UTC+7).
- Hiện báo thức khi đến giờ mà chưa check-in; có bật âm thanh và tắt âm thanh.
- Lưu giờ nhắc, ngày check-in và tối đa 100 sự kiện vào localStorage của trình duyệt; hiển thị 12 sự kiện gần nhất.
- Thử nhanh báo thức và mất/kết nối lại thiết bị mô phỏng.
- Giao diện thích ứng màn hình điện thoại và máy tính.

Nhịp tim là số ngẫu nhiên, đường nhịp trên màn hình chỉ để trang trí, không phải điện tâm đồ. Chưa đo huyết áp, chưa tích hợp cảm biến và chưa đánh giá độ chính xác y tế. CareLink không thay thế bác sĩ hoặc đảm bảo cứu hộ khẩn cấp.

## Chạy ứng dụng

Cần .NET 10 SDK. Mở terminal tại thư mục dự án:

```bash
dotnet restore
dotnet run --launch-profile http
```

Truy cập **http://localhost:5095** trên máy đang chạy ứng dụng. Dừng máy chủ bằng Ctrl+C.

Các file `appsettings*.json` được giữ local và không theo dõi bằng Git. MVP hiện không cần secret hay thiết lập bổ sung trong các file này để chạy. Không đưa thông tin đăng nhập AWS vào source code.

## Hướng dẫn thử

1. Mở trang, nhấn **Bật âm thanh** và cho phép âm thanh nếu trình duyệt yêu cầu. Bạn sẽ nghe một tiếng thử.
2. Nhấn **Thử báo thức trễ check-in**. Kiểm tra banner cảnh báo và tiếng báo lặp lại.
3. Nhấn **Tắt âm thanh**: tiếng dừng, banner vẫn còn. Nhấn **Tôi vẫn ổn** trên banner để hoàn tất check-in và đóng báo thức demo.
4. Tải lại trang: check-in và nhật ký vẫn được giữ. Âm thanh phải bật lại sau khi tải trang.
5. Nhấn **Mô phỏng mất kết nối**: số nhịp tim chuyển thành dấu gạch, không tiếp tục sinh số đo. Nhấn kết nối lại để tiếp tục.
6. Để thử hạn check-in thật, dùng một phiên trình duyệt riêng chưa check-in, đặt giờ nhắc vào phút sắp tới, lưu giờ và giữ trang mở. Khi đến hạn, cảnh báo xuất hiện sau tối đa khoảng 3 giây khi máy chủ/trình duyệt đang hoạt động bình thường.
7. Thu nhỏ cửa sổ hoặc dùng chế độ màn hình điện thoại để kiểm tra bố cục.

## Giới hạn cần biết

- Dữ liệu nằm trong trình duyệt trên máy đang dùng, chưa đồng bộ giữa thiết bị. Xóa dữ liệu trang sẽ mất lịch sử. Không dùng dữ liệu sức khỏe thật trong bản demo.
- Chỉ thử trên một tab; chưa xử lý xung đột ghi giữa nhiều tab.
- Đây là Blazor Interactive Server: tương tác cần kết nối với máy chủ đang chạy.
- Báo thức không được bảo đảm khi đóng tab, máy ngủ, mất kết nối hoặc trình duyệt hạn chế chạy nền. Không dùng làm báo thức khẩn cấp.
- Tắt âm thanh không đồng nghĩa đã check-in. Demo báo thức không thay đổi giờ nhắc; nút **Tôi vẫn ổn** vẫn ghi nhận check-in thật trong dữ liệu local nếu hôm nay chưa xác nhận.
- Chưa có đăng nhập, người hỗ trợ, gửi SMS/email, cảm biến thật hoặc tài nguyên AWS.

## Cấu trúc

- `Components/Pages/Home.razor`: dashboard, check-in, báo thức và các nút demo.
- `Services/CareStore.cs`: trạng thái, quy tắc thời gian và giao diện `ICareStore`; `BrowserCareStore` lưu localStorage.
- `wwwroot/carelink.js`: âm thanh và cầu nối localStorage.
- `wwwroot/app.css`: giao diện responsive.
- `Program.cs`: đăng ký dịch vụ và cấu hình ASP.NET Core.

## Định hướng AWS

Sau khi MVP cục bộ được kiểm tra, sẽ chọn dịch vụ để chạy web, lưu lịch sử có xác thực, xử lý check-in quá hạn trên máy chủ và nhận dữ liệu thiết bị trong tương lai. `ICareStore` là điểm tách phần lưu trữ; adapter AWS cần bổ sung xác thực, phân quyền và xử lý lỗi trước khi dùng thật.

Chưa chốt dịch vụ, tạo tài nguyên hoặc triển khai AWS. Trước bước này cần thống nhất ngân sách, quyền truy cập, cảnh báo chi phí và cách dọn tài nguyên. Backend trên AWS không tự bảo đảm điện thoại/thiết bị sẽ phát chuông khi web đóng; cần thiết kế cơ chế thông báo riêng.

## Kiểm tra đã thực hiện

- `dotnet build --no-restore`: thành công, không warning/error.
- 6 kiểm tra quy tắc: trước hạn, đúng hạn, UTC sang UTC+7, đã check-in, ngày kế tiếp và đầu ngày mới.
- HTTP trang chủ và file JavaScript: trả 200.
- Kiểm tra cú pháp JavaScript bằng `node --check`: đạt.
- Chưa xác minh tương tác trong trình duyệt hoặc âm thanh bằng tai trong phiên phát triển này; thực hiện theo hướng dẫn thử ở trên.

## Quy trình phát triển

Phát triển theo từng giai đoạn: hoàn thiện MVP cục bộ → người dùng thử và phản hồi → tích hợp AWS đã duyệt → đánh giá mở rộng. Không tạo tài nguyên có thể tính phí khi chưa được duyệt. Git bỏ qua appsettings, output build, file IDE và `.env`; dữ liệu đã commit trước đây vẫn tồn tại trong lịch sử Git.
