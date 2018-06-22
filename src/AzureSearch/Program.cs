using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AzureSearch
{
    class CustomDocument
    {
        [Key]
        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        public string id { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string nome { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string email { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        public int idade { get; set; }

        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        [IsFacetable]
        public string cidade { get; set; }

        [JsonProperty("endereco")]
        [IsFilterable]
        [IsSortable]
        [IsRetrievable(true)]
        [IsSearchable]
        public string Endereco { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //https://thnetazsearch.search.windows.net

            //6E74D968A34AD1E6A47C9EEAD75ABD09

            //mbafiap

            SearchServiceClient cliente = new SearchServiceClient("thnetazsearch", new SearchCredentials("6E74D968A34AD1E6A47C9EEAD75ABD09"));

            ISearchIndexClient index = cliente.Indexes.GetClient("mbafiap");

            //var document = new CustomDocument()
            //{
            //    id = "rm42533",
            //    cidade = "Mogi",
            //    email = "ander.unip@gmail.com",
            //    Endereco = "Avenida Mogi",
            //    idade = 26,
            //    nome = "Anderson Jesus"
            //};

            //IndexBatch<CustomDocument> batch = IndexBatch.MergeOrUpload(new List<CustomDocument> { document });

            //index.Documents.Index(batch);

            Console.WriteLine("Digite um termo para busca:");
            var termo = Console.ReadLine();

            var response = index.Documents.Search<CustomDocument>(termo, new SearchParameters()
            {
                IncludeTotalResultCount = true,
                OrderBy = new List<string> { "nome" },
                Filter = "idade gt 25",
                QueryType = QueryType.Full
                
            });

            Console.WriteLine($"{response.Count} documentos encontrados");
            foreach (var item in response.Results)
            {
                Console.WriteLine($"{item.Document.nome} {item.Document.email}");
            }            

            Console.Read();
        }
    }
}
