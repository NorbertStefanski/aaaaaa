using Domain;
using Test;

namespace OrderManagement.Domain.Tests
{
    public class OrderFactoryTests : TestBase
    {
        private Order.Factory _factory;
        private Guid _Id;
        private Guid _BarId;
        private int _TableId;
        private string _OrderedItemIds;
        private double _OrderTotal;

        [SetUp]
        public void Setup()
        {
            _factory = new Order.Factory();
            _Id = Guid.NewGuid();
            _BarId = Guid.NewGuid();
            _TableId = Random.NextPositive(100);
            _OrderTotal = Random.Next(1, 101);
            _OrderedItemIds = Guid.NewGuid().ToString();

        }

        [Test]
        public void CreateNew_ValidInput_ShouldInitializeFieldsCorrectly()
        {
            //Act
            IList<Guid> orderGuids = new List<Guid>();
            orderGuids.Add(Guid.NewGuid());
            IOrder order = _factory.CreateNewOrder(_BarId, _TableId, null, orderGuids, _OrderTotal);

            //Assert
            Assert.That(order.BarId, Is.EqualTo(_BarId));
            Assert.That(order.TableId, Is.EqualTo(_TableId));
            Assert.That(order.OrderTotal, Is.EqualTo(_OrderTotal));
            Assert.That(order.OrderedItemIds, Is.Not.Null);
        }
        [Test]
        public void CreateNew_InvalidInput_ShouldThrowContractException()
        {
            //Act
            IList<Guid> orderGuids = new List<Guid>();
            //Assert
            Assert.That(() => _factory.CreateNewOrder(_BarId, _TableId, null, orderGuids, _OrderTotal), Throws.InstanceOf<ContractException>());
        }
    }
}