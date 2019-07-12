using System.Collections.Generic;

namespace PrivateGist.Models
{
    public class RepositoryPatch
    {
        public string Description { get; set; }

        public Dictionary<string, RepositoryPatchFile> Files { get; set; }
    }
}