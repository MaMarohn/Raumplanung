CREATE TABLE dbo.Room
(
RoomID INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(255),
);

drop table dbo.Room;
drop table dbo.Reservation;
drop table dbo.Teacher;

CREATE TABLE dbo.Teacher
(
TeacherID INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(255),
);

CREATE TABLE dbo.Reservation
(
ReservationID INT PRIMARY KEY IDENTITY(1,1),
Date     DateTime,
Room   INT FOREIGN KEY REFERENCES Room(RoomID) ,
Teacher INT FOREIGN KEY REFERENCES Teacher(TeacherId)
);