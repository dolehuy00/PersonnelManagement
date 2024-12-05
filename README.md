# Giới thiệu  
Xây dựng một website hỗ trợ doanh nghiệp trong việc tổ chức, quản lý các hoạt động liên quan đến nhân sự, phòng ban, lương nhân viên và đặc biệt là quản lý phân công công việc cho các dự án.  
Hệ thống cho phép người dùng đăng nhập với quyền hạn được phân cấp rõ ràng giữa **ADMIN**, **USER** ngoài ra còn các chức năng phân công công việc được **Leader** của phòng ban thực hiện.  

# Luồng chức năng của hệ thống
1. Đầu tiên hệ thống sẽ phải có tài khoản ADMIN để người quản lý đăng nhập vào.
![Đăng Nhập](./Readme_IMG/login.png)
2. Tạo 'phòng ban'(Department).
![Thêm phòng ban](./Readme_IMG/DepartmentAdd.png)
3. Thêm 'nhân viên'(Employee) mới vào 'phòng ban' và tạo 'tài khoản' cho nhân viên.
![Thêm nhân viên](./Readme_IMG/AddEmployee.png)
4. Bầu Leader cho 'phòng ban'.
6. Tạo ra các 'dự án'(Project).
![Thêm nhân viên](./Readme_IMG/ProjectList.png)
8. ADMIN sẽ phân công các 'dự án' cho các 'phòng ban' khác nhau với với 'nhiệm vụ'(DeptAssignment) khác nhau. Mỗi 'dự án' sẽ được nhiều 'phòng ban' phụ trách.
![Thêm nhân viên](./Readme_IMG/DeptAssignmentAdd.png)
9. Khi đăng nhập với tài khoản của leader. Sẽ xem được danh sách các 'nhiệm vụ' được giao cho 'phòng ban' mà leader đó đảm nhiệm. Leader sẽ phân công các 'công việc'(Assignment) của 'dự án' cho các 'nhân viên' thuộc 'phòng ban'.
![Thêm nhân viên](./Readme_IMG/AssignmentForLeader.png)
10. Khi đăng nhập với tài khoản user. Sẽ thấy được danh sách các 'công việc' được phân công. Ngoài ra còn thấy được các thông tin của user và 'lịch sử nhận lương'(Salary history).
![Thêm nhân viên](./Readme_IMG/ProfileUser.png)
11. ADMIN có thể thêm lịch sử lương cho từng nhân viên.
![Thêm nhân viên](./Readme_IMG/SalaryAdd.png)

# Các công nghệ sử dụng  
## Backend  
- **.NET 8**: Dùng để xây dựng các API tương tác với Database và FrontEnd.  
- **SQL Server**: Dùng để lưu trữ dữ liệu cho hệ thống.  
- **JWT Token**: Dùng để xác thực phân quyền cho hệ thống.  
- **Redis Server**: Dùng để lưu trữ refresh token cho người dùng.  

## Frontend  
- **ReactJS**: Thư viện JavaScript phổ biến dùng để xây dựng giao diện người dùng (UI).  

