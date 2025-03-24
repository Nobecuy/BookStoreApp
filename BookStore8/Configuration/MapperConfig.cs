using AutoMapper;
using BookStore8.Data;
using BookStore8.Model.Author;
using BookStore8.Model.Book;
using BookStore8.Model.User;

namespace BookStore8.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AuthorReadOnlyDto, Author>().ReverseMap();
            CreateMap<AuthorCreateDto,Author>().ReverseMap();
            CreateMap<AuthorUpdateDto, Author>().ReverseMap();

            CreateMap<BookUpdateDto, Book>();
            CreateMap<BookCreateDto, Book>().ReverseMap(); 
            CreateMap<Book, BookReadOnlyDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<Book, BookDetailDto>()
                .ForMember(q => q.AuthorName, d => d.MapFrom(map => $"{map.Author.FirstName} {map.Author.LastName}"))
                .ReverseMap();

            CreateMap<ApiUser, UserDto>().ReverseMap();
        }   
    }
}
