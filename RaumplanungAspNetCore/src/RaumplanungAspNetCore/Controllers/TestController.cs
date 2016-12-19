using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RaumplanungAspNetCore.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("{wert}", Name = "Add")]
        public int GetTest(int wert)
        {
            wert = wert++;
            return wert;
        }
    }
}