using Parcel2Go.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    internal class ShippingServiceA : BaseShippingService
    {
        protected override int PricePerPackage => 10;

        public override ShippingServiceType ShippingServiceType =>  Core.Enums.ShippingServiceType.ServiceA;

        public ShippingServiceA() 
            : base(new List<Models.BulkPurchaseDiscountModel>()
                {
                    new Models.BulkPurchaseDiscountModel(3, 25)
                })
        {

        }
    }
}
