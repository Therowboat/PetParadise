using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PetParadise
{
    public class PetRepo
    {
        private readonly string ConnectionString;
        private List<Pet> pets = new List<Pet>();

        public PetRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("jsconfig1.json").Build();
            pets = new List<Pet>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public int Add(Pet pet)
        {
            // Add new pet to database and to repository
            // Return the database id of the pet

            int result = -1;

            // IMPLEMENT THIS!

            return result;
        }
        public List<Pet> GetAll()
        {
            List<Pet> result = null;

            List<Pet> pets = new List<Pet>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("SELECT PetId, Name, Type, Breed, DateOfBirth, Weight, OwnerId FROM PET", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {   
                        Pet pet = new Pet(dr.GetInt32(0))
                        {
                            Name = (string)dr["Name"],
                            PetType = Enum.Parse<PetType>(dr["Type"] as string),
                            Breed = (string)dr["Breed"],
                            DateOfBirth = DateOnly.FromDateTime((DateTime)dr["DateOfBirth"]),
                            Weight = (double)dr["Weight"]
                        };
                        pets.Add(pet);
                    }
                }
            }

            return pets;
        }
        public Pet GetById(int id)
        {
            {
                Pet? pet = null;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT PetId, Name, Type, Breed, DateOfBirth, Weight FROM Pet WHERE PetId = @PetId", con);
                    cmd.Parameters.AddWithValue("@PetId", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            pet = new Pet(dr.GetInt32(0))
                            {
                                Name = (string)dr["Name"],
                                PetType = Enum.Parse<PetType>(dr["Type"] as string),
                                Breed = (string)dr["Breed"],
                                DateOfBirth = DateOnly.FromDateTime((DateTime)dr["DateOfBirth"]),
                                Weight = (double)dr["Weight"]
                            };
                        }
                    }

                }
                return pet;
            }
        }
        public void Update(Pet pet)
        {
            // Update existing pet on database

            // IMPLEMENT THIS!
        }
        public void Remove(Pet pet)
        {
            // Delete existing pet in database

            // IMPLEMENT THIS!
        }
    }
}
