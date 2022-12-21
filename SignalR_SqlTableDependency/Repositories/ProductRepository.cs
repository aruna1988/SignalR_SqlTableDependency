using SignalR_SqlTableDependency.Models;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace SignalR_SqlTableDependency.Repositories
{
    public class ProductRepository
    {
        string connectionString;

        public ProductRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<EmpDetLocal> GetProducts()
        {
            List<EmpDetLocal> products = new List<EmpDetLocal>();
            EmpDetLocal product;

            var data = GetProductDetailsFromDb();

            foreach (DataRow row in data.Rows)
            {
                product = new EmpDetLocal
                {
                    //Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    MobileNo = row["MobileNo"].ToString(),
                    Address = row["Address"].ToString(),
                    
                };
                products.Add(product);
            }

            return products;
        }


        private DataTable GetProductDetailsFromDb()
        {
            var query = "SELECT Name,MobileNo,Address FROM EmpDetLocal";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }

                    return dataTable;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

    }
}
