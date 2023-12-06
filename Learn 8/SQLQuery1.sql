use ITStep

create table Departments (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!='')
)

create table Forms (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!='')
)

create table Groups (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
DepartmentId int not null foreign key references Departments(Id),
FormId int not null foreign key references Forms(Id)
)

create table Students (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
GroupId int not null foreign key references Groups(Id),
Email nvarchar(max)
)