BEGIN
	INSERT INTO dbo.tblUser(Id, FirstName, LastName, Email, UserName, Image, Password, ScoreId )
	VALUES	(newid(), 'Alex', 'Rosas', 'arosas@gmail.com', 'Arosas', 'Test','Test', newid()),
			(newid(), 'Test', 'test', 'test', 'test', 'Test','Test', newid()),
			(newid(), 'Test', 'test', 'test','test' , 'Test', 'Test', newid())
END