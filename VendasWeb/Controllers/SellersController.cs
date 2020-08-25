using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using VendasWeb.Models;
using VendasWeb.Models.ViewModels;
using VendasWeb.Services;
using VendasWeb.Services.Exceptions;

namespace VendasWeb.Controllers
{

    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depar = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = depar };
                return View(viewModel);
            }

            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            return View(obj);
        }

        public IActionResult Edit(int? id) //get
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depar = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = depar };
                return View(viewModel);
            }

            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem." });

            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));

            }
            catch (ApplicationException a)
            {
                return RedirectToAction(nameof(Error), new { message = a.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}