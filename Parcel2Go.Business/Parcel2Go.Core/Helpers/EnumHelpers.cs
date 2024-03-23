using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcel2Go.Core.Helpers
{
    public static class EnumHelpers
    {

        /// <summary>
        /// Attempts to get the matching enum by the description
        /// https://stackoverflow.com/a/50336830
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TEnum? GetEnumFromDescription<TEnum>(string description)where TEnum : struct, Enum
        {
            var comparison = StringComparison.OrdinalIgnoreCase;
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (string.Compare(attribute.Description, description, comparison) == 0)
                    {
                        var value = field.GetValue(null);
                        if(value != null)
                        {
                            return (TEnum)value;
                        }
                    }
                }
                if (string.Compare(field.Name, description, comparison) == 0)
                {
                    var value = field.GetValue(null);
                    if (value != null)
                    {
                        return (TEnum)value;
                    }
                }
                    
            }
            return null;
        }

    }
}
