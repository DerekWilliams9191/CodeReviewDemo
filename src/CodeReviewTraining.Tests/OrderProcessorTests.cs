using System;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace CodeReviewTraining.Tests
{
    public class OrderProcessorTests
    {
        private readonly Mock<ILogger<OrderProcessor>> _loggerMock;
        private readonly Mock<IValidator> _validatorMock;
        private readonly Mock<IDatabase> _databaseMock;
        private readonly OrderProcessor _orderProcessor;

        public OrderProcessorTests()
        {
            _loggerMock = new Mock<ILogger<OrderProcessor>>();
            _validatorMock = new Mock<IValidator>(); // Fixed: Removed extra > character
            _databaseMock = new Mock<IDatabase>();
            _orderProcessor = new OrderProcessor(_loggerMock.Object, _validatorMock.Object, _databaseMock.Object);
        }

        [Fact]
        public void ProcessOrder_WithNullOrderRequest_ThrowsArgumentNullException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _orderProcessor.ProcessOrder(null));
            Assert.Equal("orderRequest", ex.ParamName);
        }

        [Fact]
        public void ProcessOrder_WithInvalidOrder_ThrowsValidationException()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = Guid.NewGuid(),
                Items = new[] { new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };
            
            _validatorMock.Setup(v => v.ValidateOrder(orderRequest)).Returns(false);

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => _orderProcessor.ProcessOrder(orderRequest));
            Assert.Contains("validation", ex.Message.ToLower());
        }

        [Fact]
        public void ProcessOrder_WithValidOrder_ReturnsConfirmation()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = Guid.NewGuid(),
                Items = new[] { new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };
            
            _validatorMock.Setup(v => v.ValidateOrder(orderRequest)).Returns(true);

            // Act
            var result = _orderProcessor.ProcessOrder(orderRequest);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.OrderId);
            Assert.NotNull(result.TrackingNumber);
            Assert.True(result.EstimatedDelivery > DateTime.UtcNow);
            
            // Verify database was called
            _databaseMock.Verify(d => d.SaveOrder(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void ProcessOrder_WithDatabaseException_LogsAndRethrows()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                CustomerId = Guid.NewGuid(),
                Items = new[] { new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1 } }
            };
            
            _validatorMock.Setup(v => v.ValidateOrder(orderRequest)).Returns(true);
            _databaseMock.Setup(d => d.SaveOrder(It.IsAny<Order>())).Throws(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _orderProcessor.ProcessOrder(orderRequest));
            Assert.Equal("Database error", ex.Message);
            
            // Verify that error was logged
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => true),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}
