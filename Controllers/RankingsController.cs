using MobileArts.api.Domain.Services;
using MobileArts.api.Persistence.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using NLog.Fluent;
using MobileArts.api.Domain.Models;
using MobileArts.api.Services;
using MobileArts.api.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MobileArts.api.Controllers
{
    [Route("/mobilearts")]
    public class RankingsController : Controller
    {

        public RankingsController()
        {
      
        }


        [HttpGet("mytest")]
        public string testtfn()
        {
            return "test";
        }


        [HttpGet("rankingstest/os/{os}/date/{date}/country/{cn}")]
        public async Task<HttpResponseMessage> GetDataFromAPI(string os, string date, string cn)
        {
            using (var client = new HttpClient())
            {
                String userName = "48fe7725e4e1aec2213707375f1efa0b143037df";
                String passWord = "mypassword";
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{passWord}")));
                // Add the auth header to the client
                client.DefaultRequestHeaders.Authorization = authValue;
                string url = "https://api.appmonsta.com/v1/stores/" + os + "/rankings.json?date=" + date + "&country=" + cn;
                var response = await client.GetAsync(url);
          
                return response;
            }
        }
        [HttpGet("rankings/os/{os}/date/{date}/country/{cn}")]
        public async Task<AppRankings[]> GetDataFromAPI3(string os, string date, string cn)
        {
            using (var client = new HttpClient())
            {
                String userName = "48fe7725e4e1aec2213707375f1efa0b143037df";
                String passWord = "mypassword";
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{passWord}")));
                // Add the auth header to the client
                client.DefaultRequestHeaders.Authorization = authValue;
                string url = "https://api.appmonsta.com/v1/stores/" + os + "/rankings.json?date=" + date + "&country=" + cn;
                var response = await client.GetAsync(url);
                var stream = await response.Content.ReadAsStreamAsync();
                var reader = new JsonTextReader(new StreamReader(stream)) { SupportMultipleContent = true };
                var serializer = new JsonSerializer();
                var objects = new List<AppRankings>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var obj = serializer.Deserialize<AppRankings>(reader);
                        objects.Add(obj);
                    }
                }
                return objects.ToArray();
            }
        }

        [HttpGet("singleapp/{id}/os/{os}/country/{cn}")]
        public async Task<AppRankings[]> getDetailsforSingleApp2(string id, string os, string cn)
        {
            using (var client = new HttpClient())
            {
                String userName = "48fe7725e4e1aec2213707375f1efa0b143037df";
                String passWord = "mypassword";
                var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{passWord}")));
                // Add the auth header to the client
                client.DefaultRequestHeaders.Authorization = authValue;
                string url = "https://api.appmonsta.com/v1/stores/" + os + "/details/" + id + ".json?country=" + cn;
                var response = await client.GetAsync(url);
                var stream = await response.Content.ReadAsStreamAsync();
                var reader = new JsonTextReader(new StreamReader(stream)) { SupportMultipleContent = true };
                var serializer = new JsonSerializer();
                var objects = new List<AppRankings>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var obj = serializer.Deserialize<AppRankings>(reader);
                        objects.Add(obj);
                    }
                }
                return objects.ToArray();
            }
        }

        [HttpGet("singleapptest/{id}/os/{os}/country/{cn}")]
        public string getDetailsforSingleApp(string id, string os, string cn)
        {
            string url = "https://api.appmonsta.com/v1/stores/"+os+"/details/"+id+".json?country="+cn;
            WebClient client = new WebClient();
            String userName = "48fe7725e4e1aec2213707375f1efa0b143037df";
            String passWord = "mypassword";
            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + passWord));
            client.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            var result = client.DownloadString(url);
            return result;
        }

    }
}
