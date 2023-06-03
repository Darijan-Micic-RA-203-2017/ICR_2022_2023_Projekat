using ICR_2022_2023_Mapa_dogadjaja.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ICR_2022_2023_Mapa_dogadjaja.Validation.PopulatedPlace
{
    public class UniquePopulatedPlaceNameValidationRule : ValidationRule
    {
        private PopulatedPlacesViewModel populatedPlacesViewModel;

        public UniquePopulatedPlaceNameValidationRule()
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
            var enteredName = value as string;
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                return new ValidationResult(false, "Naziv grada mora biti unet!");
            }

            string firstCharacterOfEnteredName = enteredName.Substring(0, 1);
            if (firstCharacterOfEnteredName.Equals(firstCharacterOfEnteredName.ToLower()))
            {
                return new ValidationResult(false, "Naziv grada mora započeti velikim slovom!");
            }

            foreach (Model.PopulatedPlace pPlace in populatedPlacesViewModel.PopulatedPlaces)
            {
                if (pPlace.Name.Equals(enteredName))
                {
                    return new ValidationResult(false, "Već postoji grad sa unetim nazivom!");
                }
            }

            return new ValidationResult(true, null);
        }
    }
}
