using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Interfaces
{
    public interface IShippingService
    {

        /// <summary>
        /// The shipping service type e.g. service A.
        /// </summary>
       Parcel2Go.Core.Enums.ShippingServiceType ShippingServiceType { get; }

        /// <summary>
        /// Calculates the total price based on the number of items to ship.
        /// </summary>
        /// <param name="numberOfItemsToShip">The number of items to be shipped.</param>
        /// <returns>The total price for shipping the specified number of items.</returns>
        int GetTotalPrice(int numberOfItemsToShip);


    }
}
