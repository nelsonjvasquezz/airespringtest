CREATE DATABASE AireSpringDb
GO
USE AireSpringDb
GO

CREATE TABLE [dbo].[emp_employees](
[emp_employee_id] INT IDENTITY (1, 1) NOT NULL,
[emp_employee_first_name] VARCHAR(100) NOT NULL,
[emp_employee_last_name] VARCHAR(100) NOT NULL,
[emp_employee_phone] VARCHAR(20) NOT NULL,
[emp_employee_zip] VARCHAR(10) NOT NULL,
[emp_hire_date] DATETIME DEFAULT GETUTCDATE() NOT NULL,
CONSTRAINT [pk_employee] PRIMARY KEY CLUSTERED ([emp_employee_id] ASC)
);