using System;
using CST356_Week_5_Lab.Services;
using CST356_Week_5_Lab.Repositories;
using NUnit.Framework;
using FakeItEasy;
using CST356_Week_5_Lab.Data.Entities;

namespace CST345_Week_5_Tests
{
    [TestFixture]
    public class PetServiceTests
    {
        private IAppRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = A.Fake<IAppRepository>();
        }

        [Test]
        public void ShouldNotGiveCheckupAlert()
        {
            A.CallTo(() => _repository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Now.AddDays(15)
            });

            var petService = new PetService(_repository);
            var petViewModel = petService.GetPet(1);

            Assert.IsFalse(petViewModel.CheckupAlert);
        }

        [Test]
        public void ShouldGiveCheckupAlert()
        {
            A.CallTo(() => _repository.GetPet(A<int>.Ignored)).Returns(new Pet
            {
                NextCheckup = DateTime.Now.AddDays(13)
            });

            var petService = new PetService(_repository);
            var petViewModel = petService.GetPet(1);

            Assert.IsTrue(petViewModel.CheckupAlert);
        }
    }
}
