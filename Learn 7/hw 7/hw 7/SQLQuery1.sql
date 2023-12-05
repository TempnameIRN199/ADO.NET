create database Shop
use Shop
use kuma
drop table Products
drop table Sellers
drop table Buyer
drop table Sales

create table Products (
Id int not null identity primary key,
[Name] nvarchar(max) not null check([Name]!=''),
Model nvarchar(max) not null check(Model!=''),
Price money not null default(0.01) check(Price!=''),
[Count] int not null
)

create table Sellers (
Id int not null identity primary key,
FName nvarchar(100) not null check(FName!=''),
Email nvarchar(100) null,
Phone nvarchar(20) not null check(Phone!='')
)

create table Buyers (
Id int not null identity primary key,
FName nvarchar(100) not null check(FName!=''),
Email nvarchar(100) not null check(Email!=''),
Phone nvarchar(20) not null check(Phone!='')
)

create table Sales (
Id int not null identity primary key,
BuyerId int not null foreign key references Buyers(Id),
SellerId int not null foreign key references Sellers(Id),
SaleDate date not null
)
create table SaleProducts (
Id int not null identity primary key,
SaleId int not null foreign key references Sales(Id),
ProductId int not null foreign key references Products(Id),
Quantity int not null check(Quantity > 0),
Price money not null
)

select p.Name, sp.Quantity from SaleProducts as sp
join Products as p on sp.ProductId = p.Id
where p.Id = @Id




DECLARE @BuyerId INT;

SELECT @BuyerId = Id
FROM Buyers
WHERE FName = 'ИмяПокупателя';

IF @BuyerId IS NULL
BEGIN
    INSERT INTO Buyers (FName, Email, Phone)
    VALUES ('ИмяПокупателя', 'email_покупателя', 'телефон_покупателя');

    SET @BuyerId = SCOPE_IDENTITY();
END;
