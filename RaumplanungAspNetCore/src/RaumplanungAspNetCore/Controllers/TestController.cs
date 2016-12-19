using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RaumplanungAspNetCore.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public String getTestString()
        {
            return "TEST";
        }




        [HttpGet("{wert}")]
        public String GetTest(int wert)
        {
            wert = wert++;
            return wert+"";
        }
    }
}