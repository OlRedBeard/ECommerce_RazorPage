using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Pages.Items
{
    public class CartModel : PageModel
    {
        private readonly Ecommerce.Data.EcommerceContext _context;
        public CartModel(Ecommerce.Data.EcommerceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public List<Item> myCart { get; set; }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            myCart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "itemCart");
            if (myCart == null)
                myCart = new List<Item>();

            if (id == null)
                return NotFound();

            Item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
                return NotFound();

            // This doesn't work, still just adds items repeatedly
            if (myCart.Contains(Item))
            {
                myCart.Remove(Item);
                Item.Quantity++;
                myCart.Add(Item);
            }
            else
            {
                Item.Quantity = 1;
                myCart.Add(Item);
            }
            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "itemCart", myCart);
            return Page();
        }
    }
}
