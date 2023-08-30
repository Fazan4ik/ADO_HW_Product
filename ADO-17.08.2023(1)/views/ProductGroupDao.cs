using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ADO_17._08._2023_1_.views
{
    internal class ProductGroupDao
    {
        private readonly SqlConnection _connection;

        public ProductGroupDao(SqlConnection connection)
        {
            _connection = connection;
        }

        public List<ProductGroup> GetWithDelete()
        {
            var ProductGroups = new List<ProductGroup>();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandText = "SELECT * FROM ProductGroups WHERE DeleteDt IS NULL";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string deleteValue = reader["DeleteDt"] == DBNull.Value ? "No" : "Yes";
                            ProductGroups.Add(new ProductGroup
                            {
                                Id = reader.GetGuid(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Picture = reader.GetString(3),
                                Delete = deleteValue,

                            });
                        }
                    }
                }
                return ProductGroups;
            }
            catch
            {
                throw;
            }
        }

        public List<ProductGroup> GetFull()
        {
            var ProductGroups = new List<ProductGroup>();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = _connection;
                    command.CommandText = "SELECT * FROM ProductGroups";
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string deleteValue = reader["DeleteDt"] == DBNull.Value ? "No" : "Yes";
                            ProductGroups.Add(new ProductGroup
                            {
                                Id = reader.GetGuid(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Picture = reader.GetString(3),
                                Delete = deleteValue,

                            });
                        }
                    }
                }
                return ProductGroups;
            }
            catch
            {
                throw;
            }
        }


        public void Add(ProductGroup productGroup)
        {
            using SqlCommand command = new();
            command.Connection = _connection;
            command.CommandText = "INSERT INTO ProductGroups ( Id, Name, Description, Picture ) VALUES (@id, @name, @description, @picture) ";
            command.Prepare();

            command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
            command.Parameters.Add(new SqlParameter("@description", SqlDbType.NText));
            command.Parameters.Add(new SqlParameter("@picture", SqlDbType.NVarChar, 50));

            command.Parameters[0].Value = productGroup.Id;
            command.Parameters[1].Value = productGroup.Name;
            command.Parameters[2].Value = productGroup.Description;
            command.Parameters[3].Value = productGroup.Picture;


            command.ExecuteNonQuery();
        }

        public void Delete(ProductGroup productGroup)
        {
            using SqlCommand command = new();
            command.Connection = _connection;
            command.CommandText = $@"
                UPDATE
                    ProductGroups 
                SET 
                    DeleteDt = CURRENT_TIMESTAMP
                WHERE 
                    Id = '{productGroup.Id}' ";
            command.Prepare();
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            command.Parameters.Add(new SqlParameter("@deletedb", SqlDbType.DateTime));

            command.Parameters[0].Value = productGroup.Id;
            command.Parameters[1].Value = DateTime.Now;

            command.ExecuteNonQuery();
        }

        public void Restore(ProductGroup productGroup)
        {
            using SqlCommand command = new();
            command.Connection = _connection;
            command.CommandText = $@"
                UPDATE
                    ProductGroups 
                SET 
                    DeleteDt = NULL
                WHERE 
                    Id = '{productGroup.Id}' ";
            command.Prepare();
            command.Parameters.Add(new SqlParameter("@id", SqlDbType.UniqueIdentifier));
            command.Parameters.Add(new SqlParameter("@deletedb", SqlDbType.DateTime));

            command.Parameters[0].Value = productGroup.Id;
            command.Parameters[1].Value = DBNull.Value;

            command.ExecuteNonQuery();
        }

        public void Update(ProductGroup productGroup)
        {
            using SqlCommand command = new() { Connection = _connection };
            command.CommandText = @$"UPDATE ProductGroups
                                     SET Name = @name, Description = @description, Picture = @picture WHERE Id = '{productGroup.Id}'";
            command.Prepare();

            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 50));
            command.Parameters.Add(new SqlParameter("@description", SqlDbType.NText));
            command.Parameters.Add(new SqlParameter("@picture", SqlDbType.NVarChar, 50));

            command.Parameters[0].Value = productGroup.Name;
            command.Parameters[1].Value = productGroup.Description;
            command.Parameters[2].Value = productGroup.Picture;

            command.ExecuteNonQuery();
        }

    }
}
