using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace PartsClient.Data
{
    public static class PartsManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey
        static readonly string BaseAddress = "https://mslearnpartsserver148183222.azurewebsites.net";
        static readonly string Url = $"{BaseAddress}/api";

        static HttpClient client;

        private static string authorizationKey;

        private static async Task<HttpClient> GetClient()
        {
            if (client != null)
            {
                return client;
            }
            client= new HttpClient();
            if (string.IsNullOrEmpty(authorizationKey) )
            {
                authorizationKey = Preferences.Get("authkey", string.Empty, string.Empty);
                if (string.IsNullOrEmpty(authorizationKey) )
                {
                    authorizationKey = await client.GetFromJsonAsync<string>($"{Url}/login");
                    Preferences.Set("authkey", authorizationKey);
                }
            }

            client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public static async Task<IEnumerable<Part>> GetAll()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return new List<Part>();
            
            var client = await GetClient();
            return await client.GetFromJsonAsync<IEnumerable<Part>>($"{Url}/parts");

            // result = client.GetStringAsync($"{Url}/parts");
            //return JsonConvert.DeserializeObject<IEnumerable<Part>>(result);

        }

        public static async Task<Part> Add(string partName, string supplier, string partType)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return null;

            var part = new Part()
            {
                PartName = partName,
                Suppliers = new List<string>(new[] { supplier }),
                PartID = string.Empty,
                PartType = partType,
                PartAvailableDate = DateTime.Now.Date
            };

            var client = await GetClient();

            // Use either the below line of the next 2 lines after the below and dont need both
            // They achieve the same thing.
            // var response = await client.PutAsJsonAsync<Part>($"{Url}/parts)", part);

            var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}/parts");
            msg.Content = JsonContent.Create<Part>(part);

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();

            var itemJson = await response.Content.ReadAsStringAsync();
            var insertedPart = System.Text.Json.JsonSerializer.Deserialize<Part>(itemJson);

            return insertedPart;
        }

        public static async Task Update(Part part)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return ;

            HttpRequestMessage msg = new(HttpMethod.Put, $"{Url}/parts/part.PartID)");
            msg.Content = JsonContent.Create<Part>(part);

            HttpClient client = await GetClient();

            // Either this line or next line is necessary, both do the same thing.
            // The same can be used for Adding as well and reduce code complexity.
            // var response = await client.PutAsJsonAsync<Part>($"{Url}/parts/part.PartID)", part);

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();



        }

        public static async Task Delete(string partID)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                return ;

            HttpRequestMessage msg = new(HttpMethod.Delete, $"{Url}/part/partID)");

            HttpClient client = await GetClient();

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();

            //await client.DeleteAsync(partID);

        }
    }
}
