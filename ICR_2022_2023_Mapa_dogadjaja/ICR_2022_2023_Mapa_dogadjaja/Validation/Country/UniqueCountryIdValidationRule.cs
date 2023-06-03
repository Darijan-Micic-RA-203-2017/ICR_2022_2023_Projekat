using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Country
{
    public class UniqueCountryIdValidationRule : ValidationRule
    {
        private CountriesViewModel countriesViewModel;

        public UniqueCountryIdValidationRule()
        {
            countriesViewModel = new CountriesViewModel();
        }

        public CountriesViewModel CountriesViewModel
        {
            get { return countriesViewModel; }
            set { countriesViewModel = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka mora biti uneta!");
            }

            foreach (Model.Country c in countriesViewModel.Countries)
            {
                if (c.Id.Equals(enteredId))
                {
                    return new ValidationResult(false, "Već postoji država sa unetom oznakom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
