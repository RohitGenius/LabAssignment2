using LabAssignment2.Models;
using System.Data;
using System.Data.SqlClient;

namespace LabAssignment2.Repository
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string ConnString;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnString = _configuration.GetConnectionString("DbConection");
        }

        public IEnumerable<Product> ListProducts()
        {
            var products = new List<Product>();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_GetAllProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Product product = new Product();
                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Description = rdr["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]);
                    product.Summary = rdr["Summary"].ToString();
                    product.CategoryId = Convert.ToInt32(rdr["CategoryId"]);

                    products.Add(product);
                }
                con.Close();
            }
            return products;
        }

        public IEnumerable<Category> ListCategories()
        {
            var categories = new List<Category>();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_GetAllCategories", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Category category = new Category();
                    category.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                    category.Name = rdr["Name"].ToString();
                    categories.Add(category);
                }
                con.Close();
            }
            return categories;
        }

        public void AddProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_AddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Summary", product.Summary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Product GetProduct(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.Name = rdr["Name"].ToString();
                    product.Description = rdr["Description"].ToString();
                    product.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]);
                    product.Summary = rdr["Summary"].ToString();
                    product.CategoryId = Convert.ToInt32(rdr["CategoryId"]);
                }
                con.Close();
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_UpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", product.ProductId);
                cmd.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                cmd.Parameters.AddWithValue("@Summary", product.Summary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteProduct(int? id)
        {
            using (SqlConnection con = new SqlConnection(ConnString))
            {
                SqlCommand cmd = new SqlCommand("Usp_DeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
