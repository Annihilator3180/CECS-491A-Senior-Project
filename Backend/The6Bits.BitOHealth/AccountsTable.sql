CREATE TABLE Accounts (
  Username VARCHAR(30) NOT NULL,
  Email VARCHAR(255),
  Password VARCHAR(30),
  FirstName VARCHAR(20),
  LastName VARCHAR(20),
  IsEnabled BIT ,
  IsAdmin BIT
);


INSERT INTO Accounts 
	(Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin)
values ('bossadmin12','cbass@gmail.com','Password!1','admin','boss',1,1);