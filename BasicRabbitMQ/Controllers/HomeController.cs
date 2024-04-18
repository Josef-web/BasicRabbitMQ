using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BasicRabbitMQ.Models;
using BasicRabbitMQ.RabbitMQ;

namespace BasicRabbitMQ.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRabbitMQPublisher _rabbitMQ;
    
    public HomeController(ILogger<HomeController> logger, IRabbitMQPublisher rabbitMq)
    {
        _logger = logger;
        _rabbitMQ = rabbitMq;
    }

    public IActionResult Index()
    {
        var student = new Student { Id = 1, FirstName = "Yusuf", LastName = "YAMAC" };
        _rabbitMQ.SendMessage<Student>(student);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}