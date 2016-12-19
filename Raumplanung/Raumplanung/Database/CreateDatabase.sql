-- Lokale Datenbank name: (localdb)\MSSQLLocalDB

drop table dbo.Room;
drop table dbo.Reservation;
drop table dbo.Teacher;


Create Database Reservation_Service;

CREATE TABLE dbo.Room
(
RoomID INT PRIMARY KEY IDENTITY(1,1),
Name VARCHAR(255),
);

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