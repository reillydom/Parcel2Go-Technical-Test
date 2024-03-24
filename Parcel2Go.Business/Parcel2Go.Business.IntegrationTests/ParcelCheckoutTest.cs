using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Parcel2Go.Business.IntegrationTests
{
    public class ParcelCheckoutTest
    {

        private readonly ITestOutputHelper _output;

        public ParcelCheckoutTest(ITestOutputHelper output)
        {
            _output = output;
        }


        [Theory]
        [MemberData(nameof(GetTestData))]
        public void GetTotalPrice_Returns_Expected_Values(Models.ParcelCheckoutTestModel testModel)
        {
            // Arrange
            var serviceCollection = new ServiceCollection();


            serviceCollection.AddScoped<Business.Interfaces.ICheckout, Business.Instances.Checkout.ParcelCheckout>();


            var shippingServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                       .Where(x => typeof(Parcel2Go.Business.Interfaces.IShippingService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                       .ToList();

            foreach (var shippingService in shippingServices)
            {
                serviceCollection.AddTransient(typeof(Parcel2Go.Business.Interfaces.IShippingService), shippingService);
            }

            var fakeServiceProvider = serviceCollection.AddLogging(loggingBuilder =>
            {
                loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_output));
            })
            .BuildServiceProvider();


            var checkoutInstance = fakeServiceProvider.GetRequiredService<Business.Interfaces.ICheckout>();

            // Act
            foreach(var shippingService in testModel.ShippingServices)
            {
                checkoutInstance.Scan(shippingService);
            }

            var totalPrice = checkoutInstance.GetTotalPrice();

            // Assert
            Assert.True(totalPrice == testModel.ExpectedTotalCost,
                $"Expected {testModel.ExpectedTotalCost} but got {totalPrice}");
        }




        public static TheoryData<Models.ParcelCheckoutTestModel> GetTestData()
        {
            return new TheoryData<Models.ParcelCheckoutTestModel>
            {
                // Example 1: Multipurchase Discount Advantage
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service B",
                    "Service B"
                }, 20),

                // Example 2: No Multipurchase Discount
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service F",
                    "Service C"
                }, 23),
                // Example 3: Mix of Discounts and No Discount
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service F",
                    "Service F",
                    "Service B"
                }, 27),
                // Custom test cases.
                // Bulk Purchase Discount with Service A
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service A",
                    "Service A",
                    "Service A"
                }, 25),
                // No Discounts Applied
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service C",
                    "Service C",
                    "Service C",
                    "Service D",
                    "Service D",

                }, 95),
                // Mixed Discounts Applied
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service A",
                    "Service A",
                    "Service A",
                    "Service A",
                    "Service A",
                    "Service B",
                    "Service B",
                    "Service F",
                }, 73),
                // Single Item with Discount
                new Models.ParcelCheckoutTestModel(new List<string>()
                {
                    "Service F",
                }, 8),
            };


        }



        #region Original individual tests

        //[Fact]
        //public void Multipurchase_Discount_Advantage_Returns_Expected_Discount_For_Service_B()
        //{
        //    // Arrange
        //    var serviceCollection = new ServiceCollection();


        //    serviceCollection.AddScoped<Business.Interfaces.ICheckout, Business.Instances.Checkout.ParcelCheckout>();


        //    var shippingServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //               .Where(x => typeof(Parcel2Go.Business.Interfaces.IShippingService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //               .ToList();
        //    foreach (var shippingService in shippingServices)
        //    {
        //        serviceCollection.AddTransient(typeof(Parcel2Go.Business.Interfaces.IShippingService), shippingService);
        //    }

        //    var fakeServiceProvider = serviceCollection.AddLogging(loggingBuilder =>
        //    {
        //        loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_output));
        //    })
        //    .BuildServiceProvider();


        //    var checkoutInstance = fakeServiceProvider.GetRequiredService<Business.Interfaces.ICheckout>();

        //    //Customer's Cart: 2 x Service B
        //    //Standard Pricing: £12 each
        //    //Special Offer for B: 2 for £20
        //    //Total without discount: 2 x £12 = £24
        //    //With multipurchase discount: £20
        //    //Decision: The system applies the multipurchase discount, totaling £20, as it offers better savings.


        //    // Act
        //    checkoutInstance.Scan("Service B");
        //    checkoutInstance.Scan("Service B");

        //    var totalPrice = checkoutInstance.GetTotalPrice();

        //    // Assert
        //    Assert.True(totalPrice == 20);
        //}


        //[Fact]
        //public void Multipurchase_Discount_Advantage_Returns_No_Discount()
        //{
        //    // Arrange
        //    var serviceCollection = new ServiceCollection();


        //    serviceCollection.AddScoped<Business.Interfaces.ICheckout, Business.Instances.Checkout.ParcelCheckout>();


        //    var shippingServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //               .Where(x => typeof(Parcel2Go.Business.Interfaces.IShippingService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //               .ToList();
        //    foreach (var shippingService in shippingServices)
        //    {
        //        serviceCollection.AddTransient(typeof(Parcel2Go.Business.Interfaces.IShippingService), shippingService);
        //    }

        //    var fakeServiceProvider = serviceCollection.AddLogging(loggingBuilder =>
        //    {
        //        loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_output));
        //    })
        //    .BuildServiceProvider();


        //    var checkoutInstance = fakeServiceProvider.GetRequiredService<Business.Interfaces.ICheckout>();

        //    //Customer's Cart: 1 x Service F and 1 x Service C
        //    //Standard Pricing: Service F at £8 and Service C at £15
        //    //No applicable multipurchase discount.
        //    //Total without discount: £8 + £15 = £23
        //    //Decision: The system calculates the total as £23 due to no available multipurchase discounts.

        //    var expectedCost = 23;

        //    // Act
        //    checkoutInstance.Scan("Service F");
        //    checkoutInstance.Scan("Service C");

        //    var totalPrice = checkoutInstance.GetTotalPrice();

        //    // Assert
        //    Assert.True(totalPrice == expectedCost);
        //}



        //[Fact]
        //public void Multipurchase_Discount_Advantage_Returns_Expected_Value_For_Mixed_Discount_Order()
        //{
        //    // Arrange
        //    var serviceCollection = new ServiceCollection();


        //    serviceCollection.AddScoped<Business.Interfaces.ICheckout, Business.Instances.Checkout.ParcelCheckout>();


        //    var shippingServices = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
        //               .Where(x => typeof(Parcel2Go.Business.Interfaces.IShippingService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
        //               .ToList();
        //    foreach (var shippingService in shippingServices)
        //    {
        //        serviceCollection.AddTransient(typeof(Parcel2Go.Business.Interfaces.IShippingService), shippingService);
        //    }

        //    var fakeServiceProvider = serviceCollection.AddLogging(loggingBuilder =>
        //    {
        //        loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_output));
        //    })
        //    .BuildServiceProvider();


        //    var checkoutInstance = fakeServiceProvider.GetRequiredService<Business.Interfaces.ICheckout>();

        //    //Customer's Cart: 2 x Service F, 1 x Service B
        //    //Standard Pricing: F @ £8 each, B @ £12 each
        //    //Special Offer for F: 2 for £15
        //    //Total without discount: 2 x £8 + 1 x 12 = £28
        //    //With multipurchase discount: £27
        //    //Decision: The system applies the multipurchase discount for the F products, no discount on B as only 1 bought reducing the total to £27.
        //    var expectedCost = 27;

        //    // Act
        //    checkoutInstance.Scan("Service F");
        //    checkoutInstance.Scan("Service F");
        //    checkoutInstance.Scan("Service B");

        //    var totalPrice = checkoutInstance.GetTotalPrice();

        //    // Assert
        //    Assert.True(totalPrice == expectedCost);
        //}

        #endregion






    }

}