using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EAD_Assignment_3.Models;
using Microsoft.Data.SqlClient;
namespace EAD_Assignment_3.Models
{
    public class UserRepository
    {
        private string connString;
        public  UserRepository()
        {
            connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EAD_Ass_3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public string AddUser(User u)
        {
            string check;
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Users where name = @u";
            SqlParameter p1 = new SqlParameter("u", u.Name);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                check = "user exist";
            }
            else
            {
                connection.Close();
                if(u.Password.Equals(u.Retype_Password))
                {
                    string query1 = $"insert into Users (name, email,password,passwordMatch) values(@u,@e,@p,@pM)";
                    SqlParameter p3 = new SqlParameter("u", u.Name);
                    SqlParameter p4 = new SqlParameter("e", u.Email);
                    SqlParameter p5 = new SqlParameter("p", u.Password);
                    SqlParameter p6 = new SqlParameter("pM", u.Retype_Password);
                    SqlCommand cmd1 = new SqlCommand(query1, connection);
                    cmd1.Parameters.Add(p3);
                    cmd1.Parameters.Add(p4);
                    cmd1.Parameters.Add(p5);
                    cmd1.Parameters.Add(p6);
                    connection.Open();
                    int insertedRows = cmd1.ExecuteNonQuery();
                    if (insertedRows >= 1)
                    {
                        check = "user added";
                    }
                    else
                    {
                        check = "user not added";
                    }
                }
                else
                {
                    check = "password not matched";
                }

            }
            connection.Close();
            return check;
        }
        public (string check,int id) SignInUser(User1 u)
        {
            string check;
            int id = 0 ;
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Users where name = @u and password=@p";
            SqlParameter p1 = new SqlParameter("u", u.Name);
            SqlParameter p2 = new SqlParameter("p", u.Password);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            while(data.Read())
            {
                id = System.Convert.ToInt32(data.GetValue(0).ToString());
            }

            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                if(u.Name=="admin")
                {
                    check = "admin";
                }
                else
                {
                    check = "user exist";
                }

            }
            else
            {
                check = "user not exist";
            }
            connection.Close();
            return (check,id);
        }
        public bool deleteUser(int id)
        {
            SqlConnection connection = new SqlConnection(connString);
            string query = $"delete from Users where Id=@i";
            SqlParameter p1 = new SqlParameter("i", id);
            //SqlParameter p2 = new SqlParameter("p", pwd);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            // cmd.Parameters.Add(p2);
            connection.Open();
            if (cmd.ExecuteNonQuery() == 1)
            {
                return true;
            }
            //.ExecuteNonQuery();
            connection.Close();
            return false;
        }
        public List<User> getAllUsers()
        {
            List<User> users = new List<User>();
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Users";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    User user = new User();
                    user.Id = System.Convert.ToInt32(data.GetValue(0).ToString());
                    user.Name = data.GetValue(1).ToString();
                    user.Email = data.GetValue(2).ToString();
                    user.Password = data.GetValue(3).ToString();
                    users.Add(user);
                }
            }
            connection.Close();
            return users;
        }
        public User getUser(int? id)
        {
            User user = new User();
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Users where Id=@i";
            SqlParameter p1 = new SqlParameter("i", id);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    user.Id = System.Convert.ToInt32(data.GetValue(0).ToString());
                    user.Name = data.GetValue(1).ToString();
                    user.Email = data.GetValue(2).ToString();
                    user.Password = data.GetValue(3).ToString();
                    user.Retype_Password = data.GetValue(4).ToString();
                }
            }
            connection.Close();
            return user;
        }

        public int getId(string name)
        {
            int id = 0;
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select Id from Users where name=@n";
            SqlParameter p1 = new SqlParameter("n", name);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    id= System.Convert.ToInt32(data.GetValue(0).ToString());
                }
            }
            connection.Close();
            return id;
        }
        public bool updateUser(User u)
        {
            SqlConnection connection = new SqlConnection(connString);
            string query1 = $"update Users set name=@n, email=@e,password=@p,passwordMatch=@pM where Id=@i";
            SqlParameter p2 = new SqlParameter("i", u.Id);
            SqlParameter p3 = new SqlParameter("n", u.Name);
            SqlParameter p4 = new SqlParameter("e", u.Email);
            SqlParameter p5 = new SqlParameter("p", u.Password);
            SqlParameter p6 = new SqlParameter("pM", u.Retype_Password);
            //SqlParameter p2 = new SqlParameter("p", pwd);
            SqlCommand cmd1 = new SqlCommand(query1, connection);
            cmd1.Parameters.Add(p2);
            cmd1.Parameters.Add(p3);
            cmd1.Parameters.Add(p4);
            cmd1.Parameters.Add(p5);
            cmd1.Parameters.Add(p6);
            // cmd.Parameters.Add(p2);
            connection.Open();
            if (cmd1.ExecuteNonQuery() == 1)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }
    }
}
