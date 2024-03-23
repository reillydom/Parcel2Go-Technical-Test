using Parcel2Go.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    public abstract class BaseShippingService : Business.Interfaces.IShippingService
    {

        /// <summary>
        /// Returns the price per package without discount.
        /// </summary>
        protected abstract int PricePerPackage { get; }
        protected readonly List<Models.BulkPurchaseDiscountModel> _bulkPurchaseDiscounts;

        protected BaseShippingService(List<Models.BulkPurchaseDiscountModel> bulkPurchaseDiscounts)
        {
            _bulkPurchaseDiscounts = bulkPurchaseDiscounts ?? throw new ArgumentNullException(nameof(bulkPurchaseDiscounts));
        }
       
        public abstract Parcel2Go.Core.Enums.ShippingServiceType ShippingServiceType { get; }

        public virtual int GetTotalPrice(int numberOfItemsToShip)
        {
            if (numberOfItemsToShip <= 0)
            {
                throw new ArgumentException($"{nameof(numberOfItemsToShip)} must be greater than 0.");
            }

            var totalCostToShip = 0;
            if (_bulkPurchaseDiscounts?.Any() ?? false)
            {
                var sortedBulkPurchaseDiscounts = _bulkPurchaseDiscounts
                    .Where(x => x.NumberOfItemRequiredForDiscount <= numberOfItemsToShip) // Only get the discounts that we qualify for based on the number of items that we're shipping.
                    .OrderByDescending(x => x.NumberOfItemRequiredForDiscount); // Order by DESC so that we get the highest number of items first. 

                foreach (var bulkPurchaseDiscount in _bulkPurchaseDiscounts)
                {
                    if (numberOfItemsToShip < bulkPurchaseDiscount.NumberOfItemRequiredForDiscount)
                    {
                        // Break out of the loop as we no longer have enough items to apply the discounts to.
                        // All previous discounts would have been applied as the collection should be ordered by NumberOfItemRequiredForDiscount DESC 
                        break;
                    }

                    // See how many times we can apply the discount. 
                    var numberOfTimesDiscountCanBeApplied = numberOfItemsToShip / bulkPurchaseDiscount.NumberOfItemRequiredForDiscount;
                    if (numberOfTimesDiscountCanBeApplied >= 0)
                    {
                        // get the cost to ship this parcels, and add it to the total cost.
                        var costToShipTheseParcels = (numberOfTimesDiscountCanBeApplied * bulkPurchaseDiscount.DiscountedPrice);
                        totalCostToShip += costToShipTheseParcels;
                        // Remove the number of items that we've applied the bulk discount
                        // for from the total number to ship, so that we can calculate any additional discounts
                        // and the total price for any parcels that don't fall under the discount.

                        // Note: Deducting the 'NumberOfItemRequiredForDiscount' from the 'numberOfItemsToShip' will only work if we apply the discount once per transaction
                        // i.e. they can only get the discount applied once regardless of the number of items we're shipping - if they shipped 7 items, they could only get the discount of 2 items once. 
                        // Using the modulo opperator will allow us to apply the discount multiple times. 
                        // Note: We'd probably have a flag that specified if the discount can be applied once per transaction or multiple times. 
                        numberOfItemsToShip %= bulkPurchaseDiscount.NumberOfItemRequiredForDiscount; // Get the number of items left(remainder) after applying the multi-discount e.g. 7/3 = 1.

                    }
                }
            }

            // Calculate the remaining cost. 
            totalCostToShip += (numberOfItemsToShip * this.PricePerPackage);

            return totalCostToShip;
        }


        public override string ToString()
        {
            return PricePerPackage.ToString("C");
        }
    }
}
