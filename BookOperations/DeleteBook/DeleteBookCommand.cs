using System;
using MyApi.DbOperations;

namespace myApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public string BookTitle { get; set; }
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.Where(b => b.Title == BookTitle).SingleOrDefault();
        if(book is null)
            throw new InvalidOperationException("Bu isimde bir kitap yoktur.");
        
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}
