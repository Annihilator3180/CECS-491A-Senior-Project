CREATE TABLE Accounts (
  Username VARCHAR(30) NOT NULL,
  Email VARCHAR(255),
  Password VARCHAR(30),
  FirstName VARCHAR(20),
  LastName VARCHAR(20),
  IsEnabled BIT ,
  IsAdmin BIT
);

create trigger boof on Accounts after delete as
if ((select count(*)   from Accounts where IsAdmin=1)=0) begin
insert into Accounts (Username,Email,Password,FirstName,LastName,IsEnabled,IsAdmin)
values ('bossadmin','email@gmail.com','Password123','Boss','Man',1,1) end 

INSERT INTO Accounts 
	(username,email,password,first_name,last_name,enabled,administrator)
values ('boofman2','cbass@gmail.com','pass','cbass','vas',1,1);