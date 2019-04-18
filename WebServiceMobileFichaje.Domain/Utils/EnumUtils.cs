using System.ComponentModel;
using System.Reflection;
using WebServiceMobileFichaje.Domain.Services.Authorization;

namespace WebServiceMobileFichaje.Domain.Utils
{
    public static class EnumUtils
    {
        public static string GetEnumDescription(this UserLoginResultEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}