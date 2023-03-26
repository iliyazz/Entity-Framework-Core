namespace Artillery.DataProcessor.ImportDto
{
    using Newtonsoft.Json;

    public class ImportCountryIdDto
    {
        [JsonProperty(nameof(Id))]
        public int Id { get; set; }
    }
}
