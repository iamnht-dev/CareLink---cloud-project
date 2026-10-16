# CareLink

CareLink là dự án hướng đến việc hỗ trợ những người sống một mình theo dõi tình trạng và thói quen hằng ngày, đồng thời giúp họ nhận biết khi có điều gì bất thường. Ý tưởng dài hạn là phát triển thành một thiết bị đeo kết nối với hệ thống theo dõi; giai đoạn đầu tập trung vào một ứng dụng web mô phỏng để xây dựng và kiểm tra luồng sử dụng trước khi đầu tư vào phần cứng.

> **Lưu ý về trạng thái dự án:** CareLink hiện ở giai đoạn khởi tạo. Repository có bộ khung Blazor Web App; các chức năng sản phẩm bên dưới là định hướng MVP, chưa được triển khai đầy đủ.

## Mục tiêu

- Hỗ trợ người sống một mình tự xác nhận tình trạng mỗi ngày bằng check-in.
- Nhắc người dùng bằng cảnh báo hiển thị kiểu báo thức nếu check-in bị trễ.
- Dùng dữ liệu sức khỏe mô phỏng để xây dựng giao diện và kiểm tra luồng sản phẩm.
- Tạo nền tảng có thể mở rộng về sau cho người thân/người hỗ trợ và thiết bị đeo.

CareLink không thay thế bác sĩ, không chẩn đoán bệnh và không đảm bảo khả năng cứu hộ khẩn cấp.

## MVP dự kiến

1. Hồ sơ cơ bản của người dùng.
2. Check-in hằng ngày do chính người dùng xác nhận.
3. Một số chỉ số minh họa, chẳng hạn nhịp tim, được gắn nhãn rõ ràng là **dữ liệu mô phỏng**.
4. Khi quá hạn check-in, ứng dụng hiển thị cảnh báo và phát âm thanh như báo thức; người dùng có thể xác nhận hoặc tắt cảnh báo.
5. Hiển thị lịch sử check-in và trạng thái cảnh báo để người dùng kiểm tra.

Web mô phỏng chỉ chứng minh được giao diện và luồng phần mềm. Nó không chứng minh rằng máy tính hay một thiết bị đeo chưa được kiểm định có thể đo chính xác nhịp tim, huyết áp hoặc chỉ số y tế khác. Huyết áp và tích hợp cảm biến thật chưa nằm trong MVP đã định hướng.

## Công nghệ hiện tại

- **C# / ASP.NET Core Blazor Web App** — giao diện web và tương tác phía máy chủ.
- **.NET 10** — SDK mục tiêu của bộ khung hiện tại.
- **Dữ liệu mô phỏng** — dùng trong giai đoạn phát triển ban đầu; chưa kết nối thiết bị y tế.
- **Git và GitHub** — quản lý mã nguồn; nhánh chính là `main`.

AWS chưa được tích hợp. Chỉ xem xét AWS sau khi luồng cục bộ rõ ràng và có đánh giá dịch vụ, chi phí, bảo mật cùng cách dọn tài nguyên. Không tạo tài nguyên AWS có thể tính phí nếu chưa được duyệt.

## Chạy ứng dụng

Cần cài .NET 10 SDK. Từ thư mục chứa `CareHeath.csproj`, chạy:

```bash
dotnet restore
dotnet run
```

Mở địa chỉ localhost được in ra trong terminal để xem ứng dụng. Địa chỉ và cổng có thể khác nhau tùy cấu hình máy.

## Nguyên tắc phát triển

- Làm theo từng giai đoạn; sau mỗi giai đoạn, tổng kết để người dùng kiểm tra trước khi tiếp tục.
- Bắt đầu bằng dữ liệu mô phỏng và phân biệt rõ dữ liệu giả lập với dữ liệu sức khỏe thật.
- Ưu tiên kiến trúc nhỏ, dễ chạy cục bộ; không thêm dịch vụ chỉ để tăng số lượng công nghệ.
- Không đưa khóa truy cập, mật khẩu, token hay thông tin nhạy cảm vào mã nguồn.
- Kiểm soát chi phí AI và AWS; không tự mua/nâng cấp dịch vụ hoặc tạo tài nguyên có thể tính phí khi chưa được duyệt.
- Thiết kế cảnh báo cần xem xét trường hợp mất mạng, thiếu dữ liệu, báo động giả và không có người phản hồi. Báo thức trên web có thể không hoạt động nếu trình duyệt hoặc thiết bị đã tắt.

## Lộ trình

1. **Khởi tạo:** repository GitHub và bộ khung Blazor.
2. **MVP cục bộ:** check-in hằng ngày, dữ liệu mô phỏng, cảnh báo trễ và lịch sử.
3. **Đánh giá:** kiểm tra luồng bình thường, trễ, dữ liệu thiếu và giới hạn khi mất kết nối.
4. **Mở rộng có kiểm soát:** làm rõ nhu cầu người hỗ trợ, thiết bị đeo, kiểm chứng cảm biến và tích hợp cloud trước khi chọn kiến trúc.

## Trạng thái

Hiện repository đã được khởi tạo và đẩy lên GitHub. Bộ khung Blazor là phần mã đã có; các tính năng MVP và kết nối AWS vẫn là công việc tiếp theo.
