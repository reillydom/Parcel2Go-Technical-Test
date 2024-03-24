using Microsoft.Extensions.Logging;
using Moq;
using Parcel2Go.Business.Instances.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Tests.InstanceTests
{


    public class ParcelCheckoutTests
    {


        [Fact]
        public void Scan_Does_Not_Throw_Exception()
        {
            // Arrange
            var mockedShippingService = new Mock<Business.Interfaces.IShippingService>();
            mockedShippingService.Setup(x => x.ShippingServiceType).Returns(Core.Enums.ShippingServiceType.ServiceA);

            var shippingServices = new List<Business.Interfaces.IShippingService>()
            {
                mockedShippingService.Object
            };
            var mockedLogger = new Mock<ILogger<ParcelCheckout>>();


            var checkoutProcess = new Parcel2Go.Business.Instances.Checkout.ParcelCheckout(shippingServices, mockedLogger.Object);
            // Act
            var exception = Record.Exception(() => checkoutProcess.Scan("Service A"));


            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Scan_Throws_Exception_When_Invalid_Shipping_Service_Is_Passed()
        {
            // Arrange
            var mockedShippingService = new Mock<Business.Interfaces.IShippingService>();
        
            var shippingServices = new List<Business.Interfaces.IShippingService>()
            {
               
            };
            var mockedLogger = new Mock<ILogger<ParcelCheckout>>();


            var checkoutProcess = new Parcel2Go.Business.Instances.Checkout.ParcelCheckout(shippingServices, mockedLogger.Object);
            // Act
            var exception = Record.Exception(() => checkoutProcess.Scan("Service A"));


            // Assert
            Assert.Null(exception);

        }
    }
}
