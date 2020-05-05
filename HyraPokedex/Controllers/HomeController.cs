using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HyraPokedex.Models.PokeApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HyraPokedex.Controllers
{
    public class HomeController : Controller
    {
        public  ActionResult Index()
        {
            return View();
        }
    }
}