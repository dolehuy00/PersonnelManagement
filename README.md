# Giới thiệu  
Xây dựng một website hỗ trợ doanh nghiệp trong việc tổ chức, quản lý các hoạt động liên quan đến nhân sự, phòng ban, lương nhân viên và đặc biệt là quản lý phân công công việc cho các dự án.  
Hệ thống cho phép người dùng đăng nhập với quyền hạn được phân cấp rõ ràng giữa **ADMIN**, **USER** ngoài ra còn các chức năng phân công công việc được **Leader** của phòng ban thực hiện.  

# Luồng chức năng của hệ thống
1. Đầu tiên hệ thống sẽ phải có tài khoản ADMIN để người quản lý đăng nhập vào.
2. Tạo 'phòng ban'(Department).
3. Thêm 'nhân viên'(Employee) mới vào 'phòng ban' và tạo 'tài khoản' cho nhân viên.
5. Bầu Leader cho 'phòng ban'.
6. Tạo ra các 'dự án'(Project).
7. ADMIN sẽ phân công các 'dự án' cho các 'phòng ban' khác nhau với với 'nhiệm vụ'(DeptAssignment) khác nhau. Mỗi 'dự án' sẽ được nhiều 'phòng ban' phụ trách.
8. Khi đăng nhập với tài khoản của leader. Sẽ xem được danh sách các 'nhiệm vụ' được giao cho 'phòng ban' mà leader đó đảm nhiệm. Leader sẽ phân công các 'công việc'(Assignment) của 'dự án' cho các 'nhân viên' thuộc 'phòng ban'.
9. Khi đăng nhập với tài khoản user. Sẽ thấy được danh sách các 'công việc' được phân công. Ngoài ra còn thấy được các thông tin của user và 'lịch sử nhận lương'(Salary history).
10. ADMIN có thể thêm lịch sử lương cho từng nhân viên.

# Các công nghệ sử dụng  
## Backend  
- **.NET 8**: Dùng để xây dựng các API tương tác với Database và FrontEnd.  
- **SQL Server**: Dùng để lưu trữ dữ liệu cho hệ thống.  
- **JWT Token**: Dùng để xác thực phân quyền cho hệ thống.  
- **Redis Server**: Dùng để lưu trữ refresh token cho người dùng.  

## Frontend  
- **ReactJS**: Thư viện JavaScript phổ biến dùng để xây dựng giao diện người dùng (UI).  

# Mô hình quan hệ dữ liệu (ERD)  
![Mô hình ERD](#)  

# Giao diện Website  
## Giao diện đăng nhập  
![Giao diện đăng nhập](#)  

## Giao diện User  
![Giao diện User](#)  

## Giao diện Admin  
### Quản lý Nhân viên  
![Quản lý Nhân viên](#)  

### Quản lý Phòng ban  
![Quản lý Phòng ban](#)  

### Quản lý dự án  
![Quản lý dự án](#)  

### Quản lý phân công công việc  
![Quản lý phân công công việc](#)  

### Quản lý lịch sử lương  
![Quản lý lịch sử lương](#)  

# Cài đặt  
## Cài đặt môi trường  
### BackEnd  
- Cài đặt SDK .NET 8 tại: https://dotnet.microsoft.com/en-us/download 
- Cài đặt Visual Studio hoặc các IDE hỗ trợ chạy ASP.NET.
- Cài đặt **SQL Server** và **Redis** với Docker:
  ```bash
   docker-compose up -d
### FrontEnd  
- Cài đặt **npm** tại: https://nodejs.org/en

## Chạy ứng dụng  
### BackEnd  
1. Clone source:  
   ```bash
   git clone https://github.com/dolehuy00/PersonnelManagement.git
2. Khởi động SQL Server, Redis Server với docker.
3. Khởi tạo Database: Mở terminal và chạy lệnh để khởi tạo database cho hệ thống
   ```bash
   update-database
5. Sử dụng IDE để chạy backend hoặc mở Terminal trong thư mục và chạy lệnh:
   ```bash
   dotnet run
### FrontEnd
1. Clone source:  
   ```bash
   git clone https://github.com/dolehuy00/PersonManagementFEReact.git
2. Trong thư mục dự án, chạy lệnh:
   ```bash
   npm install
   npm start
