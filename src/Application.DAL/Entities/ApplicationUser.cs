using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class ApplicationUser: IdentityUser
    {
        public string? FName { set; get; }
        public string? LName { set; get; }

    }
}
