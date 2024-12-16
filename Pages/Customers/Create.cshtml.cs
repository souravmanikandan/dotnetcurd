using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace dotnet.Pages.Customers
{
    public class Create : PageModel
    {
        [BindProperty, Required(ErrorMessage = "The First Name is required")]
        public string FirstName { get; set; } = "";
        [BindProperty, Required(ErrorMessage = "The First Name is required")]
        public string LastName { get; set; } = "";
        [BindProperty, Required, EmailAddress]
        public string Email { get; set; } = "";
        [BindProperty]
        public string? Phone { get; set; }
        [BindProperty, Required]
        public string Company { get; set; } = "";
        [BindProperty]
        public string? Address { get; set; }
        [BindProperty]
        public string? Notes { get; set; }
        public void OnGet()
        {
        }
        public string ErrorMessage {get; set;} = "";
        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            if (Phone == null) Phone = "";
            if (Address == null) Address = "";
            if (Notes == null) Notes = "";

            try
            {
               string connectionString = "Server=DESKTOP-06PG4EJ\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;";


               using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();

                string sql = "INSERT INTO customers" +
                    "(firstName,lastName,email,phone,address,company,notes) VALUES"+
                    "(@firstname,@lastname,@email,@phone,@address,@company,@notes);";

                using (SqlCommand command = new SqlCommand(sql, connection)) {
                    command.Parameters.AddWithValue("@firstname", FirstName);
                    command.Parameters.AddWithValue("@lastname", LastName);
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@phone", Phone);
                    command.Parameters.AddWithValue("@address", Address);
                    command.Parameters.AddWithValue("@company", Company);
                    command.Parameters.AddWithValue("@notes", Notes);

                    command.ExecuteNonQuery();
                }
               }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }

            Response.Redirect("/customers/Index");
        }
    }
}