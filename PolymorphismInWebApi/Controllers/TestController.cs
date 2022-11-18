using Microsoft.AspNetCore.Mvc;
using PolymorphismInWebApi.Models;

namespace PolymorphismInWebApi.Controllers;

[Route("v1/test")]
public class TestController : Controller
{
    [HttpPost()]
    public Guid Create([FromBody]CreateCommand command)
    {
        return Guid.NewGuid();
    }
}
