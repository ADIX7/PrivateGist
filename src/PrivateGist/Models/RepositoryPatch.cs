using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrivateGist.Models
{
    public class RepositoryPatchFile
    {
        public string Content { get; set; }
        public string Filename { get; set; }
    }

    public class RepositoryPatch
    {
        public string Description { get; set; }

        public Dictionary<string, RepositoryPatchFile> Files { get; set; }
    }
}