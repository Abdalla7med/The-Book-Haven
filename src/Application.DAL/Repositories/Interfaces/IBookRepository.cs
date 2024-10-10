using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DAL.Repositories
{
    public interface IBookRepository:IRepository<Book>
    {

        /// Book Searching Methods 
        /// Search By Name
        Task<Book> GetBookByNameAsync(string BookName);
        Task<Book> GetBooksByCategoryAsync(string CategoryName);
        /// to Get Book by Author this's the Author Repository responsibility
        
    }
}
