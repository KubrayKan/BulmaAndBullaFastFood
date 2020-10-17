namespace BulmaAndBullaFastFood.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class OrderHistory
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Credit Card Number")]
        public string credit_card_number { get; set; }
        [Required]
        [Display(Name = "Expiration Date")]
        public string expiration_date { get; set; }
        [Required]
        [Display(Name = "Security Number")]
        public int security_number { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        public string user_Id { get; set; }
        [Display(Name = "Items List")]
        public string list_of_items { get; set; }
        [Display(Name = "Price")]
        public decimal total_price { get; set; }
        [Display(Name = "Purchase Date")]
        public string purchase_date { get; set; }
    }
}
