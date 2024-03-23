using Parcel2Go.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    internal class ShippingServiceC : BaseShippingService
    {
        public override ShippingServiceType ShippingServiceType => Core.Enums.ShippingServiceType.ServiceC;

        protected override int PricePerPackage => 15;


        public ShippingServiceC() 
            // No discount for shipping service C
            : base(new List<Models.BulkPurchaseDiscountModel>())
        {

        }
    }
}
