using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAD_Assignment_3.Models
{
    public class PostRepository
    {
        private string connString;
        public PostRepository()
        {
            connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EAD_Ass_3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }
        public string AddPost(Post p,int id)
        {
            string check;
            SqlConnection connection = new SqlConnection(connString);
            string query = $"insert into Post (title,content,userId) values(@t,@c,@i)";
            SqlParameter p1 = new SqlParameter("t", p.Title);
            SqlParameter p2 = new SqlParameter("c", p.Content);
            SqlParameter p3 = new SqlParameter("i", id);
            SqlCommand cmd1 = new SqlCommand(query, connection);
            cmd1.Parameters.Add(p1);
            cmd1.Parameters.Add(p2);
            cmd1.Parameters.Add(p3);
            connection.Open();
            int insertedRows = cmd1.ExecuteNonQuery();
            if (insertedRows >= 1)
            {
                check = "post added";
            }
            else
            {
                check = "post not added";
            }
            connection.Close();
            return check;

        }

        public List<Post> getAllPosts()
        {
            List<Post> posts = new List<Post>();
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Post";
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader data = cmd.ExecuteReader();
            //int insertedRows = cmd.ExecuteNonQuery();
            if (data.HasRows)
            {
                while (data.Read())
                {
                    Post post = new Post();
                    post.Id = System.Convert.ToInt32(data.GetValue(0).ToString());
                    post.Title = data.GetValue(1).ToString();
                    post.Content = data.GetValue(2).ToString();
                    post.UserId = System.Convert.ToInt32(data.GetValue(3).ToString());
                    posts.Add(post);
                }
            }
            connection.Close();
            return posts;
        }
        public bool deletePost(int? id)
        {
            SqlConnection connection = new SqlConnection(connString);
            string query = $"delete from Post where Id=@i";
            SqlParameter p1 = new SqlParameter("i", id);
            //SqlParameter p2 = new SqlParameter("p", pwd);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.Add(p1);
            // cmd.Parameters.Add(p2);
            connection.Open();
            if (cmd.ExecuteNonQuery()==1)
            {
                return true;
            }
            //.ExecuteNonQuery();
            connection.Close();
            return false;
        }
        public Post getPost(int? id)
        {
            Post user = new Post();
            SqlConnection connection = new SqlConnection(connString);
            string query = $"select * from Post where userId=@i";
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
                    user.Title = data.GetValue(1).ToString();
                    user.Content = data.GetValue(2).ToString();
                }
            }
            connection.Close();
            return user;
        }

        public bool updatePost(Post p)
        {
            SqlConnection connection = new SqlConnection(connString);
            string query = $"update Post set title=@n, content=@e where Id=@i";
            SqlParameter p1 = new SqlParameter("i", p.Id);
            SqlParameter p2 = new SqlParameter("n", p.Title);
            SqlParameter p3 = new SqlParameter("e", p.Content);
            SqlCommand cmd1 = new SqlCommand(query, connection);
            cmd1.Parameters.Add(p1);
            cmd1.Parameters.Add(p2);
            cmd1.Parameters.Add(p3);
            connection.Open();
            int insertedRows = cmd1.ExecuteNonQuery();
            if (insertedRows >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
