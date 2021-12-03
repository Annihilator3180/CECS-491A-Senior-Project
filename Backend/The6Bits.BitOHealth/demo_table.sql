CREATE TABLE Accounts (
  Username VARCHAR(10) NOT NULL,
  Email VARCHAR(30) ,
  Password VARCHAR(20),
  FirstName VARCHAR(20),
  LastName VARCHAR(20),
  IsEnabled BIT ,
  IsAdmin BIT
);



INSERT INTO Accounts 
	(username,email,password,first_name,last_name,enabled,administrator)
values ('boofman2','cbass@gmail.com','pass','cbass','vas',1,1);