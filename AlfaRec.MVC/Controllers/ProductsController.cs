using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundConnRec.Models;
using FundConnRec.MVC.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundConnRec.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private ApiClient httpClient;

        public ProductsController(ApiClient client)
        {
            httpClient = client;
        }
        // GET: Products
        public async Task<ActionResult> Index()
        {
            try
            {
                var products = await httpClient.GetProducts();
                return View(products);
            }
            catch(Exception ex)
            {
                ViewBag["Message"] = ex.Message;
                return View();
            }
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                await httpClient.PostProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var product = await httpClient.GetProduct(id);
                return View(product);
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Product product)
        {
            try
            {
                await httpClient.PutProduct(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: Products/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await httpClient.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag["Message"] = ex.Message;
                return View("Index");
            }
        }
    }
}