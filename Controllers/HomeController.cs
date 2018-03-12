using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

namespace mvc_layout_core.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

		[HttpGet]
		[Route("pokemon/{pokeid}"), Route("pokemon")]
		public IActionResult PokemonApi(int pokeid){
			var PokeInfo = new Dictionary<string, object>();
			
			WebRequest.GetPokeWithCallBack(pokeid, pokeJson => {
				PokeInfo = pokeJson;
			}).Wait();
			//. When we use .Wait() with async methods we do lose the benefits of asynchronous processing. 
			//  This is acceptable in situations when our main thread needs the async operation to complete before it can continue in any case.
			ViewBag.poke = PokeInfo;
			return View();
		}
    }
}
