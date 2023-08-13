using Moq;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using PopupBarMobile.ViewModels;
using PopupBarMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Tests.ViewModels
{
    public class BarCatalogViewModelTests
    {
        private BarCatalogViewModel _model;
        private Mock<IBarDataService> _barDataServiceMock;

        [SetUp]
        public void BeforeEachTest()
        {
            _barDataServiceMock = new Mock<IBarDataService>();
            _model = new BarCatalogViewModel(_barDataServiceMock.Object);
        }

        [Test]
        public void LoadBarsCommand_SuccessfulLoad_ShouldGetAllBars()
        {
            //Arrange
            List<Bar> bars = new List<Bar>();
            _barDataServiceMock.Setup(service => service.GetBarsAsync()).ReturnsAsync(bars);

            //Act
            _model.LoadBarsCommand.Execute(null);

            //Assert
            _barDataServiceMock.Verify(service => service.GetBarsAsync(), Times.Once);

        }
    }
}
