namespace Ecommerce.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Item ChosenItem { get; set; }
        public int Quantity { get; set; }
    }
}
