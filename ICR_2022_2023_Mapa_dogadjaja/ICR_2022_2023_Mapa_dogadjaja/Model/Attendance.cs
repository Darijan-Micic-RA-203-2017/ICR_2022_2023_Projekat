using ICR_2022_2023_Mapa_dogadjaja.Converter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ICR_2022_2023_Mapa_dogadjaja.Model
{
    // REFERENCE: https://brianlagunas.com/a-better-way-to-data-bind-enums-in-wpf/
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Attendance
    {
        [Description("Do 1000")]
        BELOW_1000,
        [Description("1000 - 5000")]
        FROM_1000_TO_5000,
        [Description("5000 - 10000")]
        FROM_5000_TO_10000,
        [Description("Preko 10000")]
        OVER_10000
    }
}
