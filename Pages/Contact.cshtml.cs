using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;


namespace BestShop.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "The First Name is required")]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; } = "";
        [BindProperty]

        [Required(ErrorMessage = "The Last Name is required")]
        [Display(Name = "Last Name*")]
        public string LastName { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "The Email  is required")]
        [Display(Name = "Email*")]
        public string Email { get; set; } = "";
        [BindProperty]
        public string? Phone { get; set; } = "";
        [BindProperty, Required]
        [Display(Name = "Subject*")]
        public string Subject { get; set; } = "";
        [BindProperty]
        [Required(ErrorMessage = "Message is required")]
        [MinLength(5, ErrorMessage = "The Message should be atleast 5 characters")]
        [MaxLength(1024, ErrorMessage = "The Message should be atleast 1024 characters")]
        [Display(Name = "Message*")]
        public string Message { get; set; } = "";


        public List<SelectListItem> SubjectList { get; } = new List<SelectListItem> {

                new SelectListItem { Value = "Order Status", Text = "Order Status" },
                new SelectListItem { Value = "Refund Request", Text = "Refund Request" },
                new SelectListItem { Value = "Job Application", Text = "Job Application" },
                new SelectListItem { Value = "Other", Text = "Other" },

            };
        public string SuccessMessage { get; set; } = "";
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Please fill in all fields.";
                return;
            }

            if (Phone == null) Phone = "";
            // Add this message to the database

            try
            {
                string connectionString = "Data Source=DB7beni\\SQLEXPRESS;Initial Catalog=bestshop;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO messages (firstName, lastName, email, phone, subject, message) VALUES (@firstName, @lastName, @email, @phone, @subject, @message)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@firstName", FirstName);
                        command.Parameters.AddWithValue("@lastName", LastName);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@phone", Phone);
                        command.Parameters.AddWithValue("@subject", Subject);
                        command.Parameters.AddWithValue("@message", Message);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            ErrorMessage = "There was an error sending your message. Please try again later.";
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            // Send Confirmation Email to the client

            SuccessMessage = "Your message has been sent successfully. We will get back to you as soon as possible.";

            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
            Subject = "";
            Message = "";
            ModelState.Clear();
        }
    }
}
