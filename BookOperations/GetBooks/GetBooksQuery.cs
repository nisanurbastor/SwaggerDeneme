using System;
using System.Collections.Immutable;
using MyApi.Common;
using MyApi;
using MyApi.DbOperations;
using AutoMapper;

namespace MyApi.BookOperations.GetBooks;

public class GetBooksQuery
{

    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    //Asıl işi yapacak method
    public List<BooksViewModel> Handle(){
        var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
        var vm = _mapper.Map<List<BooksViewModel>>(bookList);
        return vm;
        /* 
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
        }  */
    }
}


//Book ları ekranda yani uı da görmek istediğimiz için viewmodel kullanıyoruz
public class BooksViewModel{
    public string? Title { get; set; }
    public int PageCount { get; set; }
    public string? PublishDate { get; set; }
    public string? Genre { get; set; }
}
