using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Core.Enums
{
    public enum ShippingServiceType
    {
        [Description("Service A")]
        ServiceA,
        [Description("Service B")]
        ServiceB,
        [Description("Service C")]
        ServiceC,
        [Description("Service D")]
        ServiceD,
        // NOTE: No Service E is listed in the docs. 
        [Description("Service F")]
        ServiceF
    }
}
