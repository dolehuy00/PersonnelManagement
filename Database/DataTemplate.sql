
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'User')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 
GO
INSERT [dbo].[Employees] ([Id], [Address], [BasicSalary], [DateOfBirth], [Fullname], [Position], [StartDate], [Status], [DepartmentId]) VALUES (1, N'273 An Dương Vương', 0.5, CAST(N'2002-08-24T00:00:00.0000000' AS DateTime2), N'Đỗ Lê Huy', N'Root', CAST(N'2024-09-25T00:00:00.0000000' AS DateTime2), 'Active', NULL)
INSERT [dbo].[Employees] ([Id], [Address], [BasicSalary], [DateOfBirth], [Fullname], [Position], [StartDate], [Status], [DepartmentId]) VALUES (2, N'273 An Dương Vương', 0.5, CAST(N'2002-08-24T00:00:00.0000000' AS DateTime2), N'Đỗ Lê Huy', N'Root', CAST(N'2024-09-25T00:00:00.0000000' AS DateTime2), 'Active', NULL)
INSERT [dbo].[Employees] ([Id], [Address], [BasicSalary], [DateOfBirth], [Fullname], [Position], [StartDate], [Status], [DepartmentId]) VALUES (3, N'273 An Duong Vuong, P3, Q5', 2.5, CAST(N'2002-08-24T00:00:00.0000000' AS DateTime2), N'Nguyễn Văn Huy', N'Nhân viên', CAST(N'2024-09-25T00:00:00.0000000' AS DateTime2), 'Active', NULL)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([Id], [Email], [Password], [RoleId], [Status], [EmployeeId]) VALUES (1, N'admin@email.com', N'$2a$11$61d2ukojviUOX8oR2eeBNO0qiq4xCWJrV3OHnQ7NltBDrg2UZas9.', 1, 'Active', 1)
INSERT [dbo].[Accounts] ([Id], [Email], [Password], [RoleId], [Status], [EmployeeId]) VALUES (2, N'user@email.com', N'$2a$11$61d2ukojviUOX8oR2eeBNO0qiq4xCWJrV3OHnQ7NltBDrg2UZas9.', 2, 'Active', 2)
INSERT [dbo].[Accounts] ([Id], [Email], [Password], [RoleId], [Status], [EmployeeId]) VALUES (3, N'dolehuy222@gmail.com', N'$2a$11$16I7fegqRgMDBTWiCIna4.gHwPRv2TMZmVrmE6IqoDSYiwvnvvbPm', 2, 'Active', 3)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO