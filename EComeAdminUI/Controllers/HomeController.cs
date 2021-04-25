using EComeAdminUI.Models;
using EComeAdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EComeAdminUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDeliveryStoreRepository _deliveryRepository;

        public HomeController(ILogger<HomeController> logger, IDeliveryStoreRepository deliveryRepository)
        {
            _logger = logger;
            _deliveryRepository = deliveryRepository;
        }


        public Task<List<DeliveryStore>> Index()
        {
            var model = _deliveryRepository.GetAll();
            return model;
        }


        //public ViewResult Index()
        //{
        //    var model = _deliveryRepository.GetAll();
        //    return View(model);
        //}

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DeliveryStore deliveryStore)
        {
            if (ModelState.IsValid)
            {
                var newDeliveryStore = _deliveryRepository.AddDeliveryStore(deliveryStore);
                return RedirectToAction("Details", new { id = newDeliveryStore.Id });
            }
            return View();
        }

        public ViewResult Details()
        {
            DeliveryStoreViewModel deliveryStoreViewModel = new DeliveryStoreViewModel()
            {
                DeliveryStore = _deliveryRepository.GetDeliveryStore("L4T"),
                PageTitle = "Delivery Store Details"
            };
            DeliveryStore model = _deliveryRepository.GetDeliveryStore("L4T");
            ViewData["PagaTitle"] = "Delivery Store Details";
            return View(deliveryStoreViewModel);
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
}
