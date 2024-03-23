using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Instances.Checkout
{
    public class ParcelCheckout : Business.Interfaces.ICheckout
    {
        private readonly IEnumerable<Business.Interfaces.IShippingService> _shippingServices;
        private readonly ILogger<ParcelCheckout> _logger;

        private List<Core.Enums.ShippingServiceType> _scannedShippingServices;

        public ParcelCheckout(IEnumerable<Business.Interfaces.IShippingService> shippingServices, ILogger<ParcelCheckout> logger)
        {
            _shippingServices = shippingServices ?? throw new ArgumentNullException(nameof(shippingServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _scannedShippingServices = new();
        }

        public void Scan(string service)
        {
            var matchingShippingServiceByDescription = Core.Helpers.EnumHelpers.GetEnumFromDescription<Core.Enums.ShippingServiceType>(service);

            // Ensure it's a supported service. 
            if (!matchingShippingServiceByDescription.HasValue)
            {
                var exceptionMessage = $"Provided shipping service {service} is not supported";
                _logger.LogError(exceptionMessage);
                throw new NotSupportedException(exceptionMessage);
            }
            // Valid shipping service, add it to our collection.
            _scannedShippingServices.Add(matchingShippingServiceByDescription.Value);
        }


        public int GetTotalPrice()
        {
            _logger.LogDebug($"Call to {nameof(GetTotalPrice)}");

            var totalPrice = 0;
            var groupedScannedShippingService = _scannedShippingServices.GroupBy(x => x);
            _logger.LogDebug($"scanned shipping services and number of parcels to ship ==> {JsonSerializer.Serialize(groupedScannedShippingService.Select(x => new { Key = x.Key, NumberOfParcels = x.Count() }))}");

            foreach (var scannedShippingService in groupedScannedShippingService)
            {
                // Find the matching shipping service by the name. 
                // Let an exception be thrown if we can't find the matching service - Shouldn't happend as we check during the scan method. 
                var matchingShippingService = _shippingServices.First(x => x.ShippingServiceType == scannedShippingService.Key);
                _logger.LogDebug($"Found matching shipping service for key {scannedShippingService.Key}");

                _logger.LogDebug($"Pre-fetch of total price from shipping service ==> {totalPrice}");
                totalPrice += matchingShippingService.GetTotalPrice(scannedShippingService.Count());
                _logger.LogDebug($"Updated total price ==> {totalPrice}");
            }
            _logger.LogDebug($"Final totalPrice ==> {totalPrice}");
            return totalPrice;
        }
    }
}
