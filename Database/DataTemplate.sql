SET IDENTITY_INSERT [dbo].[AccountStatuses] ON 
GO
INSERT [dbo].[AccountStatuses] ([Id], [Name]) VALUES (1, N'Hoạt động')
GO
INSERT [dbo].[AccountStatuses] ([Id], [Name]) VALUES (2, N'Đã khóa')
GO
SET IDENTITY_INSERT [dbo].[AccountStatuses] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatuses] ON 
GO
INSERT [dbo].[EmployeeStatuses] ([Id], [Name]) VALUES (1, N'Hoạt động')
GO
INSERT [dbo].[EmployeeStatuses] ([Id], [Name]) VALUES (2, N'Dừng hoạt động')
GO
INSERT [dbo].[EmployeeStatuses] ([Id], [Name]) VALUES (3, N'Tạm dừng hoạt động')
GO
SET IDENTITY_INSERT [dbo].[EmployeeStatuses] OFF
GO
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
INSERT [dbo].[Employees] ([Id], [Address], [BasicSalary], [DateOfBirth], [Fullname], [Position], [StartDate], [AccountId], [StatusId], [TeamId]) VALUES (1, N'273 An Dương Vương', 0.5, CAST(N'2002-08-24T00:00:00.0000000' AS DateTime2), N'Đỗ Lê Huy', N'Root', CAST(N'2024-09-25T00:00:00.0000000' AS DateTime2), NULL, 1, NULL)
INSERT [dbo].[Employees] ([Id], [Address], [BasicSalary], [DateOfBirth], [Fullname], [Position], [StartDate], [AccountId], [StatusId], [TeamId]) VALUES (2, N'273 An Dương Vương', 0.5, CAST(N'2002-08-24T00:00:00.0000000' AS DateTime2), N'Đỗ Lê Huy', N'Root', CAST(N'2024-09-25T00:00:00.0000000' AS DateTime2), NULL, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 
GO
INSERT [dbo].[Accounts] ([Id], [Email], [Password], [RoleId], [StatusId], [EmployeeId]) VALUES (1, N'admin@email.com', N'$2a$11$61d2ukojviUOX8oR2eeBNO0qiq4xCWJrV3OHnQ7NltBDrg2UZas9.', 1, 1, 1)
INSERT [dbo].[Accounts] ([Id], [Email], [Password], [RoleId], [StatusId], [EmployeeId]) VALUES (2, N'user@email.com', N'$2a$11$61d2ukojviUOX8oR2eeBNO0qiq4xCWJrV3OHnQ7NltBDrg2UZas9.', 2, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO