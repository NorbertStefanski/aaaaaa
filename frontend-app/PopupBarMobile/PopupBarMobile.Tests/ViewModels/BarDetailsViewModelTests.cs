using Moq;
using PopupBarMobile.Models;
using PopupBarMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Tests.ViewModels
{
    public class BarDetailsViewModelTests
    {
        private BarDetailsViewModel _model;
        private Mock<Bar> _barMock;

        [SetUp]
        public void BeforeEachTest()
        {
            _barMock = new Mock<Bar>();
            _model = new BarDetailsViewModel(_barMock.Object);
        }

        [Test]
        public void CocktailDetailsCommand_SuccessfullLoad_ShouldLoadCorrectly()
        {
            //Arrange
            _barMock.Object.barMenuItems = new List<BarMenuItem>(); //Otherwise it is null
            //Act
            _model.CocktailDetailsCommand.Execute(_barMock.Object);
            //Assert
            Assert.That(_model.Bar, Is.Not.Null);
            Assert.That(_model.Bar, Is.EqualTo(_barMock.Object));

            Assert.That(_model.MenuItems, Is.Not.Null);
            Assert.That(_model.MenuItems, Is.EqualTo(_barMock.Object.barMenuItems));
        }
    }
}
