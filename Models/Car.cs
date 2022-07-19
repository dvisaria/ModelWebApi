using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelWebApi.Models
{
    public class Car
    {
        public Car()
        {

        }

        public Car(Car car)
        {
            ID = car.ID;
            Color = car.Color;
            Name = car.Name;
            Year = car.Year;
        }
        public int ID { get; set; }
        public string Name { get; set; }

        public string Color { get; set; }
        public int Year { get; set; }       

    }
}



