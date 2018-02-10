using CST356_Week_5_Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CST356_Week_5_Lab.Models.View;
using CST356_Week_5_Lab.Repositories;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IAppRepository _dataRepository;

        public UserController(IAppRepository repository)
        {
            _dataRepository = repository;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _dataRepository.CreateUser(MapToUser(userViewModel));

                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            _dataRepository.DeleteUser(id);

            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            var user = GetUser(id);
            return View(user);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = GetUser(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                UpdateUser(userViewModel);

                return RedirectToAction("List");
            }

            return View();
        }

        public ActionResult List()
        {
            return View(GetAllUsers());
        }

        // ----- Private functions ----- //

        private User MapToUser(UserViewModel userViewModel)
        {
            return new User
            {
                Id = userViewModel.Id,
                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                EmailAddress = userViewModel.EmailAddress,
                YearsInSchool = userViewModel.YearsInSchool,
                Age = userViewModel.Age,
                Occupation = userViewModel.Occupation
            };
        }

        private UserViewModel MapToUserViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                YearsInSchool = user.YearsInSchool,
                Age = user.Age,
                Occupation = user.Occupation
            };
        }

        private void CopyToUser(UserViewModel userViewModel, User user)
        {
            user.Id = userViewModel.Id;
            user.FirstName = userViewModel.FirstName;
            user.MiddleName = userViewModel.MiddleName;
            user.LastName = userViewModel.LastName;
            user.EmailAddress = userViewModel.EmailAddress;
            user.YearsInSchool = userViewModel.YearsInSchool;
            user.Age = userViewModel.Age;
            user.Occupation = userViewModel.Occupation;
        }

        private UserViewModel GetUser(int id)
        {
            return MapToUserViewModel(_dataRepository.GetUser(id));
        }

        private List<UserViewModel> GetAllUsers()
        {
            var users = new List<UserViewModel>();

            var realUsers = _dataRepository.GetAllUsers().ToList();

            foreach (var u in realUsers)
            {
                users.Add(MapToUserViewModel(u));
            }

            return users;
        }

        private void UpdateUser(UserViewModel userViewModel)
        {
            var user = _dataRepository.GetUser(userViewModel.Id);

            CopyToUser(userViewModel, user);

            _dataRepository.UpdateUser(user);
        }
    }
}