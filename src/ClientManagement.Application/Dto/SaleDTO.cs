namespace ClientManagement.Application.Dto
{
    public class SaleDTO : BaseDto
    {
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<SaleItemDTO> Items { get; set; }
    }
}
