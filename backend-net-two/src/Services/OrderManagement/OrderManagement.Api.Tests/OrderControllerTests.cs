using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Api.Controllers;
using OrderManagement.Api.Models;
using OrderManagement.Api.Tests.Builders;
using OrderManagement.AppLogic;
using OrderManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace OrderManagement.Api.Tests
{
    public class OrderControllerTests : TestBase
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<IOrderService> _orderServiceMock;
        private OrdersController _ordersController;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderServiceMock = new Mock<IOrderService>();
            _ordersController = new OrdersController(_orderRepositoryMock.Object, _orderServiceMock.Object);
        }

        [Test]
        public void Add_ShouldUseService()
        {
            //Arrange
            OrderCreateModel createModel = new OrderCreateModelBuilder().Build();

            IOrder createdOrder = new Mock<IOrder>().Object;
            _orderServiceMock
                .Setup(service => service.AddNewAsync(It.IsAny<Guid>(), It.IsAny<int>(), null, It.IsAny<List<Guid>>(), It.IsAny<double>()));

            //Act
            var result = _ordersController.AddNewOrder(createModel).Result as CreatedAtActionResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _orderServiceMock.Verify(
                service => service.AddNewAsync(createModel.BarId, createModel.TableId, null, createModel.OrderedItemIds, createModel.OrderTotal),
                Times.Once);
            Assert.That(result.ActionName, Is.EqualTo(nameof(OrdersController.GetAllOrders)));
        }

        [Test]
        public void GetAllOrders_ShouldUseRepository()
        {
            //Arrange
            Order order = new Mock<Order>().Object;
            List<Order> orders = new List<Order>();
            orders.Add((Order)order);

            _orderRepositoryMock.Setup(repo => repo.GetAllOrdersAsync()).ReturnsAsync(orders);
            //Act
            var result = _ordersController.GetAllOrders().Result as OkObjectResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _orderRepositoryMock.Verify(repo => repo.GetAllOrdersAsync(), Times.AtLeastOnce);
            Assert.That(result.Value, Is.SameAs(orders));
        }

        [Test]
        public void GetAllOrders_NoOrdersExists_ShouldReturnNotFound()
        {
            //Arrange
            _orderRepositoryMock
                .Setup(repo => repo.GetAllOrdersAsync())
                .ReturnsAsync(() => null);

            //Act
            var result = _ordersController.GetAllOrders().Result as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _orderRepositoryMock.Verify(repo => repo.GetAllOrdersAsync(), Times.Once);
        }
    }
}
