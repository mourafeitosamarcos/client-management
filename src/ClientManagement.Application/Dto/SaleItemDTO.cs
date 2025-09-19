namespace ClientManagement.Application.Dto
{
    public class SaleItemDTO : BaseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
