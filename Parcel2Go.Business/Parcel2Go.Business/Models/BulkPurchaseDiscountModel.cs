using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Business.Models
{
    public record BulkPurchaseDiscountModel
    {
        public BulkPurchaseDiscountModel(int numberOfItemRequiredForDiscount, int discountedPrice)
        {
            this.NumberOfItemRequiredForDiscount = numberOfItemRequiredForDiscount;
            this.DiscountedPrice = discountedPrice;
        }

        public int NumberOfItemRequiredForDiscount { get;}

        public int DiscountedPrice { get; }

    }
}
