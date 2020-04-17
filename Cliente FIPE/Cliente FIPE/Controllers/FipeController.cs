using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cliente_FIPE.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cliente_FIPE.Controllers
{
	public class FipeController : Controller
    {
		private string uriMarca = "http://fipeapi.appspot.com/api/1/carros/marcas.json";
		private string uriVeiculos = "http://fipeapi.appspot.com/api/1/carros/veiculos/idMarca.json";
		private string uriVeiculoModelos = "http://fipeapi.appspot.com/api/1/carros/veiculo/idMarca/idVeiculo.json";
		private string uriModeloDetalhes = "http://fipeapi.appspot.com/api/1/carros/veiculo/idMarca/idVeiculo/idModelo.json";


		// GET: Fipe
		public async Task<IActionResult> Index()
        {
			List<Marca> marcas = new List<Marca>();
			var httpClient = new HttpClient();
			using (var response = await httpClient.GetAsync(uriMarca) )
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				marcas = JsonConvert.DeserializeObject<List<Marca>>(apiResponse);
			}
			ViewBag.Marcas = marcas.Select(m => new SelectListItem() { Text = m.Fipe_name, Value = m.Id.ToString() });

			return View(marcas);
        }

		public async Task<IActionResult> CarregarCarros(string idmarca, string veiculo = null)
		{
			if(String.IsNullOrEmpty(veiculo))
				veiculo = "";
			var veiculos = new List<Marca>();
			var httpClient = new HttpClient();
			var uri = uriVeiculos.Replace("idMarca", idmarca);

			using (var response = await httpClient.GetAsync(uri))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				veiculos = JsonConvert.DeserializeObject<List<Marca>>(apiResponse);
			}
			StringComparison comp = StringComparison.Ordinal;
			comp = StringComparison.OrdinalIgnoreCase;
			veiculos = veiculos.Where(v => v.Name.Contains(veiculo, comp)).ToList();

			return Json(veiculos);
		}

		public async Task<IActionResult> CarregarCarroModelos(string idmarca, string idveiculo)
		{
			var veiculoModelos = new List<VeiculoModelo>();
			var httpClient = new HttpClient();
			var uri = uriVeiculoModelos.Replace("idMarca", idmarca);
			uri = uri.Replace("idVeiculo", idveiculo);

			using (var response = await httpClient.GetAsync(uri))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				veiculoModelos = JsonConvert.DeserializeObject<List<VeiculoModelo>>(apiResponse);
			}

			return Json(veiculoModelos);
		}

		public async Task<IActionResult> CarregarModeloDetalhes(string idmarca, string idveiculo, string idmodelo)
		{
			var modeloDetalhes = new ModeloDetalhes();
			var httpClient = new HttpClient();
			var uri = uriModeloDetalhes.Replace("idMarca", idmarca);
			uri = uri.Replace("idVeiculo", idveiculo);
			uri = uri.Replace("idModelo", idmodelo);

			using (var response = await httpClient.GetAsync(uri))
			{
				string apiResponse = await response.Content.ReadAsStringAsync();
				modeloDetalhes = JsonConvert.DeserializeObject<ModeloDetalhes>(apiResponse);
			}

			return Json(modeloDetalhes);
		}





		public async Task<IActionResult> Teste(string veiculo, string idMarca)
		{
			var veiculos = new Object();

			return Json(veiculos);
		}



		// GET: Fipe/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Fipe/Create
        public ActionResult Create()
        {
			var marca = new Marca();
			List<string> lista = new List<string>();
			lista.Add("Item1");
			lista.Add("Item2");
			lista.Add("Item3");
			marca.Lista = lista;

			return View(marca);
        }

        // POST: Fipe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Fipe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fipe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Fipe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fipe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}