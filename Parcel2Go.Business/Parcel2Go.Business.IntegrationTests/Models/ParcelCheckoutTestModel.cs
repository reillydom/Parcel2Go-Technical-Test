using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.IntegrationTests.Models
{
    // NOTE: Public to allow XUnit MemberData to acess
    public record ParcelCheckoutTestModel
    {
        public ParcelCheckoutTestModel(List<string> shippingServices, int expectedTotalCost)
        {
            ShippingServices = shippingServices;
            ExpectedTotalCost = expectedTotalCost;
        }

        public List<string> ShippingServices { get; }

        public int ExpectedTotalCost { get; }


    }
}
