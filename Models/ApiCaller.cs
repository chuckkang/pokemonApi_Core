using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mvc_layout_core
{
	public class WebRequest
	{
		// The second parameter is a function that returns a Dictionary of string keys to object values.
		// If an API returned an array as its top level collection the parameter type would be "Action>"
		public static async Task GetPokemonDataAsync(int PokeId, Action<Dictionary<string, object>> Callback)
		{
			// Create a temporary HttpClient connection.
			using (var Client = new HttpClient())
			{
				try
				{
					Client.BaseAddress = new Uri($"http://pokeapi.co/api/v2/pokemon/{PokeId}");
					HttpResponseMessage Response = await Client.GetAsync(""); // Make the actual API call.
					Response.EnsureSuccessStatusCode(); // Throw error if not successful.
					string StringResponse = await Response.Content.ReadAsStringAsync(); // Read in the response as a string.

					// Then parse the result into JSON and convert to a dictionary that we can use.
					// DeserializeObject will only parse the top level object, depending on the API we may need to dig deeper and continue deserializing
					Dictionary<string, object> JsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(StringResponse);

					// Finally, execute our callback, passing it the response we got.
					Callback(JsonResponse);
				}
				catch (HttpRequestException e)
				{
					// If something went wrong, display the error.
					Console.WriteLine($"Request exception: {e.Message}");
				}
			}
		}

		public static async Task GetPokeWithCallBack(int pokeid, Action<Dictionary<string, object>> Callback)
		{
			// set up location
			using (var pokeApi = new HttpClient())
			{
				//set up url
				try{
					pokeApi.BaseAddress = new Uri($"http://pokeapi.co/api/v2/pokemon/{pokeid}");
				HttpResponseMessage response = await pokeApi.GetAsync(""); // execute the command to get it
				// check if the status is good, otherwise throw a code;
				response.EnsureSuccessStatusCode();
				string strReturned = await response.Content.ReadAsStringAsync(); //this will read the content then put into a string......
				// change it to a dictionary
				//Console.WriteLine(strReturned + "---returned");
				Dictionary<string, object> pokeJson = JsonConvert.DeserializeObject<Dictionary<string, object>>(strReturned);
				Callback(pokeJson);
				}

				catch (Exception e)
				{
					// If something went wrong, display the error.
					Console.WriteLine($"Request exception: {e.Message}");
				}
				

			}
		}
	}
}