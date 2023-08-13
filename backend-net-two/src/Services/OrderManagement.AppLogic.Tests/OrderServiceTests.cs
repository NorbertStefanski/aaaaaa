using AppLogic.Events;
using Moq;
using OrderManagement.Domain;
using Test;

namespace OrderManagement.AppLogic.Tests
{
    public class OrderServiceTests : TestBase
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IOrderFactory> _orderFactory;
        private Mock<IEventBus> _eventBus;
        private OrderService _service;
        [SetUp]
        public void Setup()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderFactory = new Mock<IOrderFactory>();
            _eventBus = new Mock<IEventBus>();
            _service = new OrderService(_orderRepository.Object, _orderFactory.Object, _eventBus.Object);
        }

        [Test]
        public void AddNewAsync_Should_CreateTheOrder_AndSaveIt()
        {
            //Arrange
            Guid barId = new Guid();
            int tableId = Random.NextPositive(100);
            List<Guid> orderItemIds = new List<Guid>();
            orderItemIds.Add(new Guid());
            double orderTotal = Random.NextDouble();

            IOrder createdOrder = new Mock<IOrder>().Object;
            _orderFactory.Setup(factory => factory.CreateNewOrder(It.IsAny<Guid>(), It.IsAny<int>(), null, It.IsAny<IList<Guid>>(), It.IsAny<double>())).Returns(createdOrder);

            //Act
            IOrder result = _service.AddNewAsync(barId, tableId, null, orderItemIds, orderTotal).Result;

            //Assert
            _orderRepository.Verify(repo => repo.AddOrderAsync(It.IsAny<IOrder>()), Times.Once);
            _orderFactory.Verify(factory => factory.CreateNewOrder(barId, tableId, null, orderItemIds, orderTotal), Times.Once);
            Assert.That(result, Is.SameAs(createdOrder));
        }
    }
}