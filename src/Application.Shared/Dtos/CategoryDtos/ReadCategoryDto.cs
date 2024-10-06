using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared   
{
    public class ReadCategoryDto
    {
        public string Name { set; get; }
        public List<string> Books { set; get; }

        public bool IsDeleted {  set; get; }    
    }
}
