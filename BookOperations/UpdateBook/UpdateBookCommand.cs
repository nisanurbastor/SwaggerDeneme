using System;
using MyApi.DbOperations;

namespace myApi.BookOperations.UpdateBook;

public class UpdateBookCommand
{
    private readonly BookStoreDbContext _dbContext;

    public UpdateBookCommandModel Model { get; set; }
    public int BookId { get; set; }
    
    public UpdateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == BookId);
        if (book is null)
            throw new InvalidOperationException("Bu Id ile eşleşen kitap yoktur.");

        book.Title = Model.Title != default ? Model.Title : book.Title;
        book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

        _dbContext.SaveChanges();
    }
}

public class UpdateBookCommandModel
{
    public string? Title { get; set; }
    public int GenreId { get; set; }
}

