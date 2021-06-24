using Microsoft.AspNetCore.Mvc;

namespace Markdig.Extensions.LineHighlight.ViewComponents.Styles
{
    public class PrismJsStyleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/ViewComponents/Styles/Default.cshtml");
        }
    }
}
