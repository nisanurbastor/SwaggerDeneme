using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using myApi.BookOperations.CreateBook;
using myApi.BookOperations.DeleteBook;
using myApi.BookOperations.GetBookDetail;
using myApi.BookOperations.UpdateBook;
using MyApi.BookOperations.CreateBook;
using MyApi.BookOperations.DeleteBook;
using MyApi.BookOperations.GetBookDetail;
using MyApi.BookOperations.GetBooks;
using MyApi.BookOperations.UpdateBook;
using MyApi.DbOperations;

namespace MyApi.Controllers
{

    [ApiController]

    //Gelen requesti hangi resource un karşılayacağını route ile belirleriz
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        //_context bu kodda kullanacağım instance, context de inject edilen
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //bu dbgenerator da var artık
        /* private static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id=1,
                Title="Herland",
                GenreId = 2, // Science Fiction
                PageCount = 200,
                PublishDate = new DateTime (2010,06,12)
            },
            new Book{
                Id=2,
                Title="Dune",
                GenreId = 2,
                PageCount = 540,
                PublishDate = new DateTime (2001,06,12)
            }
        }; */


        //-------GET-------
        // tüm listeyi veya seçilen id ye göre elemanı getirir
        [HttpGet]
        public IActionResult GetBooks()
        {
            //bu kod statik booklist için yazılmıştı hepsini düzeltelim
            //var bookList = BookList.OrderBy(b => b.Id).ToList<Book>();

            //GetBooksQuery kodundan önceki hali
            /* var bookList = _context.Books.OrderBy(b => b.Id).ToList<Book>();
            return bookList; */

            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            //GetBookDetail kodundan önce kullanılan kod
            /* var book = _context.Books.Where(b => b.Id == id).SingleOrDefault();
            if (book == null)
                return NotFound(); // 404 HTTP response
            return Ok(book); // 200 HTTP response + book verisi */

            GetBookDetailViewModel result;

            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }


        //parametresiz get fonksiyonu bir tane olabilir
        //bunun çalışması için diğer parametresiz get fonksiyonunun kapatılması gerekiyor

        /* [HttpGet]
        public Book Get([FromQuery] string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToUInt32(id)).SingleOrDefault();
            return book;
        } */


        //-----POST-----
        // post ile listeye eleman eklemesi yapılır
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            try
            {
                command.Model = newBook;
                //Validasyonu burda yapmalıyız çünkü eğer doğrulamada sorun varsa kodun handle methodunu hiç çalıştırmamasını istiyoruz.
                CreateBookCommandValidator  validator = new CreateBookCommandValidator();
                //aşağıda foreachle console a yazdırdığımızı exception olaral fırlatıyor
                validator.ValidateAndThrow(command);
                command.Handle();
                //result da hata varsa yani null değilse

                /*
                ValidationResult result =  validator.Validate(command); 
                if(!result.IsValid)
                    foreach (var item in result.Errors)
                        Console.WriteLine("Özellik : " + item.PropertyName +  " - Error message : " + item.ErrorMessage);
                else
                    command.Handle(); */            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        //-----PUT------
        // put ile seçilen bir eleman düzenlenebilir
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookCommandModel updatedBook)
        {

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

            //Frombody den Book u alıyor
            /* var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book is null)
                return BadRequest();

            //defaulttan farklıysa book.GenreId = updatedBook.GenreId olsun değilse book.GenreId olsun
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok(); */
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                command.BookId = id;
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

            //parametre olarak girilen id ile _context.Books taki Id eşit ise bunu book'a at
            /* var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book is null)
                return BadRequest();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok(); */
        }

    }
}