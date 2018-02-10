using CST356_Week_5_Lab.Models.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST356_Week_5_Lab.Services
{
    public interface IPetService
    {
        PetViewModel GetPet(int id);

        IEnumerable<PetViewModel> GetPetsForUser(int userId);

        void CreatePet(PetViewModel pet);

        void UpdatePet(PetViewModel user);

        void DeletePet(int id);
    }
}
