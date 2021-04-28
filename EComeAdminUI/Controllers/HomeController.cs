using EComeAdminUI.Models;
using EComeAdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nest;

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


        public async Task<ViewResult> Index()
        {
            var model = await _deliveryRepository.GetAll();
            return View(model);
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [HttpGet]
        public async Task<ViewResult> Edit(string fsa)
        {
            ViewData["PagaTitle"] = "Edit Delivery Store";

            var model = await _deliveryRepository.GetDeliveryStoreByFSA(fsa);

            DeliveryStoreEditViewModel deliveryStoreEditViewModel = new()
            {
                //Id=model.Id,
                //FSA=model.FSA,
                StoreNumber = model.StoreNumber,
                DeliveryVendorId = model.DeliveryVendorId,
                DeliveryVendorName = model.DeliveryVendorName,
                DeliveryFeePLU = model.DeliveryFeePLU,
                DeliveryFeePromo = model.DeliveryFeePromo,
                ClientCode = model.ClientCode
            };

            return View(deliveryStoreEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DeliveryStore deliveryStore)
        {
            if (ModelState.IsValid)
            {
                var changeDeliveryStore = _deliveryRepository.UpdateDeliveryStore(deliveryStore);
                return RedirectToAction("Index", new { id = changeDeliveryStore.Id });
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create(DeliveryStore deliveryStore)
        {
            if (ModelState.IsValid)
            {
                var newDeliveryStore = _deliveryRepository.AddDeliveryStore(deliveryStore);
                return RedirectToAction("Index", new { id = newDeliveryStore.Id });
            }
            return View();
        }


        public async Task<ViewResult> Details(string id)
        {

            var model = await _deliveryRepository.GetDeliveryStoreById(id);

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var deliveryStoreForDelete = await _deliveryRepository.GetDeliveryStoreById(id);
            if(deliveryStoreForDelete == null)
            {
                return BadRequest();
            }

            var response = await _deliveryRepository.DeleteDeliveryStore(id);

            return RedirectToAction("Index", new { id = deliveryStoreForDelete.Id });

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
