CREATE TABLE [dbo].[People]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[FirstName] CHAR (50) NOT NULL,
	[Surname] CHAR (50) NOT NULL,
	[Patronymic] CHAR (50) NOT NULL,
	[Birthday] DATE NOT NULL
)
