using Microsoft.AspNetCore.Mvc;

namespace Markdig.Extensions.LineHighlight.ViewComponents.Scripts
{
    public class PrismJsScriptViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/ViewComponents/Scripts/Default.cshtml");
        }
    }
}
