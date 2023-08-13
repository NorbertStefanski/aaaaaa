using Moq;
using PopupBarMobile.Contracts.Services.Data;
using PopupBarMobile.Models;
using PopupBarMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupBarMobile.Tests.ViewModels
{
    internal class CheckoutViewModelTests
    {
        private CheckoutViewModel _model;
        private Mock<IOrderDataService> _orderDataServiceMock;
        private Mock<ObservableCollection<BarMenuItem>> _selectedCocktails;
        private string _barId;

        [SetUp]
        public void BeforeEachTest()
        {
            _orderDataServiceMock= new Mock<IOrderDataService>();
            _selectedCocktails = new Mock<ObservableCollection<BarMenuItem>>();
            _barId = "";
            _model = new CheckoutViewModel(_orderDataServiceMock.Object, _selectedCocktails.Object, _barId);
        }

        [Test]
        public void ConfirmOrderCommandCommand_ShouldUsePostOrderAsync()
        {
            //Arrange
            _orderDataServiceMock.Setup(service => service.PostOrderAsync(It.IsAny<Order>()));

            //Act
            _model.ConfirmOrderCommand.Execute(null);

            //Assert
            _orderDataServiceMock.Verify(service => service.PostOrderAsync(It.IsAny<Order>()), Times.Once);

        }
    }
}
