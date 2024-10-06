using AutoMapper;
using System;
using Application.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>()
                .ReverseMap();  // This creates a mapping for both directions

            CreateMap<Member, MemberDto>()
                .ReverseMap();

            CreateMap<Author, AuthorDto>()
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Penalty, PenaltyDto>()
                .ReverseMap();

            CreateMap<Loan, LoanDto>()
                .ReverseMap();


        }

    }
}
