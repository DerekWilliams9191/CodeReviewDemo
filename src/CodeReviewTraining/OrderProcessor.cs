using System;
using Microsoft.Extensions.Logging;

namespace CodeReviewTraining
{
    /// <summary>
    /// A service for processing user orders
    /// </summary>
    public class OrderProcessor
    {
        private readonly ILogger<OrderProcessor> _logger;
        private readonly IValidator _validator;
        private readonly IDatabase _database;

        public OrderProcessor(ILogger<OrderProcessor> logger, IValidator validator, IDatabase database)
        {
            _logger = logger;
            _validator = validator;
            _database = database;
        }

        // THIS CODE NEEDS REVIEW
        // STEP 1: First uncomment the null checks and validation to make tests pass
        // STEP 2: Then address the remaining code quality issues for PR approval

        public OrderConfirmation ProcessOrder(OrderRequest orderRequest)
        {
            // STEP 1: UNCOMMENT THESE SECTIONS FIRST TO MAKE TESTS PASS
            // ===========================================================
            
            // Uncomment for NULL CHECKS issue - REQUIRED TO PASS TESTS
            /*
            if (orderRequest == null)
            {
                throw new ArgumentNullException(nameof(orderRequest), "Order request cannot be null");
            }

            // Uncomment for VALIDATION issue - REQUIRED TO PASS TESTS
            if (!_validator.ValidateOrder(orderRequest))
            {
                throw new ValidationException("Order request failed validation");
            }
            */

            OrderConfirmation confirmation = null;
            
            // Uncomment for EXCEPTION HANDLING issue - REQUIRED TO PASS TESTS
            /*
            try
            {
            */
                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    CustomerId = orderRequest.CustomerId,
                    Items = orderRequest.Items,
                    OrderDate = DateTime.UtcNow
                };

                _database.SaveOrder(order);
                
                confirmation = new OrderConfirmation
                {
                    OrderId = order.OrderId,
                    TrackingNumber = GenerateTrackingNumber(),
                    EstimatedDelivery = DateTime.UtcNow.AddDays(3)
                };
            /*
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing order for customer {CustomerId}", orderRequest.CustomerId);
                throw;
            }
            */

            return confirmation;
            
            // STEP 2: AFTER TESTS PASS, UNCOMMENT THESE FOR CODE QUALITY
            // ===========================================================
            
            // Uncomment for LOGGING issue
            /*
            _logger.LogInformation("Processing order for customer {CustomerId}", orderRequest.CustomerId);
            */
        }

        // Uncomment for DOCUMENTATION issue
        /*
        /// <summary>
        /// Generates a unique tracking number for an order
        /// </summary>
        /// <returns>A tracking number in the format XXX-YYYYY-ZZ</returns>
        */
        private string GenerateTrackingNumber()
        {
            var random = new Random();
            var prefix = random.Next(100, 999);
            var middle = random.Next(10000, 99999);
            var suffix = random.Next(10, 99);
            
            return $"{prefix}-{middle}-{suffix}";
        }
    }

    // Supporting types
    public class OrderRequest
    {
        public Guid CustomerId { get; set; }
        public OrderItem[] Items { get; set; }
    }

    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderItem[] Items { get; set; }
        public DateTime OrderDate { get; set; }
    }

    public class OrderConfirmation
    {
        public Guid OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }

    public interface IValidator
    {
        bool ValidateOrder(OrderRequest request);
    }

    public interface IDatabase
    {
        void SaveOrder(Order order);
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
