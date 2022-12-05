using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [Table("customer", Schema = "public")]
    public class Customer
    {
        [Key]
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string mail { get; set; }
        //public DateTime birth_date { get; set; }
        public string number_phone { get; set; }
       // public JsonContent UserSettings { get; set; }
    }
}
