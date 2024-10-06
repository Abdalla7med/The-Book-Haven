using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Entities
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
        bool IsBlocked { get; set; }
    }
}
