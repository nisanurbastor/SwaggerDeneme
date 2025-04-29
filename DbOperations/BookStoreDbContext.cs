//Temel crud işlemleri yapmak için
using Microsoft.EntityFrameworkCore;

namespace MyApi.DbOperations
{

    //DbContexten kalıtım alması gerekiyor almazsa normal bir classtan farkı yok
    //DbContext entity framework ten geliyor
    public class BookStoreDbContext : DbContext
    {

        //Default constructor oluşumu
        //DbContextOptions<BookStoreDbContext> options → Veritabanı bağlantı ve konfigürasyon bilgilerini taşıyan nesne.
        //base(options) bu bilgileri DbContext base sınıfına iletiyor.
        //base(options) -> BookStoreDbContext sınıfının base sınıfı olan DbContext'e bu options’ı iletiyor. 
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }

        //Entity isimleri tekil Db isimleri çoğul yazılır   
        public DbSet<Book> Books { get; set; }

    }
}