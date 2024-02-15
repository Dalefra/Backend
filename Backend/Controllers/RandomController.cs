using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomSingleton;
        private IRandomService _randomScope;
        private IRandomService _randomTransient;
        private IRandomService _random2Singleton;
        private IRandomService _random2Scope;
        private IRandomService _random2Transient;

        public RandomController
        (
            [FromKeyedServices("RandomSingleton")]IRandomService randomSingleton, 
            [FromKeyedServices("RandomScoped")]IRandomService randomScope, 
            [FromKeyedServices("RandomTransient")]IRandomService randomTransient,
            [FromKeyedServices("RandomSingleton")] IRandomService random2Singleton,
            [FromKeyedServices("RandomScoped")] IRandomService random2Scope,
            [FromKeyedServices("RandomTransient")] IRandomService random2Transient
        )
        {
            _randomSingleton = randomSingleton;
            _randomScope = randomScope;
            _randomTransient = randomTransient;
            _random2Singleton = random2Singleton;
            _random2Scope = random2Scope;
            _random2Transient = random2Transient;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get() 
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton", _randomSingleton.Value);
            result.Add("Scope", _randomScope.Value);
            result.Add("Transient", _randomTransient.Value);
            result.Add("Singleton2", _random2Singleton.Value);
            result.Add("Scope2", _random2Scope.Value);
            result.Add("Transient2", _random2Transient.Value);

            return result;

        }



    }
}
