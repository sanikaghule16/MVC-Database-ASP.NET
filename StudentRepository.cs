using MvCDtabase.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvCDtabase.Controllers
{
    public class StudentRepository
    {
        string connString = ConfigurationManager.ConnectionStrings["MVTDBCS"].ConnectionString;

        // READ: Get all students
        public List<Student> GetAll()
        {
            List<Student> list = new List<Student>();
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Student", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    list.Add(new Student
                    {
                        ID = (int)rdr["ID"],
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString(),
               
                    });
                }
            }
            return list;
        }

        // INSERT: Add new student
        public int Insert(Student std)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "INSERT INTO Student VALUES (@FirstName, @LastName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FirstName", std.FirstName);
                cmd.Parameters.AddWithValue("@LastName", std.LastName);
               
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // UPDATE: Edit existing student
        public int Edit(Student std)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                string query = "UPDATE Student SET FirstName=@FN, LastName=@LN WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", std.ID);
                cmd.Parameters.AddWithValue("@FN", std.FirstName);
                cmd.Parameters.AddWithValue("@LN", std.LastName);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // DELETE: Remove student
        public int Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Student WHERE ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }
    }

}
