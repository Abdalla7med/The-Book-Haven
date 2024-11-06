using Application.DAL;
using Application.Shared;
using AutoMapper;

public class MappingProfile : Profile
{

    public MappingProfile()
    {

        #region  Book mappings
        // Mapping from Book entity to ReadBookDto
        CreateMap<Book, ReadBookDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author != null ? $"{src.Author.FirstName} " : null));

        // Mapping from CreateBookDto to Book entity
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.BookId, opt => opt.Ignore()) // Ignoring BookId, it will be auto-generated
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false)) // Default value for IsDeleted
            .ForMember(dest => dest.Author, opt => opt.Ignore()) // Ignoring Author property, assuming author information will be handled separately
            .ForMember(dest => dest.Loans, opt => opt.Ignore()); // Ignoring Loans collection for creation

        // Mapping from UpdateBookDto to Book entity
        CreateMap<UpdateBookDto, Book>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.AvailableCopies));
        #endregion

        #region Penalty mappings
        // Mapping from Penalty entity to ReadPenaltyDto
        CreateMap<Penalty, ReadPenaltyDto>()
            .ForMember(dest => dest.PenaltyId, opt => opt.MapFrom(src => src.PenaltyId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount ?? 0)) // Default to 0 if null
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member != null ? $"{src.Member.FirstName} {src.Member.LastName}" : string.Empty));

        // Mapping from CreatePenaltyDto to Penalty entity
        CreateMap<CreatePenaltyDto, Penalty>()
            .ForMember(dest => dest.PenaltyId, opt => opt.Ignore()) // PenaltyId is auto-generated
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false)) // Default value for IsDeleted
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId));

        // Mapping from UpdatePenaltyDto to Penalty entity
        CreateMap<UpdatePenaltyDto, Penalty>()
            .ForMember(dest => dest.PenaltyId, opt => opt.MapFrom(src => src.PenaltyId))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

        #endregion

        #region Loan mappings
        // Mapping from Loan entity to ReadLoanDto
        CreateMap<Loan, ReadLoanDto>()
            .ReverseMap();

        // Mapping from CreateLoanDto to Loan entity
        CreateMap<CreateLoanDto, Loan>()
            .ForMember(dest => dest.LoanId, opt => opt.Ignore()) // LoanId is auto-generated
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false)) // Default value for IsDeleted
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => false)) // Default value for IsReturned
            .ForMember(dest => dest.Penalty, opt => opt.Ignore()); // Penalty will be handled separately

        // Mapping from UpdateLoanDto to Loan entity
        CreateMap<UpdateLoanDto, Loan>()
            .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueTime))
            .ForMember(dest => dest.ReturnDate, opt => opt.MapFrom(src => src.ReturnDate))
            .ForMember(dest => dest.IsReturned, opt => opt.MapFrom(src => src.IsReturned))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));
        #endregion

        #region Category
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
        #endregion

        #region AppUser
        // Mapping from ApplicationUser to ReadUserDto
        CreateMap<ApplicationUser, ReadUserDto>()
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
        .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked))
        .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
        .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => src.IsPremium))
        .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        .ReverseMap();

        // Mapping from CreateUserDto to ApplicationUser
        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Id is auto-generated
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Username is the same as Email in Identity
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Password hashing will be handled elsewhere
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role ?? "Member")) // Default role is Member
            .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => src.IsPremium))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageURL))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => false));

        // Mapping from UpdateUserDto to ApplicationUser
        CreateMap<UpdateUserDto, ApplicationUser>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Update username if email changes
            .ForMember(dest => dest.IsPremium, opt => opt.MapFrom(src => src.IsPremium))
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForMember(dest => dest.IsBlocked, opt => opt.MapFrom(src => src.IsBlocked));


        #endregion


    }
}
