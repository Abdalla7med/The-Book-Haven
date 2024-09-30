using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL
{
    public class Member
    {
        public int MemberId { get; set; }
        [StringLength(25)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }
        [EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Phone, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }

        /// Relations
        public ICollection<Loan> Loans { get; set; }

        public ICollection<Penalty> Penalties { set; get; }

    }
}
