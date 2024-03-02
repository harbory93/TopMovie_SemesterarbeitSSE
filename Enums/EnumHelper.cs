using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TopMovie_SemesterarbeitSSE.Enums
{
    public class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetSelectListForEnum<TEnum>()
        where TEnum : struct, IConvertible // Ensure TEnum is an enum
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumeration type");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => new SelectListItem {
                Value = e.ToString(),
                Text = e.GetType()
                        .GetMember(e.ToString())
                        .FirstOrDefault()?
                        .GetCustomAttribute<DisplayAttribute>()?
                        .GetName() ?? e.ToString()
            });
        }
    }
}
