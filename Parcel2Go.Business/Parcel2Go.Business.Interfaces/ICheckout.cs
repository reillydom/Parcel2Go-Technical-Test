using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Interfaces
{
    public interface ICheckout
    {
        void Scan(string service); // Adds a service to the checkout
        int GetTotalPrice(); // Calculates the total price, applying the best discount option
    }
}
