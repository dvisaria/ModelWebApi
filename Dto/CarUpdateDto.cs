using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarWebApi.Dto
{
    public class CarUpdateDto : IValidatableObject
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public String Color { get; set; }
        public int? Year { get; set; }

        public CarUpdateDto(int id, string name, string color, int? year)
        {
            Id = id;
            Name = name;
            Color = color;
            Year = year;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (String.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult("InvalidName");
            }

            if (String.IsNullOrEmpty(Color))
            {
                yield return new ValidationResult("InvalidColor");
            }

            if (Year == null || Year < 1800 || Year > DateTime.Now.Year)
            {
                yield return new ValidationResult("InvalidYear");
            }

        }


    }
}