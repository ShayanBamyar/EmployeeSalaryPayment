CREATE TABLE [dbo].[EmployeeSalaryPayment_Tbl]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [FirstName] NVARCHAR(150) NOT NULL,
    [LastName] NVARCHAR(150) NOT NULL,
    [BasicSalary] DECIMAL(15) NOT NULL,
    [Allowance] DECIMAL(15) NOT NULL,
    [Transportation] DECIMAL(15) NOT NULL,
    [Date] DATETIME NOT NULL
);
