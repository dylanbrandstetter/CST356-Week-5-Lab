using CST356_Week_5_Lab.Data.Entities;
using CST356_Week_5_Lab.Models.View;
using CST356_Week_5_Lab.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CST356_Week_5_Lab.Services
{
    public class PetService : IPetService
    {
        private readonly IAppRepository _dataRepository;

        public PetService(IAppRepository appRepository)
        {
            _dataRepository = appRepository;
        }

        public PetViewModel GetPet(int id)
        {
            return MapToPetViewModel(_dataRepository.GetPet(id));
        }

        public IEnumerable<PetViewModel> GetPetsForUser(int userId)
        {
            var userPets = new List<PetViewModel>();
            var realPets = _dataRepository.GetAllPets();

            foreach (var p in realPets)
            {
                if (p.UserId == userId)
                    userPets.Add(MapToPetViewModel(p));
            }

            return userPets;
        }

        public void CreatePet(PetViewModel pet)
        {
            _dataRepository.CreatePet(MapToPet(pet));
        }

        public void UpdatePet(PetViewModel petViewModel)
        {
            var pet = _dataRepository.GetPet(petViewModel.Id);

            CopyToPet(petViewModel, pet);

            _dataRepository.UpdatePet(pet);
        }

        public void DeletePet(int id)
        {
            _dataRepository.DeletePet(id);
        }

        // ----- Private Functions ----- //

        private Pet MapToPet(PetViewModel petViewModel)
        {
            return new Pet
            {
                Id = petViewModel.Id,
                Name = petViewModel.Name,
                Age = petViewModel.Age,
                NextCheckup = petViewModel.NextCheckup,
                VetName = petViewModel.VetName,
                UserId = petViewModel.UserId
            };
        }

        private PetViewModel MapToPetViewModel(Pet pet)
        {
            return new PetViewModel
            {
                Id = pet.Id,
                Name = pet.Name,
                Age = pet.Age,
                NextCheckup = pet.NextCheckup,
                VetName = pet.VetName,
                UserId = pet.UserId,
                CheckupAlert = (pet.NextCheckup - DateTime.Now).Days < 14
                // Broken version:
                //CheckupAlert = (pet.NextCheckup - DateTime.Now).Days > 14
            };
        }

        private void CopyToPet(PetViewModel petViewModel, Pet pet)
        {
            pet.Id = petViewModel.Id;
            pet.Name = petViewModel.Name;
            pet.Age = petViewModel.Age;
            pet.NextCheckup = petViewModel.NextCheckup;
            pet.VetName = petViewModel.VetName;
            pet.UserId = petViewModel.UserId;
        }
    }
}