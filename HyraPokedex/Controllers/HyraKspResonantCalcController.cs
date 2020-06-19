using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HyraPokedex.Controllers
{
    public class HyraKspResonantCalcController : Controller
    {
        public IActionResult Index()
        {
            // inputs
            Mun selectedCelBody = new Mun();
            double desiredApoapsis = 200000;
            double radius = selectedCelBody.radius;
            double mu = selectedCelBody.mu;

            // calculate semi major axis (alpha)
            double alpha = (2.0 * desiredApoapsis + 2.0 * radius) / 2.0;
            double desiredPeriod = 2.0 * Math.PI * Math.Sqrt(Math.Pow(alpha, 3) / mu);

            // get 2/3 of period
            double resonantDivePeriod = desiredPeriod * 2.0 / 3.0;

            // solve for resonant periapsis
            double baseProd = Math.Pow(resonantDivePeriod, 2.0) * mu / 4.0 / Math.Pow(Math.PI, 2.0);
            double resonantDivePeriapsis = 2.0 * Math.Pow(baseProd, 1.0 / 3.0) - 2.0 * radius - desiredApoapsis;

            ViewBag.DesiredPeriod = desiredPeriod;
            ViewBag.ResonantDivePeriod = resonantDivePeriod;
            ViewBag.ResonantDivePeriapsis = resonantDivePeriapsis;

            return View();
        }
    }



    public class Mun
    {
        public double radius = 200000;
        public double soi = 2429559.1;
        public double mu = 6.5138398e10;
    }
}