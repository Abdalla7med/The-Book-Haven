using AutoMapper;
using System;
using Application.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;

using AutoMapper;

public class MappingProfile : Profile
{
    /// <summary>
    ///  Just Mapp Read Dto, and we'll map every thing else 
    /// </summary>
    public MappingProfile()
    {
        // Book mappings
        CreateMap<Book, ReadBookDto>()
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.ISBN, opt => opt.MapFrom(src => src.ISBN))
        .ForMember(dest => dest.AvailableCopies, opt => opt.MapFrom(src => src.AvailableCopies))
        .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
        .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
        .ForMember(dest => dest.AuthorNames, opt => opt.MapFrom(src => src.Authors.Select(a => a.FirstName +" "+ a.LastName).ToList()))
        .ForMember(dest => dest.CoverUrl, opt => opt.MapFrom(src => src.CoverUrl))
        .ForMember(dest => dest.IsDeleted , opt => opt.MapFrom(src => src.IsDeleted))
        .ReverseMap();

        // will map this manually 
        CreateMap<CreateBookDto, Book>()
             .ReverseMap();

        CreateMap<UpdateBookDto, Book>()
            .ReverseMap();

        // Penalty mappings
        CreateMap<Penalty, ReadPenaltyDto>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.FirstName+" "+src.Member.LastName))
            .ReverseMap();
        CreateMap<CreatePenaltyDto, Penalty>().ReverseMap();
        CreateMap<UpdatePenaltyDto, Penalty>().ReverseMap();

        // Loan mappings
        CreateMap<Loan, ReadLoanDto>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src =>src.Member.FirstName+" "+ src.Member.LastName))
            .ReverseMap();
        CreateMap<CreateLoanDto, Loan>().ReverseMap();
        CreateMap<UpdateLoanDto, Loan>().ReverseMap();

        // Category mappings
        CreateMap<Category, ReadCategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>().ReverseMap();
        CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        // ApplicationUser mappings
        CreateMap<ApplicationUser, ReadUserDto>().ReverseMap();
        CreateMap<UpdateUserDto, ApplicationUser>().ReverseMap();
        CreateMap<CreateUserDto, ApplicationUser>().ReverseMap();
    }
}

