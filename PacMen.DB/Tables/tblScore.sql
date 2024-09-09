CREATE TABLE [dbo].[tblScore]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Score] Int NOT NULL,
    [Date] DATETIME Not null,
    [Level] Int Not null
    
)
