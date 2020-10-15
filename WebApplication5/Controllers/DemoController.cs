using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GovTown.Core.Data;
using Microsoft.Extensions.Logging;
using Model;
using Microsoft.Extensions.DependencyInjection;

namespace WebApplication5.Controllers
{
    /// <summary>
    /// webapi 测试钩子1
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<DemoController> _logger;
        private IRepository<Course> repository;// = ServiceProvider.<IRepository<Course>>()

        public DemoController(ILogger<DemoController> logger, IRepository<Course> efRepository)
        {
            _logger = logger;
            repository = efRepository;
        }

        /// <summary>
        /// 获得所有天气列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("add")]
        public string Add()
        {
            Course course = new Course { Credits = 1, Title = "dd" };
            repository.Insert(course);
            return "ok";
        }

    }
}
