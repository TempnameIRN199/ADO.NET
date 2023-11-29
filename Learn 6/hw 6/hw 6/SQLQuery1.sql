create database lab
use lab

create table Users (
Id int not null identity primary key,
[Login] nvarchar(100) unique not null,
PasswordHash nvarchar(50) not null,
[Address] nvarchar(max) not null,
Phone nvarchar(20),
IsAdmin bit
)