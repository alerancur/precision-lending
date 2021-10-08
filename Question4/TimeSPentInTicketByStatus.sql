CREATE PROCEDURE [dbo].[TimeSpentInTicketByStatus]
	@CurrentDateTime DATETIME
AS

;WITH OrderedTickets AS  
(  
	SELECT t.Id, t.Summary, s.OldStatus, s.NewStatus, s.[Timestamp], ROW_NUMBER() OVER (PARTITION BY t.Id ORDER BY s.[Timestamp] DESC) AS RowNumber  
	FROM Ticket t
	JOIN StatusChange s ON s.TicketId = t.Id
), RawData AS (   
SELECT t1.Id, t1.Summary, 
	CASE WHEN t2.OldStatus = 'New' THEN DATEDIFF(MINUTE, t1.[Timestamp], t2.[Timestamp]) ELSE NULL END [New],
	CASE WHEN t2.OldStatus = 'In Progress' THEN DATEDIFF(MINUTE, t1.[Timestamp], t2.[Timestamp]) ELSE NULL END [InProgress],
	CASE WHEN t2.OldStatus = 'Closed' THEN DATEDIFF(MINUTE, t1.[Timestamp], t2.[Timestamp]) ELSE NULL END [Closed],
	CASE WHEN t2.OldStatus = 'Reopened' THEN DATEDIFF(MINUTE, t1.[Timestamp], t2.[Timestamp]) ELSE NULL END [Reopened]
FROM OrderedTickets t1
	LEFT JOIN OrderedTickets t2 on t1.Id = t2.Id and t1.RowNumber = t2.RowNumber+1
-- Calculating Current Status
UNION ALL
SELECT t.Id, t.Summary, 
	CASE WHEN t.NewStatus = 'New' THEN DATEDIFF(MINUTE, t.[Timestamp], @CurrentDateTime) ELSE NULL END [New],
	CASE WHEN t.NewStatus = 'In Progress' THEN DATEDIFF(MINUTE, t.[Timestamp], @CurrentDateTime) ELSE NULL END [InProgress],
	CASE WHEN t.NewStatus = 'Closed' THEN DATEDIFF(MINUTE, t.[Timestamp], @CurrentDateTime) ELSE NULL END [Closed],
	CASE WHEN t.NewStatus = 'Reopened' THEN DATEDIFF(MINUTE, t.[Timestamp], @CurrentDateTime) ELSE NULL END [Reopened]
FROM OrderedTickets t WHERE RowNumber = 1
)
SELECT id, summary, sum(new) new, sum([inprogress]) [in progress], sum(Closed) closed, sum(reopened) reopened
FROM RawData
GROUP BY id, summary


RETURN 0
