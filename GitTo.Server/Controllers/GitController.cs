using GitTo.Server.Model;
using LibGit2Sharp;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GitTo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitController : Controller
    {
        [HttpGet("BranchOrigin")]
        public async Task<BranchGroupModel> BranchOrigin(string? repoPath)
        {
            try
            {
                var branchGroup = new BranchGroupModel();
                repoPath = "C:\\Users\\tongl\\Desktop\\builk\\jubiliapi";
                using var repo = new Repository(repoPath);
                branchGroup.Stashes = repo.Stashes.Select(s => new Stashes { 
                    Message = s.Message,
                }).ToList();
                branchGroup.Tags = repo.Tags.Select(s => new GitTag {
                    FriendlyName = s.FriendlyName.Split('/').ToList()
                }).ToList();
                foreach (var item in repo.Branches)
                {

                    var model = new BranchModel
                    {
                       FriendlyName = item.FriendlyName.Split('/').ToList(),
                       RemoteName =  item.RemoteName,
                       IsRemote = item.IsRemote,
                       IsCurrentRepositoryHead = item.IsCurrentRepositoryHead,
                    };

                    if(model.RemoteName == null)
                    {
                        branchGroup.LocalBranch.Add(model);  
                    } 
                    else if(model.RemoteName == "origin")
                    {
                        branchGroup.OriginBranch.Add(model);
                    }

                }

                return branchGroup;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
