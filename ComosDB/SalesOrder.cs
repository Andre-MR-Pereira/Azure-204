using Newtonsoft.Json;

namespace ComosDB
{
    public class SalesOrder
    {
        public string id { get; set; }

        [JsonProperty(PropertyName = "partition")]
        public string Partition { get; set; }

        public SalesOrder(string id, string partition) 
        {
            this.id = id;
            this.Partition = partition;
        }
    }
}
