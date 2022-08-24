using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ConsoleApiCall.Models;

namespace ConsoleApiCall
{
    class Program
    {
        static void Main()
        {
          var apiCallTask = ApiHelper.ApiCall(EnvironmentVariables.ApiKey);
          var result = apiCallTask.Result;

          JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
          List<Movie> movieList = JsonConvert.DeserializeObject<List<Movie>>(jsonResponse["results"].ToString());

          foreach (Movie movie in movieList)
            {
                Console.WriteLine($"Title: {movie.Display_Title}");
                Console.WriteLine($"Rating: {movie.Mpaa_Rating}");
                Console.WriteLine($"Headline: {movie.Headline}");
                Console.WriteLine($"Summary: {movie.Summary_Short}");
            }

        }
    }

    class ApiHelper
    {
      public static async Task<string> ApiCall(string apiKey)
      {
        RestClient client = new RestClient("https://api.nytimes.com/svc/movies/v2");
        RestRequest request = new RestRequest($"reviews/picks.json?api-key={apiKey}", Method.GET);
        var response = await client.ExecuteTaskAsync(request);
        return response.Content;
      }
    }
}
