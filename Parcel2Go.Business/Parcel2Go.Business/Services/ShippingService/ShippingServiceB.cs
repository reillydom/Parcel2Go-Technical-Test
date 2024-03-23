using Parcel2Go.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    internal class ShippingServiceB : BaseShippingService
    {
        public override ShippingServiceType ShippingServiceType => Core.Enums.ShippingServiceType.ServiceB;

        protected override int PricePerPackage => 12;


        public ShippingServiceB() 
            : base(new List<Models.BulkPurchaseDiscountModel>()
                {
                    new Models.BulkPurchaseDiscountModel(2, 20)
                })
        {

        }
    }
}
