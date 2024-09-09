BEGIN
	INSERT INTO dbo.tblScore(Id, UserId, Score, Date, Level)
	VALUES	(newid(), newid(), 6, '2024-05-10', 3),
			(newid(), newid(), 5, '2024-06-01', 1),
			(newid(), newid(), 5, '2024-02-03', 2)
END