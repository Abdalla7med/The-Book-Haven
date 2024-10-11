using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class UpdateCategoryDto
    {
        public Guid CategoryId { set; get; }
        public string Name { set; get; }

        // Admin controls soft-delete
        public bool IsDeleted { get; set; }  // Admin-specific field for soft delete
    }
}
