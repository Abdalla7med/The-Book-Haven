using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class CreateCategoryDto
    {
        [Required]
        [MinLength(2,ErrorMessage = "Category Name Must be Greater Than One Character")]
        [MaxLength(25, ErrorMessage = "Category Name Must be Less Than 25 Characters")]
        public string Name { get; set; }    
    }
}
