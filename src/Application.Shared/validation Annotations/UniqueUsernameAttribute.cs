using Application.DAL;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));

        if (userManager != null)
        {
            var userName = value as string;
            if (!string.IsNullOrEmpty(userName))
            {
                // Check if the username already exists
                var user = userManager.FindByNameAsync(userName).Result;
                if (user != null)
                {
                    return new ValidationResult("Username is already taken.");
                }
            }
        }

        return ValidationResult.Success;
    }
}
