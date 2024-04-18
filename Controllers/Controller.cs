using CarDealerShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CarDealerShopAPI.Controllers
{
    // READ 
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IMongoCollection<Car> _carCollection;
        private readonly ILogger<SearchController> _logger;

        public SearchController(ILogger<SearchController> logger, MongoDatabaseBase database)
        {
            _logger = logger;
            _carCollection = database.GetCollection<Car>("cars");
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "name")] string name = null, [FromQuery(Name = "brand")] string brand = null)
        {
            _logger.LogInformation($"Buscando carro(s) com nome: {name} e marca: {brand}");

            try
            {
                if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(brand))
                {
                    var cars = _carCollection.Find(_ => true).ToList();
                    return Ok(cars);
                }
                else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(brand))
                {
                    var cars = _carCollection.Find(c => c.Name == name).ToList();

                    if (cars == null || cars.Count == 0)
                    {
                        _logger.LogWarning($"Carro com nome {name} não encontrado");
                        return NotFound();
                    }

                    return Ok(cars);
                }
                else if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(brand))
                {
                    var cars = _carCollection.Find(c => c.Brand == brand).ToList();

                    if (cars == null || cars.Count == 0)
                    {
                        _logger.LogWarning($"Carro com marca {brand} não encontrado");
                        return NotFound();
                    }

                    return Ok(cars);
                }
                else
                {
                    var car = _carCollection.Find(c => c.Name == name && c.Brand == brand).SingleOrDefault();

                    if (car == null)
                    {
                        _logger.LogWarning($"Carro com nome {name} e marca {brand} não encontrado");
                        return NotFound();
                    }
                    return Ok(car);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao obter carro(s): {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

    }
    
    // ADD 
    [ApiController]
    [Route("[controller]")]
    public class AddVehicleController : ControllerBase
    {
        private readonly ILogger<AddVehicleController> _logger;
        private readonly IMongoCollection<Car> _carCollection;


        public AddVehicleController(ILogger<AddVehicleController> logger, MongoDatabaseBase database)
        {
            _logger = logger;
            _carCollection = database.GetCollection<Car>("cars");

        }

        [HttpPost]
        public IActionResult Post([FromBody] Car car)
        {
            try
            {
                _carCollection.InsertOne(car);

                _logger.LogInformation($"Veículo adicionado: {car.Brand} {car.Name}");
                return Ok("Veículo adicionado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao adicionar veículo: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

    }
    
    // DELETE 
    [ApiController]
    [Route("[controller]")]
    public class RemoveVehicleController : ControllerBase
    {
        private readonly ILogger<RemoveVehicleController> _logger;
        private readonly IMongoCollection<Car> _CarCollection;

        public RemoveVehicleController(ILogger<RemoveVehicleController> logger, MongoDatabaseBase database)
        {
            _logger = logger;
            _CarCollection = database.GetCollection<Car>("cars");
        }
        [HttpDelete]
        public IActionResult DeleteCar(string name, string brand)
        {
            try
            {
                var filter = Builders<Car>.Filter.Eq(c => c.Name, name) & Builders<Car>.Filter.Eq(c => c.Brand, brand);
                var result = _CarCollection.DeleteOne(filter);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning($"Carro com nome '{name}' e marca '{brand}' não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Carro com nome '{name}' e marca '{brand}' deletado com sucesso");
                return Ok($"Carro com nome '{name}' e marca '{brand}' deletado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao deletar carro: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

    }

    // UPDATE 
    [ApiController]
    [Route("[controller]")]

    public class UpdateVehicleController : ControllerBase
    {
        private readonly ILogger<UpdateVehicleController> _logger;
        private readonly IMongoCollection<Car> _CarCollection;

        public UpdateVehicleController(ILogger<UpdateVehicleController> logger, MongoDatabaseBase database)
        {
            _logger = logger;
            _CarCollection = database.GetCollection<Car>("cars");
        }
        [HttpPatch]
        public IActionResult UpdateCar(string name, string brand, [FromBody] Dictionary<string, object> fieldsToUpdate)
        {
            try
            {
                var filter = Builders<Car>.Filter.Eq(c => c.Name, name) & Builders<Car>.Filter.Eq(c => c.Brand, brand);
                var update = Builders<Car>.Update.Combine(fieldsToUpdate.Select(kv => Builders<Car>.Update.Set(kv.Key, kv.Value)));

                var result = _CarCollection.UpdateOne(filter, update);

                if (result.ModifiedCount == 0)
                {
                    _logger.LogWarning($"Carro com nome '{name}' e marca '{brand}' não encontrado");
                    return NotFound();
                }

                _logger.LogInformation($"Carro com nome '{name}' e marca '{brand}' atualizado com sucesso");
                return Ok($"Carro com nome '{name}' e marca '{brand}' atualizado com sucesso");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao atualizar carro: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }
    
    }
}
