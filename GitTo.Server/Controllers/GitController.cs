using LibGit2Sharp;
using Microsoft.AspNetCore.Mvc;

namespace GitTo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitController : Controller
    {
        [HttpGet("BranchOrigin")]
        public void BranchOrigin(string repoPath)
        {
            using var repo = new Repository(repoPath);
            var a = repo.Branches;
            Console.WriteLine(a);
        }
    }
}
