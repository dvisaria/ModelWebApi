using CarWebApi.Dto;
using CarWebApi.Models;
using ModelWebApi.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ModelWebApi.DAL
{
    public class CarInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CarContext>
    {
        protected override void Seed(CarContext context)
        {
            var cars = new List<Car>
            {
                new Car { Name="Toyota Camry", Color="Black", Year=2017 },         
                new Car { Name="Toyota HighLander", Color="Black", Year=2017 }
            
            };

            cars.ForEach(c => context.Cars.Add(c));
            context.SaveChanges();
            
        }
    }
}