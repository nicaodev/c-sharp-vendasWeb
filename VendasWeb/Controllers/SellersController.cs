using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using VendasWeb.Models;
using VendasWeb.Models.ViewModels;
using VendasWeb.Services;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAll();

            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depar = await _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = depar };
                return View(viewModel);
            }

            await _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido." });

            var obj = await _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = await _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) //get
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "Id não fornecido" });

            var obj = await _sellerService.FindById(id.Value);

            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe." });

            List<Department> departments = await _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var depar = await _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = depar };
                return View(viewModel);
            }

            if (id != seller.Id)
                return RedirectToAction(nameof(Error), new { message = "Ids não correspondem." });

            try
            {
                await _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException a)
            {
                return RedirectToAction(nameof(Error), new { message = a.Message });
            }
        }

        public IActionResult Error(string message) // Não tem acesso a dados, não precisa ser assíncrona.
        {
            var viewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}