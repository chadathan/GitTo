using GitTo.Server.Model;
using LibGit2Sharp;
using Microsoft.AspNetCore.Mvc;

namespace GitTo.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoHomeController : ControllerBase
    {
        [HttpGet("GetTodos")]
        public IActionResult GetTodos()
        {
            return Ok(new[] { "Todo1", "Todo2ss" });
        }

        [HttpGet("GetBranchOrigin")]
        public async Task<ActionResult<BranchGroupModel>> GetBranchOrigin(string? repoPath)
        {
            try
            {
                var branchGroup = new BranchGroupModel();
                repoPath = "C:\\Users\\tongl\\Desktop\\builk\\jubiliapi";
                using var repo = new Repository(repoPath);
                branchGroup.Stashes = repo.Stashes.Select(s => new Stashes
                {
                    Message = s.Message,
                }).ToList();
                branchGroup.Tags = repo.Tags.Select(s => new GitTag
                {
                    FriendlyName = s.FriendlyName.Split('/').ToList()
                }).ToList();
                foreach (var item in repo.Branches)
                {
                    var model = new BranchModel
                    {
                        FriendlyName = item.FriendlyName.Split('/').ToList(),
                        RemoteName = item.RemoteName,
                        IsRemote = item.IsRemote,
                        IsCurrentRepositoryHead = item.IsCurrentRepositoryHead,
                    };

                    if (model.RemoteName == null)
                    {
                        branchGroup.LocalBranch.Add(model);
                    }
                    else if (model.RemoteName == "origin")
                    {
                        branchGroup.OriginBranch.Add(model);
                    }
                }

                return Ok(branchGroup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
