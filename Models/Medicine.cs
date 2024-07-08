

using ConsoleProjectCodeAcademy.Exceptions;

namespace ConsoleProjectCodeAcademy.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }

        private decimal _price;
        public decimal Price {
            get
            {
                return _price;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine(value);
                    throw new PriceException("Price should be greater than '0'");
                }

                _price= value;
            }
        }
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
            return $"ID: {Id}, Name: {Name}, Price: {Price}, Category ID: {CategoryId}, DateTime - {CreatedTime.ToShortDateString()})";
        }
    }
}
