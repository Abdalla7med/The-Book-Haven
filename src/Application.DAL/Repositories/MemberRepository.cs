using Application.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public class MemberRepository:GenericRepository<Member>, IMemberRepository
    {

        public MemberRepository(BookHavenContext _context) : base(_context) { } 
    }
}
