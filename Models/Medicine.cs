

namespace ConsoleProjectCodeAcademy.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedTime { get; set; }

        public Medicine(string name, decimal price, int userId, int categoryId)
        {
            Name = name;
            Price = price;
            CreatedTime = DateTime.Now;
            UserId = userId;
            CategoryId = categoryId;
        }
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Price: {Price}, Category ID: {CategoryId})";
        }
    }
}
