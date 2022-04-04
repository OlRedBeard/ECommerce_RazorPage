using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Pages
{
    public class ConfirmationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string name { get; set; }

        public void OnGet()
        {
        }
    }
}
