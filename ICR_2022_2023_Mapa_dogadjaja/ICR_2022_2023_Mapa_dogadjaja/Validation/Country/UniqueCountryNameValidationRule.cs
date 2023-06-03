using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.Country
{
    public class UniqueCountryNameValidationRule : ValidationRule
    {
        private CountriesViewModel countriesViewModel;

        public UniqueCountryNameValidationRule()
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
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
            {
                return new ValidationResult(false, "Naziv mora započeti velikim slovom!");
            }

            foreach (Model.Country c in countriesViewModel.Countries)
            {
                if (c.Name.Equals(enteredName))
                {
                    return new ValidationResult(false, "Već postoji država sa unetim nazivom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
