using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestShop.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        [Required(ErrorMessage = "The First Name is required")]
        public string FirstName { get; set; } = "";


        [Required(ErrorMessage = "The Last Name is required")]
        public string LastName { get; set; } = "";


        [Required(ErrorMessage = "The Email is required"), EmailAddress]
        public string Email { get; set; } = "";
        public string? Phone { get; set; } = "";


        [Required(ErrorMessage = "The Address is required")]
        public string Address { get; set; } = "";


        [Required(ErrorMessage = "The Password is required")]
        [StringLength(50, ErrorMessage ="Password must be between 5 and 50 characters",MinimumLength =5)]
        public string Password { get; set; } = "";


        [Required(ErrorMessage = "Comfirm password is required")]
        [Compare("Password",ErrorMessage = "Password and Comfirm Password do not match")]
        public string ComfirmPassword { get; set; } = "";


        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Data validation failed";
                return;
            }

            //successfully data validation

            if (Phone == null) Phone = "";

            // add the user details to the database

            // send comfirmation email to the user

            successMessage = "account created successfully";
        }
    }
}
