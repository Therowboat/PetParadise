using PetParadise;

namespace UnitTest1

{
    [TestClass]
    public class UnitTest1
    {
        Controller controller;
        [TestInitialize]
        public void TestInitialize()
        {
            controller = new Controller();
        }

        [TestMethod]
        public void TestGetAllOwnersCheckCount()
        {
            // Arrange
            List<Owner> owners = controller.GetAllOwners();

            // Assert
            Assert.AreEqual(5, owners.Count);
        }
        [TestMethod]
        public void TestGetAllOwnersCheckItem2()
        {
            // Arrange
            List<Owner> owners = controller.GetAllOwners();

            // Assert
            Assert.AreEqual("3: Liz Frier - T: 555 537 6543 - M: Liz.Frier@somewhere.com", owners[2].ToString());
        }
        [TestMethod]
        public void TestGetSpecificOwner3()
        {
            // Arrange
            Owner owner = controller.GetSpecificOwner(5);

            // Assert
            Assert.AreEqual("5: Hilary Evans - T: 210-634-2345 - M: Hilary.Evans@somewhere.com", owner.ToString());
        }
        [TestMethod]
        public void TestGetAllPetsCheckCount()
        {
            // Arrange
            List<Pet> pets = controller.GetAllPets();

            // Assert
            Assert.AreEqual(8, pets.Count);
        }
        [TestMethod]
        public void TestGetAllPetsCheckItem6()
        {
            // Arrange
            List<Pet> pets = controller.GetAllPets();

            // Assert
            Assert.AreEqual("7: Buster, Dog, Border Collie, 11-12-2008 00:00:00, 25", pets[6].ToString());
        }
        [TestMethod]
        public void TestGetSpecificPet7()
        {
            // Arrange
            Pet pet = controller.GetSpecificPet(7);

            // Assert
            Assert.AreEqual("7: Buster, Dog, Border Collie, 11-12-2008 00:00:00, 25", pet.ToString());
        }
        [TestMethod]
        public void TestGetAllTreatmentsCheckCount()
        {
            // Arrange
            List<Treatment> treatments = controller.GetAllTreatments();

            // Assert
            Assert.AreEqual(9, treatments.Count);
        }
        [TestMethod]
        public void TestGetAllTreatmentsCheckItem2()
        {
            // Arrange
            List<Treatment> treatments = controller.GetAllTreatments();

            // Assert
            Assert.AreEqual("3: Parasites on 13-10-2014 00:00:00 costs 42", treatments[2].ToString());
        }
        [TestMethod]
        public void TestGetSpecificTreatment7()
        {
            // Arrange
            Treatment treatment = controller.GetSpecificTreatment(7);

            // Assert
            Assert.AreEqual("7: Skin lnfection on 03-10-2014 00:00:00 costs 35", treatment.ToString());
        }

        [TestMethod]
        public void TestOwner_Add_Update_Remove_ViaController()
        {
            // ADD
            Owner o = new Owner(0)
            {
                FirstName = "Unit",
                LastName = "TestOwner",
                Phone = null,
                Email = "unit.owner@test.dk"
            };

            int ownerId = controller.CreateOwner(o);
            Assert.IsTrue(ownerId > 0);

            Owner? created = controller.GetSpecificOwner(ownerId);
            Assert.IsNotNull(created);
            Assert.AreEqual("Unit", created.FirstName);

            // UPDATE
            created.Phone = "11112222";
            controller.UpdateOwner(created);

            Owner? updated = controller.GetSpecificOwner(ownerId);
            Assert.IsNotNull(updated);
            Assert.AreEqual("11112222", updated.Phone);

            // REMOVE
            controller.DeleteOwner(updated); // hvis din Delete tager Owner
                                             // eller: controller.DeleteOwner(ownerId);

            Owner? deleted = controller.GetSpecificOwner(ownerId);
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void TestTreatment_Add_Update_Remove_ViaController()
        {
            // ADD
            Treatment t = new Treatment(0)
            {
                Service = "UnitService",
                Date = new DateOnly(2020, 1, 2),
                Charge = 123
            };

            int treatmentId = controller.CreateTreatment(t);
            Assert.IsTrue(treatmentId > 0);

            Treatment? created = controller.GetSpecificTreatment(treatmentId);
            Assert.IsNotNull(created);
            Assert.AreEqual("UnitService", created.Service);

            // UPDATE
            created.Charge = 456;
            controller.UpdateTreatment(created);

            Treatment? updated = controller.GetSpecificTreatment(treatmentId);
            Assert.IsNotNull(updated);
            Assert.AreEqual(456, updated.Charge);

            // REMOVE
            controller.DeleteTreatment(updated); // eller DeleteTreatment(treatmentId)

            Treatment? deleted = controller.GetSpecificTreatment(treatmentId);
            Assert.IsNull(deleted);
        }
    }
}

//        [TestMethod]
//        public void TestPet_Add_Update_Remove_ViaController()
//        {
//            // PET har FK til Owner -> vi laver en owner først
//            Owner o = new Owner(0)
//            {
//                FirstName = "Pet",
//                LastName = "Owner",
//                Phone = null,
//                Email = "pet.owner@test.dk"
//            };
//            int ownerId = controller.CreateOwner(o);
//            Assert.IsTrue(ownerId > 0);

//            // ADD PET
//            Pet p = new Pet(0)
//            {
//                Name = "UnitPet",
//                PetType = PetType.Dog,
//                Breed = "Border Collie",
//                DateOfBirth = new DateOnly(2019, 5, 6),
//                Weight = 10
//                // OwnerId = ownerId // hvis din Pet-klasse har den property
//            };

//            int petId = controller.CreatePet(p, ownerId); // hvis din controller kræver ownerId separat
//                                                          // eller: int petId = controller.CreatePet(p);

//            Assert.IsTrue(petId > 0);

//            Pet? created = controller.GetSpecificPet(petId);
//            Assert.IsNotNull(created);
//            Assert.AreEqual("UnitPet", created.Name);

//            // UPDATE
//            created.Weight = 12;
//            controller.UpdatePet(created, ownerId); // eller UpdatePet(created)

//            Pet? updated = controller.GetSpecificPet(petId);
//            Assert.IsNotNull(updated);
//            Assert.AreEqual(12, updated.Weight);

//            // REMOVE
//            controller.DeletePet(updated); // eller DeletePet(petId)

//            Pet? deleted = controller.GetSpecificPet(petId);
//            Assert.IsNull(deleted);

//            // cleanup owner
//            Owner? ownerToDelete = controller.GetSpecificOwner(ownerId);
//            if (ownerToDelete != null)
//                controller.DeleteOwner(ownerToDelete);
//        }
//    }
//}
//}
