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
    public MappingProfile()
    {
        // Book mappings
        CreateMap<Book, ReadBookDto>().ReverseMap();
        CreateMap<CreateBookDto, Book>().ReverseMap();
        CreateMap<UpdateBookDto, Book>().ReverseMap(); 

        // Penalty mappings
        CreateMap<Penalty, ReadPenaltyDto>().ReverseMap();
        CreateMap<CreatePenaltyDto, Penalty>().ReverseMap();
        CreateMap<UpdatePenaltyDto, Penalty>().ReverseMap();

        // Loan mappings
        CreateMap<Loan, ReadLoanDto>().ReverseMap();
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

