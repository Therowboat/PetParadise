using System;
using System.Collections.Generic;
using System.Text;

namespace PetParadise
{
    public class Controller
    {
        private OwnerRepo ownerRepo = new OwnerRepo();
        private PetRepo petRepo = new PetRepo();
        private TreatmentRepo treatmentRepo = new TreatmentRepo();

        public List<Owner> GetAllOwners()
        {
            return ownerRepo.GetAll();
        }
        public Owner GetSpecificOwner(int ownerId)
        {
            return ownerRepo.GetById(ownerId);
        }

        public List<Pet> GetAllPets()
        {
            return petRepo.GetAll();
        }
        public Pet GetSpecificPet(int petId)
        {
            return petRepo.GetById(petId);
        }

        public List<Treatment> GetAllTreatments()
        {
            return treatmentRepo.GetAll();
        }
        public Treatment GetSpecificTreatment(int treatId)
        {
            return treatmentRepo.GetById(treatId);
        }
        public int CreateOwner(Owner owner)
        {
            return ownerRepo.Add(owner);
        }
        public void UpdateOwner(Owner owner)
        {
            ownerRepo.Update(owner);
        }
        public void DeleteOwner(Owner owner)
        {
            ownerRepo.Remove(owner);
        }
        public int CreateTreatment(Treatment treatment)
        {
            return treatmentRepo.Add(treatment);
        }
        public void UpdateTreatment(Treatment treatment)
        {
            treatmentRepo.Update(treatment);
        }
        public void DeleteTreatment(Treatment treatment)
        {
            treatmentRepo.Remove(treatment);
        }
        
        
    }

}
