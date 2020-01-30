using System;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApiCall
{
    public class Article
    {
        public string Section { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public string Byline { get; set; }
    }
  class Program
  {
    static void Main(string[] args)
    {
      var apiCallTask = ApiHelper.ApiCall("W4PwG8Tww8rrdX6fKDA3llvHyGnH6YGS");
      var result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

            foreach (Article article in articleList)
            {
                Console.WriteLine($"Section: {article.Section}");
                Console.WriteLine($"Title: {article.Title}");
                Console.WriteLine($"Abstract: {article.Abstract}");
                Console.WriteLine($"Url: {article.Url}");
                Console.WriteLine($"Byline: {article.Byline}");
            }
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}