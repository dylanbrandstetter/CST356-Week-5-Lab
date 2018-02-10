using CST356_Week_5_Lab.Data.Entities;
using CST356_Week_5_Lab.Models.View;
using CST356_Week_5_Lab.Repositories;
using CST356_Week_5_Lab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CST356_Week_5_Lab.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _dataService;

        public PetController(IPetService service)
        {
            _dataService = service;
        }

        public ActionResult List(int userId)
        {
            ViewBag.UserId = userId;
            return View(_dataService.GetPetsForUser(userId));
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
                _dataService.CreatePet(petViewModel);

                return RedirectToAction("List", new { UserId = petViewModel.UserId });
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var pet = _dataService.GetPet(id);

            if (pet == null)
                return RedirectToAction("List", "User", null);
            else
            {
                int userId = pet.UserId;
                _dataService.DeletePet(id);

                return RedirectToAction("List", new { userId = userId });
            }
        }

        public ActionResult Details(int id)
        {
            var pet = _dataService.GetPet(id);

            return View(pet);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pet = _dataService.GetPet(id);
            ViewBag.UserId = pet.UserId;

            return View(pet);
        }

        [HttpPost]
        public ActionResult Edit(PetViewModel petViewModel)
        {
            if (ModelState.IsValid)
            {
                _dataService.UpdatePet(petViewModel);

                return RedirectToAction("List", new { UserId = petViewModel.UserId });
            }

            return View();
        }
    }
}