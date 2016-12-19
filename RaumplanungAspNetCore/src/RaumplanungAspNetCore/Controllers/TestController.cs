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

        public IActionResult Index()
        {
            return View();
        }



        [Route("[controller]/String")]
        [HttpGet]
        public string getTestString()
        {
            return "TEST";
        }





        [Route("[controller]/Add")]
        [HttpGet("{wert}", Name= "Add" )]
        public int GetTest(int wert)
        {

            wert++;
            return wert;
        }
    }
}