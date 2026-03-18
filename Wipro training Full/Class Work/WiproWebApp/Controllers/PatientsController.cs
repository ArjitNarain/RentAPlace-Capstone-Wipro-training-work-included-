using Microsoft.AspNetCore.Mvc;using System.Security.Cryptography.Pkcs;using WiproWebApp.Models;namespace WiproWebApp.Controllers{
    public class PatientsController : Controller    {        public IActionResult Index()        {            Patient p1 = new Patient()            {                Id = 1,                Name = "Kumar"            };            Patient p2 = new Patient()            {                Id = 2,                Name = "Preethi"            };            Patient p3 = new Patient()            {                Id = 3,                Name = "Goutham"            };            List<Patient> patients = new List<Patient>()            {             p1,p2,p3            };            return View(patients);        }        public IActionResult Create()        {
            return View();

        }        [HttpPost]        public IActionResult Create(Patient p)        {
            return RedirectToAction("Index");
        }

    }
}