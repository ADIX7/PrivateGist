using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrivateGist.Models
{
    public class RepositoryPatchFile
    {
        public string Content { get; set; }
        public string Filename { get; set; }
    }
}