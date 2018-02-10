using CST356_Week_5_Lab.Data.Entities;
using CST356_Week_5_Lab.Models.View;
using CST356_Week_5_Lab.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CST356_Week_5_Lab.Controllers
{
    public class PetController : Controller
    {
        private readonly IAppRepository _dataRepository;

        public PetController(IAppRepository repository)
        {
            _dataRepository = repository;
        }

        public ActionResult List(int userId)
        {
            ViewBag.UserId = userId;
            return View(GetAllPetsForUser(userId));
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public ActionResult Create(PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                _dataRepository.CreatePet(MapToPet(petViewModel));

                return RedirectToAction("List", new { UserId = petViewModel.UserId });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            Pet pet = _dataRepository.GetPet(id);

            if (pet == null)
                return RedirectToAction("List", "User", null);
            else
            {
                int userId = pet.UserId;
                _dataRepository.DeletePet(id);

                return RedirectToAction("List", new { userId = userId });
            }
        }

        public ActionResult Details(int id)
        {
            var pet = GetPet(id);

            return View(pet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pet = GetPet(id);
            ViewBag.UserId = pet.UserId;

            return View(pet);
        }

        [HttpPost]
        public ActionResult Edit(PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                UpdatePet(petViewModel);

                return RedirectToAction("List", new { UserId = petViewModel.UserId });
            }

            return View();
        }

        // ----- Private functions ----- //

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
                UserId = pet.UserId
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

        private PetViewModel GetPet(int id)
        {
            return MapToPetViewModel(_dataRepository.GetPet(id));
        }

        private List<PetViewModel> GetAllPetsForUser(int userId)
        {
            var userPets = new List<PetViewModel>();
            var realPets = _dataRepository.GetAllPets().ToList();

            foreach (var p in realPets)
            {
                if (p.UserId == userId)
                    userPets.Add(MapToPetViewModel(p));
            }

            return userPets;
        }

        private void UpdatePet(PetViewModel petViewModel)
        {
            var pet = _dataRepository.GetPet(petViewModel.Id);

            CopyToPet(petViewModel, pet);

            _dataRepository.UpdatePet(pet);
        }
    }
}