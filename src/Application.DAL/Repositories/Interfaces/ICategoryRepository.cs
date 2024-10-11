using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        /// <summary>
        /// will be used for case of getting category books, and for check if category exists 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Category> GetCategory(string name);
      

    }
}
