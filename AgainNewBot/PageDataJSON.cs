using Newtonsoft.Json;
namespace AgainNewBot
{
    public class PageData
    {
        [JsonProperty("pageid")]
        public int Pageid { get; set; }

        [JsonProperty("ns")]
        public int Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("extract")]
        public string Extract { get; set; }
    }
}
