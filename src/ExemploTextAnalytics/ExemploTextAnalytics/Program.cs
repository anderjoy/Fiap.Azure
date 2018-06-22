using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace ExemploTextAnalytics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Como foi sua experiência em nossa loja?");
            string opiniao = Console.ReadLine();

            var doc = new
            {
                documents = new[]
                {
                    new
                    {
                        id = 1,
                        text = opiniao
                        //language = "en"
                    }
                }
            };

            var json = JsonConvert.SerializeObject(doc);

            //endpoint
            //var endpoint = "https://brazilsouth.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
            //var endpoint = "https://brazilsouth.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";
            //var endpoint = "https://brazilsouth.api.cognitive.microsoft.com/text/analytics/v2.0/entities";
            var endpoint = "https://brazilsouth.api.cognitive.microsoft.com/text/analytics/v2.0/languages";

            //primary key
            var key = "514ae7b737e24f87a42d472a136e5760";

            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

            byte[] content = UTF8Encoding.UTF8.GetBytes(json);

            using (var payload = new ByteArrayContent(content))
            {
                payload.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = http.PostAsync(endpoint, payload).Result;

                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.Read();

            //text-analytics
        }
    }
}
