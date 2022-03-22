using KhareedLo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KhareedLo.Controllers
{
    public class ConsumeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            List<Product> prodsList = new List<Product>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44349/api/GenericAPI/get-all-products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    
                    prodsList = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(prodsList);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product p)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/create-product");

            var response = client.PostAsJsonAsync<Product>("https://localhost:44349/api/GenericAPI/create-product", p);

            response.Wait();

            var test = response.Result;

            if(test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            
            return View("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Product p = null;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/get-product-by-id/");

            var response = client.GetAsync("https://localhost:44349/api/GenericAPI/get-product-by-id/" + id.ToString());

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Product>();

                display.Wait();

                p = display.Result;
            }

            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Product p = null;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/get-product-by-id/");

            var response = client.GetAsync("https://localhost:44349/api/GenericAPI/get-product-by-id/" + id.ToString());

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Product>();

                display.Wait();

                p = display.Result;
            }

            return View(p);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Product p)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/update-product");

            var response = client.PutAsJsonAsync<Product>("https://localhost:44349/api/GenericAPI/update-product", p);

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product p = null;

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/get-product-by-id/");

            var response = client.GetAsync("https://localhost:44349/api/GenericAPI/get-product-by-id/" + id.ToString());

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Product>();

                display.Wait();

                p = display.Result;
            }

            return View(p);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteProd(int id)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44349/api/GenericAPI/DeleteProductById/");

            var response = client.DeleteAsync("https://localhost:44349/api/GenericAPI/DeleteProductById/" + id.ToString());

            response.Wait();

            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Delete");
            }

        }



        //[HttpGet]
        //public IActionResult GetProductWithID(/*int id*/)
        //{
        //    //Product p = null;

        //    //client.BaseAddress = new Uri("https://localhost:44349/api/ProductAPII");

        //    //var response = client.GetAsync("ProductAPII?id=" +id.ToString());

        //    //response.Wait();

        //    //var test = response.Result;

        //    //if (test.IsSuccessStatusCode)
        //    //{
        //    //    var display = test.Content.ReadAsAsync<Product>();

        //    //    display.Wait();

        //    //    p = display.Result;
        //    //}

        //    return View();
        //}
    }
}
