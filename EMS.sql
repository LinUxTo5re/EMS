IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = 'EMS')
BEGIN
    CREATE DATABASE EMS;
END;
go

USE EMS
go

IF OBJECT_ID('EmployeeDetails', 'U') IS NOT NULL
BEGIN
    DROP TABLE EmployeeDetails;
END

CREATE TABLE EmployeeDetails
(
  EmployeeId INT PRIMARY KEY IDENTITY(101, 1),
  EmployeeName NVARCHAR(50),
  EmployeeCode INT,
  MailId VARCHAR(25),
  MobileNumber VARCHAR(15), 
  Gender VARCHAR(10),
  CONSTRAINT CHK_EmployeeCodeLength CHECK (LEN(CONVERT(VARCHAR, EmployeeCode)) <= 6),
  --CONSTRAINT CHK_ValidMobileNumber CHECK (ISNUMERIC(MobileNumber) = 1)
);

GO

CREATE OR ALTER PROCEDURE spAddEmployeeDetails
    @EmployeeName NVARCHAR(50),
    @EmployeeCode INT,
    @MailId VARCHAR(25),
    @MobileNumber VARCHAR(15), -- Adjust the size based on your requirements
    @Gender VARCHAR(10)
AS
BEGIN
    INSERT INTO EmployeeDetails (EmployeeName, EmployeeCode, MailId, MobileNumber, Gender)
    VALUES (@EmployeeName, @EmployeeCode, @MailId, @MobileNumber, @Gender);
END;

GO

--EXEC spAddEmployeeDetails
--    @EmployeeName = N'John Doe',
--    @EmployeeCode = 100001,
--    @MailId = 'john.doe@example.com',
--    @MobileNumber = '1234567890',
--    @Gender = 'Male';

--GO

SELECT * FROM dbo.EmployeeDetails;

delete dbo.EmployeeDetails where EmployeeId >110