using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace PetParadise
{
    public class OwnerRepo
    {
        private readonly string ConnectionString;
        private List<Owner> owners;
        public OwnerRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("jsconfig1.json").Build();
            owners = new List<Owner>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }



        public int Add(Owner owner)
        {
            int result;
            // Add new owner to database and to repository
            // Return the database id of the owner
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); using (SqlCommand cmd = new SqlCommand("INSERT INTO Owner (FirstName, LastName, Phone, Email) " +
                    "VALUES(@FirstName, @LastName, @Phone, @Email)" +
                    "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = owner.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = owner.LastName;
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value =
                owner.Phone == null ? DBNull.Value : owner.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = owner.Email;
                    //owner.OwnerId = Convert.ToInt32(cmd.ExecuteScalar());
                    owners.Add(owner);

                    result = Convert.ToInt32(cmd.ExecuteScalar());
                    owner.OwnerId = result;
                }
            }


            return result;
        }
        public List<Owner> GetAll()
        {

            List<Owner> result = null;

            List<Owner> owners = new List<Owner>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("SELECT OwnerId, FirstName, LastName, Phone, Email FROM Owner", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Owner owner = new Owner(dr.GetInt32(0))
                        {
                            OwnerId = dr.GetInt32(0),
                            FirstName = (string)dr["FirstName"],
                            LastName = (string)dr["LastName"],
                            Phone = dr["Phone"] == DBNull.Value ? null : (string)dr["Phone"],
                            Email = (string)dr["Email"]
                        };
                        owners.Add(owner);
                    }
                }
            }

            return owners;
        }
        public Owner GetById(int id)
        {
            // Get owner by id from 
            {
                Owner? owner = null;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT OwnerId, FirstName, LastName, Phone, Email FROM Owner WHERE OwnerId = @OwnerId", con);
                    cmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            owner = new Owner(dr.GetInt32(0))
                            {
                                OwnerId = dr.GetInt32(0),
                                FirstName = (string)dr["FirstName"],
                                LastName = (string)dr["LastName"],
                                Phone = dr["Phone"] == DBNull.Value ? null : (string)dr["Phone"],
                                Email = (string)dr["Email"]
                            };
                        }
                    }

                }
                return owner;
            }
        }
        public void Update(Owner owner)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE OWNER SET FirstName = @FirstName, LastName = @LastName, Phone = @Phone, Email = @EMAIL FROM Owner WHERE OwnerId = @OwnerId", con);
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = owner.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = owner.LastName;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value =
            owner.Phone == null ? DBNull.Value : owner.Phone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = owner.Email;
                cmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = owner.OwnerId;
                cmd.ExecuteNonQuery();
            }
        }
        public void Remove(Owner owner)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Owner WHERE OwnerId = @OwnerId", con);
                cmd.Parameters.Add("@OwnerId", SqlDbType.Int).Value = owner.OwnerId;
                cmd.ExecuteNonQuery();
            }
        }

    }
}
