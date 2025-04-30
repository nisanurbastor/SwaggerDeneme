using AutoMapper;
using MyApi;
using MyApi.DbOperations;

namespace MyApi.BookOperations.CreateBook;

public class CreateBookCommand
{
    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle(){
        var book = _dbContext.Books.Where(b => b.Title == Model.Title).SingleOrDefault();
        if(book is not null) 
            throw new InvalidOperationException("Bu kitap zaten mevcut.");

        book = _mapper.Map<Book>(Model);
        
         /*book = new Book();
        book.Title = Model.Title;
        book.PageCount = Model.PageCount;
        book.PublishDate = Model.PublishDate;
        book.GenreId = Model.GenreId; */

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}

//UI'da görüntülemeyeceğiz bu yüzden viewmodel olarak tanımlamıyoruz
public class CreateBookModel
{
    public string? Title { get; set; }
    public int GenreId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
}
