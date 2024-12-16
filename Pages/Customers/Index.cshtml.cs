using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace dotnet.Pages.Customers
{
    public class Index : PageModel
    {
        public List<CustomerInfo> CustomerList { get; set; } = [];
        public void OnGet()
        {
            try
            {
              string connectionString = "Server=DESKTOP-06PG4EJ\\SQLEXPRESS;Database=crmdb;Trusted_Connection=True;";

                using SqlConnection connection = new(connectionString);
                connection.Open();

                string sql = "SELECT * FROM customers2";

                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CustomerInfo customerInfo = new()
                    {
                        Id = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        Phone = reader.GetString(4),
                        Address = reader.GetString(5),
                        Company = reader.GetString(6),
                        Notes = reader.GetString(7),
                        CreatedAt = reader.GetDateTime(8).ToString("MM/dd/yyyy")
                    };

                    CustomerList.Add(customerInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public class CustomerInfo {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Address { get; set; } = "";
        public string Company { get; set; } = "";
        public string Notes { get; set; } = "";
        public string CreatedAt { get; set;} = "";
    }
}