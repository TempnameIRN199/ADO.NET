using lab_9.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NintendoContext db = new NintendoContext())
            {
                // 1) Список должников
                var rents = db.Clients.Where(c => c.IsDebtor == true).Select(c => new
                {
                    Client = c.FullName,
                    Rents = c.Rents.Select(r => new
                    {
                        Book = r.Book.Name,
                        Author = r.Book.BookAuthors.Select(ba => ba.Author.Name)
                    })
                });

                foreach (var rent in rents)
                {
                    Console.WriteLine($"Клиент: {rent.Client}");

                    foreach (var r in rent.Rents)
                    {
                        Console.WriteLine($"Книга: {r.Book}");

                        foreach (var a in r.Author)
                        {
                            Console.WriteLine($"Автор: {a}");
                        }
                    }
                }

                // 2) Список авторов книги №3
                var authorsOfBook = db.Authors.Where(a => a.BookAuthors.Any(ba => ba.BookId == 3))
                    .Select(a => a.Name).ToList();

                Console.Write("Авторы книги №3:");

                foreach (var author in authorsOfBook)
                {
                    Console.Write($" {author}");
                }

                Console.WriteLine();

                // 3) Список доступных книг
                var availableBooks = db.Books.Where(b => b.IsAvailable == true).Select(b => b.Name).ToList();

                Console.Write("Доступные книги:");

                foreach (var book in availableBooks)
                {
                    Console.Write($" {book}");
                }

                Console.WriteLine();

                // 4) Список книг на руках у посетителя №2
                var booksOdClient = db.Rents.Where(b => b.ClientId == 2).Select(b => b.Book.Name).ToList();

                Console.Write("Книги на руках у посетителя №2: ");

                foreach (var book in booksOdClient)
                {
                    Console.Write(book);
                }

                Console.ReadKey();
            }
        }
    }
}
