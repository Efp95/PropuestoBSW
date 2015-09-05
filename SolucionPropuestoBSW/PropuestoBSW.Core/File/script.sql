CREATE DATABASE DbSolucion
GO--GO

USE DbSolucion
GO--GO


CREATE TABLE TB_Log
(
	IdLog			int identity(1,1) primary key,
	LogMessage		varchar(max),
	Logdescription	char(1)
)
GO--GO


CREATE PROCEDURE dbo.USP_InsertLog
(
	@pMessage		varchar(max),
	@pDescription	char(1)
)
AS
BEGIN
SET NOCOUNT ON;

	INSERT INTO TB_Log (LogMessage, LogDescription)
	VALUES (@pMessage, @pDescription)

SET NOCOUNT OFF;
END
GO--GO

