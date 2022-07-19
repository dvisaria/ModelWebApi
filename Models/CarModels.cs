using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace CarWebApi.Models
{
    [Serializable]
    [Table(Name = "car")]
    public class CarModels : IValidatableObject
    {
        [Column(Name = "id")]
        public int? Id { get; set; }

        [Required]
        [Column(Name = "name")]
        public string Name { get; set; }

        [Required]
        [Column(Name = "color")]
        public string Color { get; set; }

        [Required]
        [Range(1800,2999)]
        [Column(Name = "year")]
        public int Year {  get; set; }

        public CarModels(int id, string name, string color, int year)
        {
            Id = id;
            Name = name;
            Color = color;
            Year = year;
        }

        public CarModels(CarModels car)
        {
            Name = car.Name;
            Color = car.Color;
            Year = car.Year;
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

            if ( Year < 1800 || Year > DateTime.Now.Year)
            {
                yield return new ValidationResult("InvalidYear");
            }

        }
    }
}