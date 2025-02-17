using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static GetALLContacts.Program;
using System.Net;
using System.Security.Policy;
using System.Runtime.InteropServices;

namespace GetALLContacts
{
    internal class Program
    {
        public static string connectionString = @"server=DTERMINAL105\MSSQLSERVER2022;User Id=SSDEV\abdullah;database=ContactsDB;Integrated Security=True;";

        public struct Stcontactinfo
        {
            public int ContactID { set; get; }
            public string FirstName { set; get; }
            public string LastName { set; get; }
            public string Email { set; get; }
            public string Phone { set; get; }
            public string Address { set; get; }
            public int CountryID { set; get; }

        }
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

        static void SercheinFirstNameAllContacts(string FirstName)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Contacts where FirstName like '' + @FirstName  + '%'";

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
        static string ReadFirstNameByContactId(int ContactID)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT FirstName FROM Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();

                object reader = command.ExecuteScalar();

                connection.Close();

                return Convert.ToString(reader);
            }


            catch (Exception ex)
            {

                return "Error: " + ex.Message;
            }

        }

        static bool FindSingleContactById(int ContactID, ref Stcontactinfo contactinfo)
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isfound = true;
                    contactinfo.ContactID = (int)reader["ContactID"];
                    contactinfo.FirstName = (string)reader["FirstName"];
                    contactinfo.LastName = (string)reader["LastName"];
                    contactinfo.Email = (string)reader["Email"];
                    contactinfo.Phone = (string)reader["Phone"];
                    contactinfo.Address = (string)reader["Address"];
                    contactinfo.CountryID = (int)reader["CountryID"];
                }

                reader.Close();
                connection.Close();

            }


            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            return isfound;
        }
        static void AddNewcontactinfo( ref Stcontactinfo contactinfo)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO [Contacts] ([FirstName],[LastName],[Email],[Phone],[Address] ,[CountryID]) VALUES (@FirstName,@LastName,@Email,@Phone,@Address,@CountryID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", contactinfo.FirstName);
            command.Parameters.AddWithValue("@LastName", contactinfo.LastName);
            command.Parameters.AddWithValue("@Email", contactinfo.Email);
            command.Parameters.AddWithValue("@Phone", contactinfo.Phone);
            command.Parameters.AddWithValue("@Address", contactinfo.Address);
            command.Parameters.AddWithValue("@CountryID", contactinfo.CountryID);

            try
            {
                connection.Open();

                if (command.ExecuteNonQuery() > 0)
                    Console.WriteLine($"Added Contact Sucsses ");

                connection.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);

            }            
        }

        static void PrintContactInfo(ref Stcontactinfo contactinfo) 
        {
            Console.WriteLine($"Contact ID: {contactinfo.ContactID}");
            Console.WriteLine($"Name: {contactinfo.FirstName} {contactinfo.LastName}");
            Console.WriteLine($"Email: {contactinfo.Email}");
            Console.WriteLine($"Phone: {contactinfo.Phone}");
            Console.WriteLine($"Address: {contactinfo.Address}");
            Console.WriteLine($"Country ID: {contactinfo.CountryID}");
            Console.WriteLine();
        }

        static void Readcontactinfo(ref Stcontactinfo contactinfo)
        { 

            Console.WriteLine(" Palse Enter FirstName:");
            contactinfo.FirstName = Console.ReadLine();            
            Console.WriteLine(" Palse Enter LastName:  ");
            contactinfo.LastName = Console.ReadLine();            
            Console.WriteLine("Palse Enter Email:");
            contactinfo.Email = Console.ReadLine();
            Console.WriteLine("Palse Enter Phone: ");
            contactinfo.Phone = Console.ReadLine();
            Console.WriteLine("Palse Enter Address: ");
            contactinfo.Address = Console.ReadLine();
            Console.WriteLine("Palse Enter Country ID:");
            contactinfo.CountryID = Convert.ToInt32( Console.ReadLine());
            Console.WriteLine();

        }
        static void Main(string[] args)
        {
 
            Stcontactinfo contactinfo = new Stcontactinfo(); 
            Readcontactinfo( ref contactinfo);
            AddNewcontactinfo(ref  contactinfo);
            PrintContactInfo (ref contactinfo);
            Console.ReadKey();
        }
    }
}
