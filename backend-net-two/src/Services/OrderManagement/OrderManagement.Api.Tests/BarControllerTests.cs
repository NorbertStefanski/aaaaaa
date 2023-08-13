using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.Api.Controllers;
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
    public class BarControllerTests : TestBase
    {
        private BarsController _barsController;
        private Mock<IBarRepository> _barRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _barRepositoryMock = new Mock<IBarRepository>();
            _barsController = new BarsController(_barRepositoryMock.Object);
        }

        [Test]
        public void GetAllBars_ShouldUseRepository()
        {
            //Arrange
            Bar bar = new Mock<Bar>().Object;
            List<Bar> bars = new List<Bar>();
            bars.Add(bar);

            _barRepositoryMock.Setup(repo => repo.GetAllBarsAsync()).ReturnsAsync(bars);
            //Act
            var result = _barsController.GetAllBars().Result as OkObjectResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _barRepositoryMock.Verify(repo => repo.GetAllBarsAsync(), Times.AtLeastOnce);
            Assert.That(result.Value, Is.SameAs(bars));
        }

        [Test]
        public void GetAllBars_NoBarsExists_ShouldReturnNotFound()
        {
            //Arrange
            _barRepositoryMock
                .Setup(repo => repo.GetAllBarsAsync())
                .ReturnsAsync(() => null);

            //Act
            var result = _barsController.GetAllBars().Result as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _barRepositoryMock.Verify(repo => repo.GetAllBarsAsync(), Times.Once);
        }

        [Test]
        public void GetById_ShouldUseRepositoryAndReturnResult()
        {
            //Arrange
            Bar bar = new Mock<Bar>().Object;
            _barRepositoryMock
                .Setup(repo => repo.GetBarByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(bar);

            //Act
            var result = _barsController.GetBarById(bar.Id).Result as OkObjectResult;

            //Assert
            Assert.That(result, Is.Not.Null);

            _barRepositoryMock.Verify(repo => repo.GetBarByIdAsync(bar.Id), Times.Once);
            Assert.That(result.Value, Is.SameAs(bar));
        }

        [Test]
        public void GetById_BarDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange
            _barRepositoryMock
                .Setup(repo => repo.GetBarByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(() => null);

            //Act
            var result = _barsController.GetBarById(new Guid()).Result as NotFoundResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            _barRepositoryMock.Verify(repo => repo.GetBarByIdAsync(new Guid()), Times.Once);
        }
    }
}
