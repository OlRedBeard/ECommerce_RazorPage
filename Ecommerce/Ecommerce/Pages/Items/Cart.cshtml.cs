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
        public decimal total { get; set; }

        [BindProperty]
        public List<Item> myCart { get; set; }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            bool dec = false;

            myCart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "itemCart");
            if (myCart == null)
                myCart = new List<Item>();

            if (id == null)
                return NotFound();

            if (id < 0)
            {
                dec = true;
                id = id * -1;
            }

            Item = await _context.Item.FirstOrDefaultAsync(m => m.Id == id);

            if (Item == null)
                return NotFound();

            bool inCart = false;

            if (dec)
            {
                for(int i = 0; i < myCart.Count; i++)
                {
                    if (myCart[i].Id == id)
                    {
                        myCart[i].Quantity--;
                        if (myCart[i].Quantity <= 0)
                            myCart.Remove(myCart[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < myCart.Count; i++)
                {
                    if (myCart[i].Id == id)
                    {
                        myCart[i].Quantity++;
                        inCart = true;
                    }
                }

                if (!inCart)
                {
                    Item.Quantity = 1;
                    myCart.Add(Item);
                }
            }
            
            foreach(var item in myCart)
            {
                total += item.Price * item.Quantity;
            }
            
            SessionHelper.SetObjectAsJson(HttpContext.Session, "itemCart", myCart);
            return Page();
        }
    }
}
