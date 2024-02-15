using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Suma(decimal a, decimal b) => a + b;

        [HttpPost]
        public decimal Resta(Numbers numbers) => numbers.A - numbers.B;

        [HttpPut]
        public decimal Multi(decimal a, decimal b) => a * b;

        [HttpDelete]
        public decimal Divi(decimal a, decimal b) => a / b;

    }

    public class Numbers 
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }


}
