using Parcel2Go.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    internal class ShippingServiceD : BaseShippingService
    {
        public override ShippingServiceType ShippingServiceType => Core.Enums.ShippingServiceType.ServiceD;

        protected override int PricePerPackage => 25;


        public ShippingServiceD() 
            // No discount for shipping service D
            : base(new List<Models.BulkPurchaseDiscountModel>())
        {

        }
    }
}
