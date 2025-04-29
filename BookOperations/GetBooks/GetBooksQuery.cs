using System;
using System.Collections.Immutable;
using myApi.Common;
using MyApi;
using MyApi.DbOperations;

namespace myApi.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDbContext _dbContext;
    public GetBooksQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Asıl işi yapacak method
    public List<BooksViewModel> Handle(){
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
        List<BooksViewModel> vm = new List<BooksViewModel>();

        foreach (var item in bookList)
        {
            vm.Add(new BooksViewModel()
            {
                Title = item.Title,
                PageCount = item.PageCount,
                Genre = ((GenreEnum)item.GenreId).ToString(),
                PublishDate = item.PublishDate.Date.ToString("dd/MM/yyy"),
            });
        } 
        return vm;
    }
}


//Book ları ekranda yani uı da görmek istediğimiz için viewmodel kullanıyoruz
public class BooksViewModel{
    public string? Title { get; set; }
    public int PageCount { get; set; }
    public string? PublishDate { get; set; }
    public string? Genre { get; set; }
}
