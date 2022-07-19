using CarWebApi.Controllers;
using CarWebApi.Dto;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using ModelWebApi.DAL;
using ModelWebApi.Models;

namespace CarWebApi.Tests.Controllers
{
    [TestClass]
    public class CarControllerTest
    {
        CarContext db = new CarContext();
        public CarControllerTest()
        {

        }
        [TestMethod]
        public void GetAll()
        {
            // Arrange            
            CarsController controller = new CarsController(db);

            // Act
            IHttpActionResult result = controller.GetAll();
            var contentResult = result as OkNegotiatedContentResult<List<Car>>;
            var cars = contentResult.Content;

            // Assert
            Assert.IsNotNull(cars);

        }



        [TestMethod]
        public void Create()
        {
            // Arrange            
            CarsController controller = new CarsController(db);

            // Act
            IHttpActionResult result = controller.Create(new CarDto("Test123", "Red", 2022));

            // Assert
            var contentResult = result as OkNegotiatedContentResult<Car>;
            var car = contentResult.Content;
            Assert.IsNotNull(car.ID);
            Assert.AreEqual("Test123", car.Name);
            Assert.AreEqual("Red", car.Color);
            Assert.AreEqual(2022, car.Year);
            controller.DeleteById(car.ID);
        }

        [TestMethod]
        public void Update()
        {

            // Arrange
            CarsController controller = new CarsController(db);
            IHttpActionResult result = controller.Create(new CarDto("Test456", "Red", 2022));
            Car carToUpdate = controller.GetByName("Test456");

            // Act
            result = controller.Update(new CarUpdateDto(carToUpdate.ID, null, "Blue", null) );

            // Assert
            var contentResult = result as OkNegotiatedContentResult<Car>;
            var car = contentResult.Content;
            Assert.AreEqual("Blue", car.Color);
            controller.DeleteById(carToUpdate.ID);

        }


        [TestMethod]
        public void GetByYear()
        {

            // Arrange
            CarsController controller = new CarsController(db);
            IHttpActionResult result = controller.Create(new CarDto("Test456", "Red", 3000));
            result = controller.Create(new CarDto("Test456", "Blue", 3000));
            result = controller.Create(new CarDto("Test456", "Green", 3000));
            result = controller.GetByYear(3000);
            var contentResult = result as OkNegotiatedContentResult<List<Car>>;
            List<Car> cars = contentResult.Content as List<Car>;

            
            // Assert
            Assert.AreEqual(3, cars.Count);
            cars.ForEach(c => db.Cars.Remove(c));
            db.SaveChanges();

        }


    }
}
