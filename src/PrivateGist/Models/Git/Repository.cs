using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PrivateGist.Models.Git
{
    public class Repository
    {
        public Repository() { }
        public Repository(IGlobalSettings settings, string id)
        {
            Id = id;

            Url = settings.RootWebUrl + "gists/" + Id;
            Forks_Url = settings.RootWebUrl + "gists/" + Id + "/forks";
            Commits_Url = settings.RootWebUrl + "gists/" + Id + "/commits";
            Comments_Url = settings.RootWebUrl + "gists/" + Id + "/comments";

            Git_Push_Url = settings.RootWebUrl + Id + ".git";
            Git_Pull_Url = settings.RootWebUrl + Id + ".git";

            Html_Url = settings.RootWebUrl + Id;

            Description = Id; //TODO change
            Created_At = DateTime.Now;
            Updated_At = DateTime.Now;
        }

        public string Id { get; set; }
        public string Url { get; set; }
        public string Forks_Url { get; set; }
        public string Commits_Url { get; set; }
        //public string Node_Id { get; set; }

        [JsonPropertyName("git_pull_url")]
        public string Git_Pull_Url { get; set; }

        [JsonPropertyName("git_push_url")]
        public string Git_Push_Url { get; set; }

        [JsonPropertyName("html_url")]
        public string Html_Url { get; set; }
        public Dictionary<string, File> Files { get; set; }
        public bool Public { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime Created_At { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime Updated_At { get; set; }
        public string Description { get; set; }
        public int Comments { get; set; }
        public string User { get; set; }
        public string Comments_Url { get; set; }
        public bool Truncated { get; set; }
    }
}