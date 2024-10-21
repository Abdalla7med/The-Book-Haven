using AutoMapper;
using Application.DAL;
using Application.Shared;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Book mappings
        CreateMap<Book, ReadBookDto>()
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
        .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.AvailableCopies))
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
        .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
        .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName + " " + a.LastName).ToList()))
        .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverUrl))
        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
        .ReverseMap();

        CreateMap<CreateBookDto, Book>().ReverseMap();
        CreateMap<UpdateBookDto, Book>().ReverseMap();

        // Penalty mappings
        CreateMap<Penalty, ReadPenaltyDto>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FirstName + " " + src.Member.LastName))
            .ReverseMap();
        CreateMap<CreatePenaltyDto, Penalty>().ReverseMap();
        CreateMap<UpdatePenaltyDto, Penalty>().ReverseMap();

        // Loan mappings
        CreateMap<Loan, ReadLoanDto>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FirstName + " " + src.Member.LastName))
            .ReverseMap();
        CreateMap<CreateLoanDto, Loan>().ReverseMap();
        CreateMap<UpdateLoanDto, Loan>().ReverseMap();

        // Category mappings
        CreateMap<Category, ReadCategoryDto>()
            .ForMember(catDto => catDto.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(catDto => catDto.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(catDto => catDto.Books, opt => opt.MapFrom(src => src.Books))
            .ReverseMap();

        CreateMap<CreateCategoryDto, Category>()
            .ForMember(Category => Category.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        CreateMap<UpdateCategoryDto, Category>()
            .ForMember(Category => Category.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(Category => Category.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ReverseMap();

        // ApplicationUser mappings
        CreateMap<ApplicationUser, ReadUserDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Role , opt => opt.MapFrom(src=> src.Role))
            .ForMember(dest => dest.Loans, opt => opt.MapFrom(src => src.Loans))
            .ForMember(dest => dest.BooksAuthored, opt => opt.MapFrom(src => src.BooksAuthored))
            .ForMember(dest => dest.Penalties, opt => opt.MapFrom(src => src.Penalties))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ReverseMap();

        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Password should be hashed manually
            .ReverseMap();

        CreateMap<UpdateUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ReverseMap();

        CreateMap<LoginUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ReverseMap();
    }
}
