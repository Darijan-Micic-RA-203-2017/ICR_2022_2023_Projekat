using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace ICR_2022_2023_Mapa_dogadjaja.ViewModel
{
    // REFERENCE: https://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    public class EnumBindingSourceExtension : MarkupExtension
    {
        private Type enumType;
        
        public EnumBindingSourceExtension() { }

        public EnumBindingSourceExtension(Type enumType)
        {
            EnumType = enumType;
        }

        public Type EnumType
        {
            get { return enumType; }
            set
            {
                if (value != enumType)
                {
                    if (value != null)
                    {
                        Type typeofValue = Nullable.GetUnderlyingType(value) ?? value;

                        if (!typeofValue.IsEnum)
                        {
                            throw new ArgumentException("Type must be for an Enum!");
                        }
                    }

                    enumType = value;
                }
            }
        }
        
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (enumType == null)
            {
                throw new InvalidOperationException("The EnumType must be specified!");
            }

            Type actualEnumType = Nullable.GetUnderlyingType(enumType) ?? enumType;
            Array enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == enumType)
            {
                return enumValues;
            }

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);

            return tempArray;
        }
    }
}