# Mô hình quan hệ dữ liệu (ERD)  
[![Mô hình ERD](#) ](./Readme_IMG/ERD.png) 

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

# Api example
## 1. Department
### a. Filter
```
  GET /api/department/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Optional**               |
| `name`     | `string` | **Optional**               |
| `SortBy`   | `string` | **Required**               | name:asc / name:dec / id:asc / id:dec
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter department.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "DeptDev",
            "taskDetail": "Developing and maintaining applications",
            "status": "Active",
            "leaderId": 3,
            "leaderName": "Nguyễn Văn Huy",
            "deptAssignmentsId": null,
            "employeeIds": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### b. Get One
```
  GET /api/department/get/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an department.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "DeptDev",
            "taskDetail": "Developing and maintaining applications",
            "status": "Active",
            "leaderId": 3,
            "leaderName": "Nguyễn Văn Huy",
            "deptAssignmentsId": null,
            "employeeIds": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### c. Add
```
  POST /api/Department/add
```
Request body
```json
{
    "name": "DeptDev",
    "taskDetail": "Developing and maintaining applications",
    "status": "Active",
    "leaderId": 1
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `name`       | `string`   | **Required**               |
| `taskDetail`     | `string` | **Required**               |
| `status`   | `string` | **Required**               |
| `leaderId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an department.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "DeptDev",
            "taskDetail": "Developing and maintaining applications",
            "status": "Active",
            "leaderId": 1,
            "leaderName": null,
            "deptAssignmentsId": null,
            "employeeIds": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### d. Update
```
  PUT /api/Department/edit
```
Request body
```json
{
    "id": 1,
    "name": "DeptDev",
    "taskDetail": "Developing and maintaining applications",
    "status": "Active",
    "leaderId": 3
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Required**               |
| `name`       | `string`   | **Required**               |
| `taskDetail`     | `string` | **Required**               |
| `status`   | `string` | **Required**               |
| `leaderId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Update an department.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "DeptDev",
            "taskDetail": "Developing and maintaining applications",
            "status": "Active",
            "leaderId": 3,
            "leaderName": null,
            "deptAssignmentsId": null,
            "employeeIds": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### e. Delete
```
  DELETE /api/Department/delete/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Delete an department.",
    "status": 200,
    "messages": [
        "Delete department id = 2 successfully."
    ]
}
```
## 2. Department Assignment
### a. Filter
```
  GET /api/DeptAssignment/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Optional**               |
| `SortBy`   | `string` | **Required**               | id:asc / id:dec
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter deptAssignment.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho các thành viên trong dự án A",
            "projectId": 1,
            "departmentId": 1,
            "departmentName": null,
            "assignmentDTOs": null
        },
        {
            "id": 2,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho các thành viên trong dự án B",
            "projectId": 2,
            "departmentId": 1,
            "departmentName": null,
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 2
}
```
### b. Get One
```
  GET /api/deptassignment/get/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an deptAssignment.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho các thành viên trong dự án A",
            "projectId": 1,
            "departmentId": 1,
            "departmentName": null,
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### c. Add
```
  POST /api/DeptAssignment/add
```
Request body
```json
{
    "priorityLevel": 4,
    "mainTaskDetail": "Phân công công việc cho các thành viên trong dự án A",
    "projectId": 1,
    "departmentId": 1
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `priorityLevel`       | `int`   | **Required**               |
| `mainTaskDetail`     | `string` | **Required**               |
| `projectId`   | `long` | **Required**               |
| `departmentId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an deptAssignment.",
    "status": 200,
    "results": [
        {
            "id": 4,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho các thành viên trong dự án A",
            "projectId": 1,
            "departmentId": 1,
            "departmentName": "DeptDev",
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### d. Add many
```
  POST /api/DeptAssignment/addMany
```
Request body
```json
[
    {
            "priotityLevel": 2,
            "mainTaskDetail": "Phân công công việc cho dự án A",
            "projectId": 1,
            "departmentId": 1,
            "assignmentDTOs": null
        },
        {
            "priotityLevel": 2,
            "mainTaskDetail": "Phân công công việc cho dự án B",
            "projectId": 2,
            "departmentId": 1,
        },
        {
            "priotityLevel": 2,
            "mainTaskDetail": "Phân công công việc cho dự án C",
            "projectId": 3,
            "departmentId": 1,
        }
]
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `priorityLevel`       | `int`   | **Required**               |
| `mainTaskDetail`     | `string` | **Required**               |
| `projectId`   | `long` | **Required**               |
| `departmentId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create multiple deptAssignments.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho dự án A",
            "projectId": 1,
            "departmentId": 1,
            "assignmentDTOs": null
        },
        {
            "id": 2,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho dự án B",
            "projectId": 2,
            "departmentId": 1,
            "assignmentDTOs": null
        },
        {
            "id": 3,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho dự án C",
            "projectId": 3,
            "departmentId": 1,
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### e. Update
```
  PUT /api/DeptAssignment/edit
```
Request body
```json
{
    "id": 4,
    "priorityLevel": 1,
    "mainTaskDetail": "Phân công công việc dự án A",
    "projectId": 1,
    "departmentId": 1
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Required**               |
| `priorityLevel`       | `int`   | **Required**               |
| `mainTaskDetail`     | `string` | **Required**               |
| `projectId`   | `long` | **Required**               |
| `departmentId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Update an deptAssignment.",
    "status": 200,
    "results": [
        {
            "id": 4,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc dự án A",
            "projectId": 1,
            "departmentId": 1,
            "departmentName": "DeptDev",
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### f. Delete
```
  DELETE /api/DeptAssignment/delete/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Delete an deptAssignment.",
    "status": 200,
    "messages": [
        "Delete deptAssignment id = 5 successfully."
    ]
}
```
### g. Search Id
```
  GET /api/deptassignment/search
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Optional**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Search dept assignment",
    "status": 200,
    "results": [
        {
            "id": 1,
            "priotityLevel": 0,
            "mainTaskDetail": "Phân công công việc cho dự án A",
            "projectId": 1,
            "departmentId": 1,
            "departmentName": "DeptDev",
            "assignmentDTOs": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

## 3. Project
### a. Filter
```
  GET /api/Project/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `startDate`       | `date`   | **Optional**        | format `YYYY-MM-DD`
| `duration`     | `date` | **Optional**               |format `YYYY-MM-DD`
| `SortBy`   | `string` | **Required**               | id:asc / id:dec
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter project.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "Employee Training Program",
            "detail": "Implement a training program for employees to improve skills and knowledge in key areas.",
            "duration": "2024-05-20T00:00:00",
            "startDate": "2024-01-20T00:00:00",
            "status": "In Progress",
            "deptAssignmentDTOs": []
        },
        {
            "id": 2,
            "name": "Customer Relationship Management System",
            "detail": "Develop a CRM system to enhance customer engagement and streamline sales processes.",
            "duration": "2024-10-15T00:00:00",
            "startDate": "2024-06-01T00:00:00",
            "status": "In Progress",
            "deptAssignmentDTOs": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 2
}
```

### b. Add
```
  POST /api/project/add
```
Request body
```json
{
    "Name": "Employee Training Program",
    "Detail": "Implement a training program for employees to improve skills and knowledge in key areas.",
    "Duration": "2024-05-20",
    "StartDate": "2024-01-20",
    "Status": "In Progress"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `name`       | `string`   | **Required**               |
| `Detail`     | `string` | **Required**               |
| `status`   | `string` | **Required**               |
| `Duration`   | `date` | **Required**               |format `YYYY-MM-DD`
| `StartDate`   | `date` | **Required**               |format `YYYY-MM-DD`

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an project.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "Employee Training Program",
            "detail": "Implement a training program for employees to improve skills and knowledge in key areas.",
            "duration": "2024-05-20T00:00:00",
            "startDate": "2024-01-20T00:00:00",
            "status": "In Progress",
            "deptAssignmentDTOs": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### c. Update
```
  PUT /api/project/edit
```
Request body
```json
{
    "id": 1,
    "Name": "Employee Training Program",
    "Detail": "Implement a training program for employees to improve skills and knowledge in key areas.",
    "Duration": "2024-05-25",
    "StartDate": "2024-01-20",
    "Status": "In Progress"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Required**               |
| `name`       | `string`   | **Required**               |
| `Detail`     | `string` | **Required**               |
| `status`   | `string` | **Required**               |
| `Duration`   | `date` | **Required**               |format `YYYY-MM-DD`
| `StartDate`   | `date` | **Required**               |format `YYYY-MM-DD`

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Update an project.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "Employee Training Program",
            "detail": "Implement a training program for employees to improve skills and knowledge in key areas.",
            "duration": "2024-05-25T00:00:00",
            "startDate": "2024-01-20T00:00:00",
            "status": "In Progress",
            "deptAssignmentDTOs": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### d. Delete
```
  DELETE /api/project/delete/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Delete an project.",
    "status": 200,
    "messages": [
        "Delete project id = 4 successfully."
    ]
}
```

## 4. Account
### a. Login
```
  POST /api/Account/login
```
Request body
```json
{
    "email": "admin@email.com",
    "password":"12345678"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `email`       | `string`   | **Required**               |
| `password`     | `string` | **Required**               |

Response body
```json
{
    "title": "Login successfully",
    "status": 200,
    "results": [
        {
            "id": 1,
            "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIyNjk3ZDBlYy0yZGI1LTQwZmEtODE5Yi1jNmQwMmYxY2MzNmMiLCJpYXQiOiIwMi8xMi8yMDI0IDM6NDg6MzYgQ0giLCJJZCI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTczMzE1NDU3NiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0NzU2MyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDc1NjMifQ.b0a6Z429uJfdmKNSj509acH53s5BgJ1IpcnxEmuo3UA",
            "email": "admin@email.com",
            "employeeName": "Đỗ Lê Huy",
            "employeeImage": "https://localhost:7084/image/avatar-user-1.jpg/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTdGFpY0ltYWdlQWNjZXNzVG9rZW4iLCJqdGkiOiJiNDE0MGVhZS1kYTNjLTRmOTEtYWRmZC03OWQ2NDY0NDdiN2YiLCJpYXQiOiIwMi8xMi8yMDI0IDM6NDg6MzYgQ0giLCJleHAiOjE3MzMxNTQ1NzYsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDc1NjMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ3NTYzIn0.9O-s84SainNByFYJR_WVOTxGc5mR5gczayCBXgXUP4k",
            "role": "Admin",
            "leaderOfDepartments": [
                {
                    "id": 1,
                    "name": "DeptDev"
                }
            ]
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### b. Get new access token
```
  POST /api/Account/get-access-token
```
Request cookies
| Name        | Value     | Description                | HttpOnly
| :--------  | :------- | :------------------------- | :----------------
| `refreshToken`       | `{token}`   | **Required**    | `true`

Response body and cookie new refreshToken
```json
{
    "title": "Get access successfully.",
    "status": 200,
    "results": [
        {
            "newAccessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiIyNjk3ZDBlYy0yZGI1LTQwZmEtODE5Yi1jNmQwMmYxY2MzNmMiLCJpYXQiOiIwMi8xMi8yMDI0IDM6NDg6MzYgQ0giLCJJZCI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTczMzE1NDU3NiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo0NzU2MyIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDc1NjMifQ.b0a6Z429uJfdmKNSj509acH53s5BgJ1IpcnxEmuo3UA",
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### c. Change password
```
  POST /api/Account/change-password
```
Request body
```json
{
    "currentPassword": "123456789",
    "newPassword":"12345678",
    "passwordConfirm":"12345678"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `currentPassword`       | `string`   | **Required**               |
| `newPassword`     | `string` | **Required**               |
| `passwordConfirm`     | `string` | **Required**               |


Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}


Response body
```json
{
    "title": "Change password.",
    "status": 200,
    "messages": [
        "Password changed successfully."
    ]
}
```

### d. Forgot password request
```
  POST /api/Account/forgot-password-request
```
Request body
```json
{
    "email": "dolehuy222@gmail.com"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `email`       | `string`   | **Required**               |

Response body
```json
{
    "title": "Forgot password.",
    "status": 200,
    "messages": [
        "Send request successfully."
    ]
}
```

### e. Forgot password verify code
```
  POST /api/Account/forgot-password-verify-code
```
Request body
```json
{
    "email": "dolehuy222@gmail.com",
    "code": "173328"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `email`       | `string`   | **Required**               |
| `code`       | `string`   | **Required**               |

Response body
```json
{
    "title": "Forgot password verify code.",
    "status": 200,
    "messages": [
        "Code verification successful."
    ]
}
```

### e. Forgot password change password
```
  POST /api/Account/forgot-password-change
```
Request body
```json
{
    "email": "dolehuy222@gmail.com",
    "code": 173328,
    "Password": "1234567899",
    "PasswordConfirm": "1234567899"
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `email`       | `string`   | **Required**               |
| `code`       | `string`   | **Required**               |
| `Password`       | `string`   | **Required**               |
| `PasswordConfirm`       | `string`   | **Required**               |

Response body
```json
{
    "title": "Forgot password change password.",
    "status": 200,
    "messages": [
        "Change password successfully."
    ]
}
```
### f. Filter
```
  GET /api/Account/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `keyword`       | `string`   | **Optional**               |
| `filterByRole`       | `string`   | **Optional**               |
| `keywordByEmployee`       | `string`   | **Optional**               |
| `filterByStatus`     | `string` | **Optional**               |
| `sortByEmail`   | `string` | **Required**               | "dec" or "asc"
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter account.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "email": "admin@email.com",
            "roleId": 1,
            "roleName": "Admin",
            "status": "Active",
            "employeeId": 1,
            "employeeName": "Đỗ Lê Huy",
            "employeeImage": "https://localhost:7084/image/avatar-user-1.jpg",
            "leaderOfDepartments": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### g. Get One
```
  GET /api/Account/get/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an account.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "email": "admin@email.com",
            "roleId": 1,
            "roleName": "Admin",
            "status": "Active",
            "employeeId": 1,
            "employeeName": "Đỗ Lê Huy"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### h. Add
```
  POST /api/account/add
```
Request body
```json
{
    "Email": "email@gmail.com",
    "RoleId": 2,
    "Status": "Active",
    "EmployeeId": 11
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Email`       | `string`   | **Required**               |
| `RoleId`     | `int` | **Required**               |
| `status`   | `string` | **Required**               |
| `EmployeeId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an account.",
    "status": 200,
    "results": [
        {
            "id": 4,
            "email": "huydo24082002@gmail.com",
            "roleId": 2,
            "roleName": null,
            "status": "Active",
            "employeeId": 11,
            "employeeName": null,
            "leaderOfDepartments": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### i. Update
```
  PUT /api/Account/edit
```
Request body
```json
{
    "Id": 3,
    "Email": "email@gmail.com",
    "RoleId": 2,
    "Status": "Active",
    "employeeId": 3
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Required**               |
| `Email`       | `string`   | **Required**               |
| `RoleId`     | `int` | **Required**               |
| `status`   | `string` | **Required**               |
| `EmployeeId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Update an account.",
    "status": 200,
    "results": [
        {
            "id": 3,
            "email": "dolehuy222@gmail.com",
            "roleId": 2,
            "roleName": null,
            "status": "Active",
            "employeeId": 3,
            "employeeName": null,
            "employeeImage": null,
            "leaderOfDepartments": []
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### j. Delete
```
  DELETE /api/Account/delete/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Delete an account.",
    "status": 200,
    "messages": [
        "Delete account id = 5 successfully."
    ]
}
```


## 5. Role
### a. Get all
```
  GET /api/Role/get/all
```
Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get all Role.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "name": "Admin"
        },
        {
            "id": 2,
            "name": "User"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 2
}
```

## 6. Assignment
### a. Filter
```
  GET /api/assignment/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Status`       | `string`   | **Optional**               |
| `ResponsiblePesonId`       | `long`   | **Optional**               |
| `ProjectId`       | `long`   | **Optional**               |
| `DepartmentId`     | `long` | **Optional**               |
| `SortBy`   | `string` | **Required**               | fullname:asc / fullname:dec / priotityLevel:asc / priotityLevel:dec
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter Assignment.",
    "status": 200,
    "results": [
        {
            "id": 6,
            "detail": "Conduct a market analysis to identify trends and opportunities for the upcoming product launch.",
            "name": "Market Analysis Report",
            "priotityLevel": 5,
            "status": "Assigned",
            "responsiblePesonId": 2,
            "responsiblePesonName": "Đỗ Lê Huy",
            "deptAssignmentId": 1,
            "projectId": 1,
            "projectName": "Employee Training Program"
        },
        {
            "id": 9,
            "detail": "Oversee the implementation of the new HR software across all departments.",
            "name": "HR Software Implementation",
            "priotityLevel": 5,
            "status": "Pending",
            "responsiblePesonId": null,
            "responsiblePesonName": null,
            "deptAssignmentId": 1,
            "projectId": 1,
            "projectName": "Employee Training Program"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 10
}
```
### g. Get One
```
  GET /api/assignment/get/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an Assignment.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "detail": "Prepare the quarterly financial report for review.",
            "name": "Quarterly Financial Report",
            "priotityLevel": 3,
            "status": "Assigned",
            "responsiblePesonId": 3,
            "responsiblePesonName": "Nguyễn Văn Huy",
            "deptAssignmentId": 1,
            "projectId": null,
            "projectName": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

### h. Add
```
  POST /api/assignment/add
```
Request body
```json
{
    "detail": "Develop the presentation slides for the annual meeting.",
    "name": "Annual Meeting Presentation", 
    "PriotityLevel": 1, 
    "Status": "Completed", 
    "DeptAssignmentId": 3 
}

```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `detail`       | `string`   | **Required**               |
| `name`     | `string` | **Required**               |
| `PriotityLevel`     | `int` | **Required**               |
| `status`   | `string` | **Required**               |
| `DeptAssignmentId`   | `long` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an Assignment.",
    "status": 200,
    "results": [
        {
            "id": 8,
            "detail": "Develop the presentation slides for the annual meeting.",
            "name": "Annual Meeting Presentation",
            "priotityLevel": 1,
            "status": "Completed",
            "responsiblePesonId": null,
            "responsiblePesonName": null,
            "deptAssignmentId": 3,
            "projectId": null,
            "projectName": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```

## 7. Employee
### a. Add
```
  POST /api/Employee/add
```
Request body form-data
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Address`       | `string`   | **Required**               |
| `BasicSalary`     | `double` | **Required**               |
| `DateOfBirth`   | `string` | **Required**               | format `YYYY-MM-DD`
| `Fullname`   | `string` | **Required**               |
| `Position`   | `string` | **Required**               |
| `StartDate`   | `string` | **Required**               | format `YYYY-MM-DD`
| `Status`   | `string` | **Required**               | Active / Lock
| `FileImage`   | `file` | **Optional**               |
| `DepartmentId`   | `long` | **Optional**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Create an employee.",
    "status": 200,
    "results": [
        {
            "id": 10042,
            "address": "273 An Duong Vuong",
            "basicSalary": 20,
            "image": null,
            "dateOfBirth": "2002-03-28T00:00:00",
            "fullname": "Nguyễn Văn Tèo",
            "position": "Nhân viên",
            "startDate": "2024-03-28T00:00:00",
            "status": "Active",
            "departmentId": null,
            "departmentName": null,
            "fileImage": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### b. Change image by Admin
```
  POST /api/Employee/change-image/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request body form-data
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `file`   | `file` | **Requied**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Change image employee.",
    "status": 200,
    "results": [
        {
            "id": "1",
            "fileUrl": "https://localhost:7084/image/avatar-user-1.jpg"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### c. Change image by User
```
  POST /api/Employee/change-image
```
Request body form-data
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `file`   | `file` | **Requied**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Change image employee.",
    "status": 200,
    "results": [
        {
            "id": "1",
            "fileUrl": "https://localhost:7084/image/avatar-user-1.jpg"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### d. Edit
```
  PUT /api/Employee/edit
```
Request body
```json
{
    "id": 3,
    "address": "273 An Dương Vương",
    "basicSalary": 0.5,
    "image": "https://localhost:7084/image/avatar-user-1.jpg",
    "dateOfBirth": "2002-08-24T00:00:00",
    "fullname": "Nguyễn Văn Huy",
    "position": "Leader",
    "startDate": "2024-09-25T00:00:00",
    "departmentId": 1
}
```
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `id`       | `long`   | **Required**               |
| `address`       | `string`   | **Required**               |
| `basicSalary`     | `double` | **Required**               |
| `image`   | `string` | **Optional**               |
| `dateOfBirth`   | `string` | **Required**               |format `YYYY-MM-DD`
| `fullname`       | `string`   | **Required**               |
| `position`     | `string` | **Required**               |
| `startDate`   | `string` | **Required**               |format `YYYY-MM-DD`
| `departmentId`   | `long` | **Optional**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Update an employee.",
    "status": 200,
    "results": [
        {
            "id": 3,
            "address": "273 An Dương Vương",
            "basicSalary": 0.5,
            "image": "https://localhost:7084/image/avatar-user-1.jpg",
            "dateOfBirth": "2002-08-24T00:00:00",
            "fullname": "Nguyễn Văn Huy",
            "position": "Leader",
            "startDate": "2024-09-25T00:00:00",
            "status": null,
            "departmentId": 1,
            "departmentName": null
        }
    ],
    "page": 1,
    "totalPage": 1,
}
```
### e. Delete
```
  DELETE /api/Employee/delete/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Delete an employee.",
    "status": 200,
    "messages": [
        "Delete employee id = 10042 successfully."
    ]
}
```
### f. Get one by Admin
```
  GET /api/Employee/get/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an employee.",
    "status": 200,
    "results": [
        {
            "id": 10034,
            "address": "66 Nguyễn Trãi",
            "basicSalary": 0.03,
            "image": "https://localhost:7084/image/avatar-user-10034.jpg/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTdGFpY0ltYWdlQWNjZXNzVG9rZW4iLCJqdGkiOiIwZmNlZTFmNC03ZmVhLTQ2YjUtOTkyMy0yNmI3OTQzMzdhNjYiLCJpYXQiOiIwNS8xMi8yMDI0IDc6MTU6MzEgU0EiLCJleHAiOjE3MzMzODI5OTEsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDc1NjMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ3NTYzIn0.OonDEdDi5l15FfRfApNEASuFu_iSa3m5uZM_0OjD1w0",
            "dateOfBirth": "2024-12-19T00:00:00",
            "fullname": "Nguyen Van Ti",
            "position": "Nhân viên",
            "startDate": "2024-12-12T00:00:00",
            "status": "Active",
            "departmentId": null,
            "departmentName": null,
            "fileImage": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### g. Get one by User
```
  GET /api/Employee/get
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get an employee.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "address": "273 An Dương Vương",
            "basicSalary": 0.5,
            "image": "https://localhost:7084/image/avatar-user-1.jpg/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJTdGFpY0ltYWdlQWNjZXNzVG9rZW4iLCJqdGkiOiJhNzhkNjJjZS1lOWViLTQ4Y2YtOGRiNC1iMzMzMzQ1MDAwZDQiLCJpYXQiOiIwNS8xMi8yMDI0IDc6MTY6MDkgU0EiLCJleHAiOjE3MzMzODMwMjksImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NDc1NjMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjQ3NTYzIn0.uPplo6H4YPSG3ZL-CLp0tSadGv_yGyF-NBrZ-6t1_kM",
            "dateOfBirth": "2002-08-24T00:00:00",
            "fullname": "Đỗ Lê Huy",
            "position": "Root",
            "startDate": "2024-09-25T00:00:00",
            "status": "Active",
            "departmentId": 1,
            "departmentName": "Dev",
            "fileImage": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### h. Search Name or Id
```
  GET /api/Employee/search
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `fullnameOrId`     | `string` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Get page employee.",
    "status": 200,
    "results": [
        {
            "id": 1,
            "address": "273 An Dương Vương",
            "basicSalary": 0.5,
            "dateOfBirth": "2002-08-24T00:00:00",
            "fullname": "Đỗ Lê Huy",
            "position": "Root",
            "startDate": "2024-09-25T00:00:00",
            "accountId": null,
            "status": "Active",
            "departmentId": null,
            "departmentName": "No"
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 1
}
```
### i. Filter
```
  GET /api/Employee/filter
```
Request params
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `NameOrId`       | `string`   | **Optional**               |
| `Address`     | `string` | **Optional**               |
| `FromDoB`       | `string`   | **Optional**               |format `YYYY-MM-DD`
| `ToDoB`     | `string` | **Optional**               | format `YYYY-MM-DD`
| `FromSalary`       | `double`   | **Optional**               |
| `ToSalary`     | `double` | **Optional**               |
| `Position`       | `string`   | **Optional**               |
| `FromStartDate`     | `string` | **Optional**               |format `YYYY-MM-DD`
| `ToStartDate`       | `string`   | **Optional**               |format `YYYY-MM-DD`
| `DepartmentId`     | `string` | **Optional**               |
| `SortBy`   | `string` | **Required**               | fullname:asc / fullname:dec / dateOfBirth:asc / dateOfBirth:dec / startDate:asc / startDate:dec
| `Page`   | `int` | **Required**               |
| `PageSize`   | `int` | **Required**               |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Filter employee.",
    "status": 200,
    "results": [
        {
            "id": 10032,
            "address": "273 Võ Văn Kiệt",
            "basicSalary": 765432,
            "image": null,
            "dateOfBirth": "2024-11-25T00:00:00",
            "fullname": "Đỗ Lê Huy 2",
            "position": "Nhân Viên",
            "startDate": "2024-11-12T00:00:00",
            "status": "Lock",
            "departmentId": null,
            "departmentName": null
        },
        {
            "id": 10031,
            "address": "273 Võ Văn Kiệt",
            "basicSalary": 2,
            "image": null,
            "dateOfBirth": "2002-08-28T00:00:00",
            "fullname": "Đỗ Lê Huy",
            "position": "Nhân Viên",
            "startDate": "2024-11-12T00:00:00",
            "status": "Active",
            "departmentId": null,
            "departmentName": null
        }
    ],
    "page": 1,
    "totalPage": 1,
    "totalCount": 2
}
```
### j. Lock
```
  PUT /api/Employee/lock/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Lock employee id = 11.",
    "status": 200,
    "messages": [
        "11",
        "Lock"
    ]
}
```
### j. Unlock
```
  PUT /api/Employee/unlock/{id}
```
Path parameters
| Key | Type     | Description                       |
| :--------  | :------- | :-------------------------------- |
| `id`     | `long` | **Required** |

Request header
| Key        | Type     | Description                | Data struct
| :--------  | :------- | :------------------------- | :----------------
| `Authorization`       | `string`   | **Required**  | Bearer {tokenJwt}

Response body
```json
{
    "title": "Unlock employee id = 11.",
    "status": 200,
    "messages": [
        "11",
        "Unlock"
    ]
}
```
