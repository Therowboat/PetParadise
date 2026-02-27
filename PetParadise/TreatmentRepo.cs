using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetParadise
{
    public class TreatmentRepo
    {
        private readonly string ConnectionString;
        private List<Treatment> treatments = new List<Treatment>();

        public TreatmentRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("jsconfig1.json").Build();
            treatments = new List<Treatment>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public int Add(Treatment treatment)
        {
            // Add new treatment to database and to repository
            // Return the database id of the treatment

            int result = -1;

            // IMPLEMENT THIS!

            return result;
        }
        public List<Treatment> GetAll()
        {
            List<Treatment> result = null;

            List<Treatment> treatments = new List<Treatment>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("SELECT TreatmentId, Service, Date, Charge FROM Treatment", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Treatment treatment = new Treatment(dr.GetInt32(0))
                        {
                            Service = (string)dr["Service"],
                            Date = DateOnly.FromDateTime((DateTime)dr["Date"]),
                            Charge = (double)dr["Charge"]
                        };
                        treatments.Add(treatment);
                    }
                }
            }

            return treatments;
        }
        public Treatment GetById(int id)
        {
            {
                Treatment? treatment = null;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT TreatmentId, Service, Date, Charge FROM Treatment WHERE TreatmentId = @TreatmentId", con);
                    cmd.Parameters.Add("@TreatmentId", SqlDbType.Int).Value = id;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            treatment = new Treatment(dr.GetInt32(0))
                            {
                                TreatmentId = dr.GetInt32(0),
                                Service = (string)dr["Service"],
                                Date = DateOnly.FromDateTime((DateTime)dr["Date"]),
                                Charge = (double)dr["Charge"]

                            };
                        }
                    }

                }
                return treatment;
            }
        }
        public void Update(Treatment treatment)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Treatment SET Service = @Service, Date = @Date, Charge = @Charge FROM Treatment WHERE TreatmentId = @TreatmentId", con);
                cmd.Parameters.Add("@Service", SqlDbType.NVarChar).Value = treatment.Service;
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = treatment.Date;
                cmd.Parameters.Add("@Charge", SqlDbType.Int).Value = treatment.Charge;
                cmd.Parameters.Add("@TreatmentId", SqlDbType.Int).Value = treatment.TreatmentId;
                cmd.ExecuteNonQuery();
            }
        }
        public void Remove(Treatment treatment)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Treatment WHERE TreatmentId = @TreatmentId", con);
                cmd.Parameters.AddWithValue("@TreatmentId", treatment); cmd.ExecuteNonQuery();
            }
        }
    }
}
