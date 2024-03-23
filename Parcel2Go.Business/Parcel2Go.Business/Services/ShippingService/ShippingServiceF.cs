using Parcel2Go.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Services.ShippingService
{
    internal class ShippingServiceF : BaseShippingService
    {
        public override ShippingServiceType ShippingServiceType => Core.Enums.ShippingServiceType.ServiceF;

        protected override int PricePerPackage => 8;


        public ShippingServiceF() 
            : base(new List<Models.BulkPurchaseDiscountModel>()
            {
                new Models.BulkPurchaseDiscountModel(2, 15)
            })
        {

        }
    }
}
