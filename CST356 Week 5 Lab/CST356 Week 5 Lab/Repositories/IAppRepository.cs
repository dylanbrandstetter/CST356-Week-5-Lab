using CST356_Week_5_Lab.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST356_Week_5_Lab.Repositories
{
    public interface IAppRepository
    {
        User GetUser(int id);

        IEnumerable<User> GetAllUsers();

        void CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(int id);

        Pet GetPet(int id);

        IEnumerable<Pet> GetAllPets();

        void CreatePet(Pet pet);

        void UpdatePet(Pet pet);

        void DeletePet(int id);
    }
}
