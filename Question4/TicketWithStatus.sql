CREATE VIEW [dbo].[TicketWithStatus]
	AS 

	WITH OrderedTickets AS  
	(  
		SELECT t.Id, t.Summary, s.NewStatus [Status], ROW_NUMBER() OVER (PARTITION BY t.Id ORDER BY s.Timestamp DESC) AS RowNumber  
		FROM Ticket t
		JOIN StatusChange s ON s.TicketId = t.Id
	)   
	SELECT Id, Summary, [Status]
	FROM OrderedTickets   
	WHERE RowNumber = 1 AND [Status] <> 'Closed'; 
