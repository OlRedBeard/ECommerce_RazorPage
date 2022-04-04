using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Pages
{
    [Authorize(Roles = "User,Admin")]

    public class CheckoutModel : PageModel
    {       
        [BindProperty]
        public InputModel input { get; set; }
        public string resultInfo { get; set; }
        public string Total { get; set; }

        public class InputModel
        {
            [StringLength(20, MinimumLength = 1)]
            [Required(ErrorMessage = "Please enter your first name")]
            [Display(Name = "First Name:")]
            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Capital first letter, no symbols")] // Capital first letter, no symbols
            public string firstName { get; set; }

            [StringLength(20, MinimumLength = 1)]
            [Required(ErrorMessage = "Please enter your last name")]
            [Display(Name = "Last Name:")]
            [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Capital first letter, no symbols")] // Capital first letter, no symbols
            public string lastName { get; set; }

            [Required(ErrorMessage = "Please enter your street address")]
            [Display(Name = "Address:")]
            public string streetAddress { get; set; }

            [Required(ErrorMessage = "Please enter your postal code")]
            [Display(Name = "Postal Code:")]
            [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Please enter a valid postal code")]
            public string postalCode { get; set; }

            [Required(ErrorMessage = "Please enter a province")]
            [Display(Name = "Province:")]
            public string province { get; set; }

            [Required(ErrorMessage = "Please enter your country")]
            [Display(Name = "Country:")]
            public string country { get; set; }

            [Required(ErrorMessage = "Please enter your email")]
            [EmailAddress]
            [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Please enter a valid e-mail address")]
            [Display(Name = "Email:")]
            public string eMail { get; set; }

            [StringLength(19, MinimumLength = 16)]
            [Required(ErrorMessage = "Please enter your credit card number")]
            [RegularExpression(@"^(\d{4}[- ]){3}\d{4}|\d{16}$", ErrorMessage = "Please enter a valid credit card number (16 digits)")]
            [Display(Name = "Credit Card Number:")]
            public string CreditCardNum { get; set; }

            [StringLength(5, MinimumLength = 4)]
            [Required(ErrorMessage = "Please enter the expiry date for the card")]
            [RegularExpression(@"^(0[1-9]|1[0-2])\/?([1-9]{2})$", ErrorMessage = "Please enter your card's expiry date (12/31)")]
            [Display(Name = "Expiry Date")]
            public string CCExp { get; set; }

            [StringLength(3, MinimumLength = 3)]
            [Required(ErrorMessage = "Please enter the number on the back of your card")]
            [RegularExpression(@"^[0-9]{3}", ErrorMessage = "Please enter the 3-digit number on the back of your card")]
            [Display (Name = "CVV2")]
            public string CVV2 { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string name = "";
            name += input.firstName + " " + input.lastName;

            if (!ModelState.IsValid)
            {
                resultInfo = "You must enter all the information";
                return Page();
            }

            else
            {
                return RedirectToPage("/Confirmation", new { name });
            }
        }
    }
}
