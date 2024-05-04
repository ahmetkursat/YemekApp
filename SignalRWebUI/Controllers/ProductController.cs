using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.CategoryDtos;
using SignalRWebUI.Dtos.ProductDtos;

namespace SignalRWebUI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responsemessage = await client.GetAsync("https://localhost:7249/api/Product/ProductListWithCategory");
			if (responsemessage.IsSuccessStatusCode)
			{
				var jsondata = await responsemessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsondata);
				return View(values);
			}
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			var client = _httpClientFactory.CreateClient();
			var responsemessage = await client.GetAsync("https://localhost:7249/api/Category");
			var jsonData = await responsemessage.Content.ReadAsStringAsync();
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
			List<SelectListItem> values2 =(from x in values 
										   select new SelectListItem
										   {
											   Text = x.CategoryName,
											   Value = x.CategoryID.ToString(),

										   }).ToList();
			ViewBag.v = values2;

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			createProductDto.Status = true;
			var client = _httpClientFactory.CreateClient();
			var jsondata = JsonConvert.SerializeObject(createProductDto);
			StringContent stringContent = new StringContent(jsondata);
			var responsemessage = await client.PostAsync("https://localhost:7249/api/Product",stringContent);
            if (responsemessage.IsSuccessStatusCode)
            {
				return RedirectToAction("Index");
            }
			return View();
        }
		
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responsemessage = await client.DeleteAsync($"https://localhost:7249/api/Product/{id}");
			if(responsemessage.IsSuccessStatusCode)
			{
				return View("Index");
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> UpdateProduct(int id)
		{
			var client = _httpClientFactory.CreateClient();
			var responsemessage = await client.GetAsync($"https://localhost:7249/api/Product/{id}");
			if(responsemessage.IsSuccessStatusCode)
			{
				var jsonData = await responsemessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<UpdateProductDto>>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var client = _httpClientFactory.CreateClient();	
			var jsondata = JsonConvert.SerializeObject(updateProductDto);
			StringContent content = new StringContent(jsondata);
			var responsemessage = await client.PutAsync($"https://localhost:7249/api/Product/",content);
			if(responsemessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index");
			}
			return View();
		}
	
	}
}
