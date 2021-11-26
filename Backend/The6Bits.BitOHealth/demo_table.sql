CREATE TABLE Accounts (
  username VARCHAR(10) NOT NULL,
  email VARCHAR(30) NOT NULL,
  password VARCHAR(20) NOT NULL,
  first_name VARCHAR(20),
  last_name VARCHAR(20),
  enabled BIT NOT NULL,
  administrator BIT NOT NULL
);


INSERT INTO Accounts 
	(username,email,password,first_name,last_name,enabled,administrator)
values ('boofman2','cbass@gmail.com','pass','cbass','vas',1,1);