go
create proc GetBookCount as
begin
select count (*) as TotalBooks from Books
end
go

go
create proc GetAuthorBooksInfo @authorName nvarchar(max) as begin
declare @TotalPrice decimal(10,2)
declare @TotalPages int
select @TotalPrice = sum(b.Price) from Books as b
join Book_Author as ba on b.Id = ba.BookId
join Authors as a on ba.AuthorId = a.Id
where a.Name = @authorName

select @TotalPages = sum(b.Pages) from Books as b
join Book_Author as ba on b.Id = ba.BookId
join Authors as a on ba.AuthorId = a.Id
where a.Name = @authorName

select @TotalPrice as 'Total Price', @TotalPages as 'Total Pages'
end
go