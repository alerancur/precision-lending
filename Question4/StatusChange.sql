CREATE TABLE [dbo].[StatusChange]
(
	[TicketId] INT NOT NULL, 
    [Timestamp] DATETIME NOT NULL, 
    [OldStatus] NVARCHAR(50) NULL, 
    [NewStatus] NVARCHAR(50) NOT NULL
)
