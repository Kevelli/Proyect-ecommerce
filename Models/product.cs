using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string? Name {get; set;}

        public ProductState state {get; set;}

        [DataType(DataType.Currency)]
        public decimal Price {get; set;}

        [DataType(DataType.Date)]
        public DateTime CreatedAt {get; set;}

        [DataType(DataType.Date)]
        public DateTime UpdatedAt {get; set;}
        public int Amount {get; set;}

        [DataType(DataType.Text)]
        public string? Description {get; set;}

    }

    public enum ProductState
    {
        AVAILABLE, NON_AVAILABLE
    }
}