namespace GitTo.Server.Model
{
    public class BranchGroupModel
    {
        public List<BranchModel> LocalBranch { get; set; } = new List<BranchModel>();
        public List<BranchModel> OriginBranch { get; set; } = new List<BranchModel>();
        public List<GitTag> Tags { get; set; } = new List<GitTag>();
        public List<Stashes> Stashes { get; set; } = new List<Stashes>();
    }

    public class BranchModel
    {
        public List<string> FriendlyName { get;  set; } = new List<string>();
        public string? RemoteName { get; internal set; }
        public bool IsRemote { get; internal set; }
        public bool IsCurrentRepositoryHead { get;  set; }
    }

    public class GitTag 
    {
        public List<string> FriendlyName { get; set; } = new List<string>();
    }

      
    public class Stashes
    {
       public string? Message { get; set; }
    }
}
