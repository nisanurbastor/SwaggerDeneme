using System;
using Microsoft.EntityFrameworkCore;
namespace MyApi.DbOperations;

public class DbGenerator
{
    //Initialize(IServiceProvider) → Uygulama ilk başladığında, servislere ulaşarak başlangıç ayarları yapar.
    //program.cs kendi içindeki service provider la bu metodu çağırır
    public static void Initialize(IServiceProvider serviceProvider)
    {

        //serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()
        //Dependency Injection ile DbContextOptions ayarlarını alıyor.(Yani veritabanı bağlantı bilgileri, hangi sağlayıcıyı kullanacağın gibi ayarlar burada.)
        //new BookStoreDbContext(...)→ DbContextOptions’ı kullanarak yeni bir veritabanı bağlamı (context) oluşturuyor.
        using (var context = new BookStoreDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            // Look for any book.
            if (context.Books.Any())
            {
                return;   // Data was already seeded
            }

            context.Books.AddRange(
               new Book()
               {
                   //Id = 1;
                   Title = "Lean Startup",
                   GenreId = 1, // Personal Growth
                   PageCount = 200,
                   PublishDate = new DateTime(2001, 06, 12)
               },
               new Book()
               {
                   Title = "Herland",
                   GenreId = 2, // Science Fiction
                   PageCount = 250,
                   PublishDate = new DateTime(2010, 05, 23)
               },
               new Book()
               {
                   Title = "Dune",
                   GenreId = 2, // Science Fiction
                   PageCount = 540,
                   PublishDate = new DateTime(2001, 12, 21)
               }
               );


            context.SaveChanges();
        }
    }
}
