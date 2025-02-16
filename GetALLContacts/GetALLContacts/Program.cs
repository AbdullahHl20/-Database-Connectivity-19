using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GetALLContacts
{
    internal class Program
    {
        public static string connectionString = @"server=DTERMINAL105\MSSQLSERVER2022;User Id=SSDEV\abdullah;database=ContactsDB;Integrated Security=True;";
       

        static void PrintAllContacts(string FirstName)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Contacts where FirstName = @FirstName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int contactID = (int)reader["ContactID"];
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    string email = (string)reader["Email"];
                    string phone = (string)reader["Phone"];
                    string address = (string)reader["Address"];
                    int countryID = (int)reader["CountryID"];

                    Console.WriteLine($"Contact ID: {contactID}");
                    Console.WriteLine($"Name: {firstName} {lastName}");
                    Console.WriteLine($"Email: {email}");
                    Console.WriteLine($"Phone: {phone}");
                    Console.WriteLine($"Address: {address}");
                    Console.WriteLine($"Country ID: {countryID}");
                    Console.WriteLine();
                }

                reader.Close();
                connection.Close();

            }


            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }


        }

        static void Main(string[] args)
        {
            PrintAllContacts("John");
            Console.ReadKey();
        }
    }
}
