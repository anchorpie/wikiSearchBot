using Newtonsoft.Json;
using System.Collections.Generic;

namespace AgainNewBot
{
    public class ResponseJSON
    {
        [JsonProperty("batchcomplete")]
        public string Batchcomplete { get; set; }

        [JsonProperty("continue")]
        public Continue Continue { get; set; }

        [JsonProperty("query")]
        public Query Query { get; set; }

    }

    public class Continue
    {
        [JsonProperty("sroffset")]
        public int Sroffset { get; set; }

        [JsonProperty("continue")]
        public string Continue2 { get; set; }
    }

    public class Query
    {
        [JsonProperty("searchinfo")]
        public Searchinfo Searchinfo { get; set; }
        
        [JsonProperty("search")]
        public List<Search> Search { get; set; }
    }

    public class Searchinfo
    {
        [JsonProperty("totalhits")]
        public int Totalhits { get; set; }
    }

    public class Search
    {
        [JsonProperty("ns")]
        public int Ns { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("pageid")]
        public int Pageid { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("wordcount")]
        public int Wordcount { get; set; }

        [JsonProperty("snippet")]
        public string Snippet { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}
