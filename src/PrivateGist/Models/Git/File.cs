using System.Text.Json.Serialization;

namespace PrivateGist.Models.Git
{
    public class File
    {
        public File() { }
        public File(IGlobalSettings settings, Repository containerRepo, string fileName, string gitId)
        {
            Filename = fileName;
            Raw_Url = containerRepo.Html_Url + "/raw/" + gitId;
        }
        public string Filename { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        
        [JsonPropertyName("raw_url")]
        public string Raw_Url { get; set; }
        public int Size { get; set; }
    }
}