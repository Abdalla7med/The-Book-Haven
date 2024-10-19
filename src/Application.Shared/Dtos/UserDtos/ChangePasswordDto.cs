using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{

    public class ChangePasswordDto
    {
        public string Email { set; get; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}



