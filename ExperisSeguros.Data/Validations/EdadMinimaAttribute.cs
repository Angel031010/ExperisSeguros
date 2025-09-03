using System;
using System.ComponentModel.DataAnnotations;

namespace ExperisSeguros.Data.Validations
{
    public class EdadMinimaAttribute : ValidationAttribute
    {
        private readonly int _edadMinima;

        public EdadMinimaAttribute(int edadMinima)
        {
            _edadMinima = edadMinima;
            ErrorMessage = $"El usuario debe tener al menos {edadMinima} años";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime fechaNacimiento)
            {
                var edad = DateTime.Today.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) 
                    edad--;

                if (edad >= _edadMinima)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}