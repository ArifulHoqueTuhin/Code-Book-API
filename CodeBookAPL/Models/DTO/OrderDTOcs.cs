namespace CodeBookAPL.Models.DTO
{
    public class OrderDTOcs

    {

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public decimal AmountPaid { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new();
    }
}
