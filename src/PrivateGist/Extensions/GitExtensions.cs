using System;
using System.Linq;
using LibGit2Sharp;

namespace PrivateGist.Extensions
{
    public static class GitExtensions
    {
        public static Branch GetMainBranch(this Branch[] branches) => Array.Find(branches, b => b.FriendlyName == "master") ?? branches[0];
        public static Branch GetMainBranch(this BranchCollection branches) => branches.FirstOrDefault(b => b.FriendlyName == "master") ?? branches.First();
    }
}