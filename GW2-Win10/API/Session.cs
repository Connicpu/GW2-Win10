using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace GW2_Win10.API
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Session
    {
        public Session(string apiKey)
        {
            ApiKey = apiKey;
            CreateClient();
        }

        public HttpClient Client { get; private set; }

        [JsonProperty]
        public string ApiKey { get; }

        [JsonProperty]
        public ApiKeyInfo KeyInfo { get; set; }

        [JsonProperty]
        public Account Account { get; set; }

        [JsonProperty]
        public Characters CharacterNames { get; set; }

        [JsonProperty]
        public ObservableCollection<Character> Characters { get; set; } = new ObservableCollection<Character>();

        public async Task LoadInfo()
        {
            KeyInfo = await Retrieve<ApiKeyInfo>();
            Account = await Retrieve<Account>();
        }

        public async Task Refresh()
        {
            await LoadInfo();

            try
            {
                CharacterNames = await Retrieve<Characters>();

                (Characters = Characters ?? new ObservableCollection<Character>()).Clear();
                foreach (var name in CharacterNames)
                {
                    Characters.Add(await Retrieve<Character>(new { id = name }));
                }
            }
            catch (ApiException)
            {
                // You don't have character permission
                CharacterNames = null;
                Characters = null;
            }
        }

        public bool HasPermission(string perm)
        {
            return KeyInfo?.Permissions?.Contains(perm) ?? false;
        }

        public UriBuilder GetUri(string endpoint)
        {
            return new UriBuilder("https", "api.guildwars2.com", 443, endpoint);
        }

        public async Task<T> Retrieve<T>(string api, object args)
        {
            var endpoint = MakeUri(api, args);
            return await DoGet<T>(endpoint);
        }

        public async Task<T> Retrieve<T>(object args = null) where T : IApiType, new()
        {
            var obj = new T();
            var endpoint = MakeUri(obj.Endpoint, args);
            await DoGet(endpoint, obj);
            return obj;
        }

        public async Task<List<T>> RetrieveList<T>(object args = null) where T : IApiListType, new()
        {
            var endpoint = MakeUri(new T().Endpoint, args);
            return await DoGet<List<T>>(endpoint);
        }

        private Uri MakeUri(string api, object args)
        {
            var endpoint = GetUri(api);
            if (args != null)
            {
                endpoint.Query = string.Join(
                    "&",
                    from prop in args.GetType().GetProperties()
                    select $"{prop.Name}={WebUtility.UrlEncode(prop.GetValue(args).ToString())}"
                    );
            }
            return endpoint.Uri;
        }

        private async Task<T> DoGet<T>(Uri api)
        {
            // Request from the server
            var response = await Client.GetAsync(api);

            // Read the response
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException(responseData);
            }

            // Decode the response
            return JsonConvert.DeserializeObject<T>(responseData);
        }

        private async Task DoGet<T>(Uri api, T obj)
        {
            // Request from the server
            var response = await Client.GetAsync(api);

            // Read the response
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new ApiException(responseData);
            }

            // Decode the response
            JsonConvert.PopulateObject(responseData, obj);
        }

        private void CreateClient()
        {
            Client = new HttpClient();
            var headers = Client.DefaultRequestHeaders;
            // Authorization: Bearer <API key>
            headers.Add("Authorization", $"Bearer {ApiKey}");
            // Accept: application/json
            headers.Accept.Clear();
            headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}