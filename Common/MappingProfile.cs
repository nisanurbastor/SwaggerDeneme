using AutoMapper;
using MyApi.BookOperations.CreateBook;
using MyApi.BookOperations.GetBookDetail;
using MyApi.BookOperations.GetBooks;

namespace MyApi.Common;


//Mapper Konfigürasyonu için Profile sınıfından kalıtım alan aşağıdaki gibi bir sınıf implemente etmemiz gerekir.
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateBookModel den Book'a dönüş yapabilsin
        // < source , target >
        CreateMap<CreateBookModel, Book>();
        CreateMap<Book, GetBookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
                                                 .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));;
        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
                                         .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));
 
    }
}