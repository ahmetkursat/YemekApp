﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SignalRWebUI.Dtos.AboutDtos;
using System.Text;

namespace SignalRWebUI.Controllers
{
	public class AboutController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

			public AboutController(IHttpClientFactory httpClientFactory)
			{
				_httpClientFactory = httpClientFactory;
			}

			public async Task<IActionResult> Index()
			{
				var client = _httpClientFactory.CreateClient();
				var responsemessage = await client.GetAsync("https://localhost:7249/api/About");
				if (responsemessage.IsSuccessStatusCode)
				{
					var jsonData = await responsemessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
					return View(values);
				}
				// API çağrısı hatasını ele al (örneğin, bir hata görünümü döndür, hatayı kaydet)
				return View(); // Veya belirli bir hata görünümü modeli döndür

			}
			[HttpGet]
			public IActionResult CreateAbout()
			{
				return View();
			}

			[HttpPost]
			public async Task<IActionResult> CreateAbout(CreateBookingDto createAboutDto)
			{
				var client = _httpClientFactory.CreateClient();
				var jsonData = JsonConvert.SerializeObject(createAboutDto);
				StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
				var responsoMessage = await client.PostAsync("https://localhost:7249/api/About", stringContent);
				if (responsoMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View();

			}
			public async Task<IActionResult> DeleteAbout(int id)
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.DeleteAsync($"https://localhost:7249/api/About/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View();
			}

			public async Task<IActionResult> UpdateAbout(int id)
			{
				var client = _httpClientFactory.CreateClient();
				var responseMessage = await client.GetAsync($"https://localhost:7249/api/About/{id}");
				if (responseMessage.IsSuccessStatusCode)
				{
					var jsondata = await responseMessage.Content.ReadAsStringAsync();
					var values = JsonConvert.DeserializeObject<UpdateAboutDto>(jsondata);
					return View(values);
				}
				return View();
			}
			[HttpPost]
			public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
			{
				var client = _httpClientFactory.CreateClient();
				var jsondata = JsonConvert.SerializeObject(updateAboutDto);
				StringContent stringContent = new StringContent(jsondata, Encoding.UTF8, "application/json");
				var responseMessage = await client.PutAsync($"https://localhost:7249/api/About/", stringContent);
				if (responseMessage.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				return View();
			}
		}
	}
