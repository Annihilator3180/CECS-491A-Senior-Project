CREATE TABLE Logs (
  username VARCHAR(30) NOT NULL,
  description VARCHAR(255),
  LogLevel VARCHAR(30),
  LogCategory VARCHAR(30),
  Date_Time DateTime

);


INSERT INTO Logs (username, description, LogLevel, LogCategory, Date_Time) values ('{username}', '{description}', '{LogLevel}' , '{LogCategory}',CURRENT_TIMESTAMP   )