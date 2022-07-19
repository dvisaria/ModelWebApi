using System.Linq;
using System.Web.Http;
using CarWebApi.Dto;
using ModelWebApi.DAL;
using ModelWebApi.Models;

namespace CarWebApi.Controllers
{
    [Authorize]
    public class CarsController : ApiController
    {
        private CarContext db;


        public CarsController(CarContext context)
        {
            db = context;            
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var cars = db.Cars.ToList<Car>();            
            return Ok(cars);
        }

        [HttpGet]
        public IHttpActionResult GetByYear([FromUri] int year)
        {
            var result = db.Cars.Where<Car>(c => c.Year == year).ToList<Car>();

            return Ok(result);
        }

        
        [HttpPost]

        public IHttpActionResult Create([FromBody] CarDto car)
        {
            if (ModelState.IsValid)
            {
                var result = db.Cars.FirstOrDefault<Car>(c => c.Name == car.Name && c.Year == car.Year && c.Color == car.Color);
                if (result == null)
                {

                    Car newCar = new Car { Name = car.Name, Color = car.Color, Year = car.Year };
                    Car saveCar = db.Cars.Add(newCar);
                    int count = db.SaveChanges();                    
                    if( count > 0 )
                    {
                        return Ok(saveCar);
                    }
                }
                
                return BadRequest("Error Creating Car");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
            

        [HttpPatch]
        public IHttpActionResult Update([FromBody] CarUpdateDto cardto)
        {
            var car = db.Cars.FirstOrDefault<Car>(c => c.ID == cardto.Id);

            
            if (car != null)
            {
                var changeCar = new Car(car);

                if (!string.IsNullOrEmpty(cardto.Name))
                {
                    changeCar.Name = cardto.Name;
                }

                if (!string.IsNullOrEmpty(cardto.Color))
                {
                    changeCar.Color = cardto.Color;
                }

                if (cardto.Year != null)
                {
                    changeCar.Year = (int)cardto.Year;
                }

                var result = db.Cars.FirstOrDefault(c => c.Name == changeCar.Name && c.Year == changeCar.Year && c.Color == changeCar.Color);
                if (result == null)
                {   
                    car.Color = changeCar.Color;
                    car.Name = changeCar.Name;
                    car.Year = changeCar.Year;
                    db.SaveChanges();
                    return Ok(car);
                }
            }

            return BadRequest("Error:Duplicate car record");

        }


        
        public Car GetByName(string name)
        {
            var car = db.Cars.FirstOrDefault<Car>(c => c.Name == name);

            if (car != null)
            {
                return car;
            }

            return null;

        }

        public Car GetById(int id)
        {
            var car = db.Cars.FirstOrDefault<Car>(c => c.ID == id);

            if (car != null)
            {
                return car;
            }

            return null;

        }

        public bool DeleteById(int id)
        {
            var car = db.Cars.SingleOrDefault<Car>(c => c.ID == id);

            if (car != null)
            {
                db.Cars.Remove(car);
                db.SaveChanges();
                return true;
            }

            return false;

        }
    }
}
