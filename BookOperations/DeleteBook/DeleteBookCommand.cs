using System;
using MyApi.DbOperations;

namespace MyApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public int BookId { get; set; }
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.Where(b => b.Id == BookId).SingleOrDefault();
        if(book is null)
            throw new InvalidOperationException("Bu Id ile eşleşen herhangi bir kitap yoktur.");
        
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();
    }
}
