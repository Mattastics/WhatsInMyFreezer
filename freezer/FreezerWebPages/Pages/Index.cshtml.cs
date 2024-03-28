using Microsoft.AspNetCore.Mvc.RazorPages;
using freezer.Logic;

namespace FreezerWebPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IFreezerLogic _freezerLogic;

        public IndexModel(IFreezerLogic freezerLogic)
        {
            _freezerLogic = freezerLogic;
        }

        public void OnGet()
        {
        }
    }
}

