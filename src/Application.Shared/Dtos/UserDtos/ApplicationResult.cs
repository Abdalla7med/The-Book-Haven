using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Shared
{
    public class ApplicationResult
    {
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public object Data { get; set; }

        /// <summary>
        ///  Used as an Object Initializer 
        /// </summary>
        public ApplicationResult() { }
        public ApplicationResult(IdentityResult identityResult)
        {
            Succeeded = identityResult.Succeeded;
            Errors = identityResult.Errors.Select(e => e.Description);
            Data = new object();
        }

        public static ApplicationResult Success(object data = null)
        {
            return new ApplicationResult { Succeeded = true, Data = data };
        }

        public static ApplicationResult Failure(IEnumerable<string> errors)
        {
            return new ApplicationResult { Succeeded = false, Errors = errors };
        }
    }

}
