using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Customers.Models
{
    public class CRUD
    {
        private string _connectionString = @"Data Source=IN-B2WMGK3\SQLEXPRESS;Initial Catalog=paginationDb;User Id=sa;Password=sa";

        public DataTable GetAllEmployee()
        {
            DataTable dataTable = new DataTable();
            
            using(SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Customers", sqlConnection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }
        public DataTable GetEmployeeById(int id)
        {
            DataTable dataTable = new DataTable();

            // string _connectionString = @"Data Source=IN-B2WMGK3\SQLEXPRESS;Initial Catalog=paginationDb;User Id=sa; Password=sa";
            
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("Select * from Customers where Id=" + id, sqlConnection);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataTable);
            }

            return dataTable;
        }

        public int CreateEmployee(string FirstName, string LastName, string Contact, string Email)
        {
            // string strConString = @"Data Source=IN-B2WMGK3\SQLEXPRESS;Initial Catalog=paginationDb;User Id=sa; Password=sa";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                string query = "INSERT INTO Customers(FirstName,LastName,Contact, Email) VALUES (@firstname, @lastname , @contact, @email)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@firstname", FirstName);
                cmd.Parameters.AddWithValue("@lastname", LastName);
                cmd.Parameters.AddWithValue("@contact", Contact);
                cmd.Parameters.AddWithValue("@email", Email);
                return cmd.ExecuteNonQuery();
            }
        }
        public int UpdateEmployee(Employee employee)
        {
            //string strConString = @"Data Source=IN-B2WMGK3\SQLEXPRESS;Initial Catalog=paginationDb;User Id=sa;Password=sa";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = "Update Customers SET FirstName=@firstname, LastName=@lastname , Contact=@contact, Email=@email where Id=@id";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@firstname", employee.FirstName);
                cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                cmd.Parameters.AddWithValue("@contact", employee.Contact);
                cmd.Parameters.AddWithValue("@email", employee.Email);
                cmd.Parameters.AddWithValue("@id", employee.Id);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Delete(int id)
        {
            //string strConString = @"Data Source=IN-B2WMGK3\SQLEXPRESS;Initial Catalog=paginationDb;User Id=sa; Password=sa";

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string query = "Delete from Customers where Id=@id";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            }
        }
    }
    
}