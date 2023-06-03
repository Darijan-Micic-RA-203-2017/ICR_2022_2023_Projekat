using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.PopulatedPlace
{
    public class UniquePopulatedPlaceIdValidationRule : ValidationRule
    {
        private PopulatedPlacesViewModel populatedPlacesViewModel;

        public UniquePopulatedPlaceIdValidationRule()
        {
            populatedPlacesViewModel = new PopulatedPlacesViewModel();
        }

        public PopulatedPlacesViewModel PopulatedPlacesViewModel
        {
            get { return populatedPlacesViewModel; }
            set { populatedPlacesViewModel = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var enteredId = value as string;
            if (string.IsNullOrWhiteSpace(enteredId))
            {
                return new ValidationResult(false, "Oznaka grada mora biti uneta!");
            }

            foreach (Model.PopulatedPlace pPlace in populatedPlacesViewModel.PopulatedPlaces)
            {
                if (pPlace.Id.Equals(enteredId))
                {
                    return new ValidationResult(false, "Već postoji grad sa unetom oznakom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
