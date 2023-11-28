using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace zoostore.Utils
{
    public class Class1
    {
        public string GetUserRole()
        {
            using SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["zooStoreDB"].ConnectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                MessageBox.Show("Не удалось подключиться к бд");
            }
            SqlCommand getData = new SqlCommand
            {
                Connection = connection,
                CommandText = $"SELECT [UserRole] FROM [User] WHERE [UserLogin] = 'pixil59@gmail.com' and [UserPassword] = '2L6KZG'"
            };
            string role = "";
            SqlDataReader readerData = getData.ExecuteReader();

            while (readerData.Read())
            {
                role = readerData[0].ToString();
            }
            readerData.Close();
            connection.Close();
            return role;
        }
    }
}
