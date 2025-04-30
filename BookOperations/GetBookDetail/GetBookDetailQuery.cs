using Microsoft.AspNetCore.Http.HttpResults;
using MyApi.Common;
using MyApi;
using MyApi.DbOperations;
using AutoMapper;

namespace MyApi.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    public int BookId { get; set; }
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GetBookDetailViewModel Handle(){
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);

        if(book is null)
            throw new InvalidOperationException("Bu Id ile eşleşen kitap yoktur..");

        GetBookDetailViewModel vm = _mapper.Map<GetBookDetailViewModel>(book);

        
        /* GetBookDetailViewModel vm = new GetBookDetailViewModel();
        vm.Title = book?.Title;
        vm.Genre = ((GenreEnum)book.GenreId).ToString();
        vm.PageCount = book.PageCount;
        vm.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"); */

        return vm;
    }

}

public class GetBookDetailViewModel
{
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public int PageCount { get; set; }
    public string? PublishDate { get; set; }
}
