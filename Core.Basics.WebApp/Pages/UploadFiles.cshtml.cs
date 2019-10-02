using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace Core.Basics.WebApp.Pages
{
    public class UploadFiles : PageModel
{
    

    public void OnPost(List<IFormFile> files) {
        var size = files.Sum(x => x.Length);
    }
}
}