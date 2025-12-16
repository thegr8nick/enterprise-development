using AutoMapper;
using Library.Application.Contracts.BookIssues;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;
using Library.Application.Contracts.Readers;
using Library.Domain.Models;

namespace Library.Application;
/// <summary>
/// Профиль AutoMapper для сопоставления доменных сущностей и DTO библиотечного приложения
/// </summary>
public class LibraryProfile : Profile
{
    /// <summary>
    /// Инициализирует правила сопоставления сущностей и DTO для операций получения и создания или обновления
    /// </summary>
    public LibraryProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookCreateUpdateDto, Book>();

        CreateMap<BookIssue, BookIssueDto>();
        CreateMap<BookIssueCreateUpdateDto, BookIssue>();

        CreateMap<EditionType, EditionTypeDto>();
        CreateMap<EditionTypeCreateUpdateDto, EditionType>();

        CreateMap<Publisher, PublisherDto>();
        CreateMap<PublisherCreateUpdateDto, Publisher>();

        CreateMap<Reader, ReaderDto>();
        CreateMap<ReaderCreateUpdateDto, Reader>();
    }
}