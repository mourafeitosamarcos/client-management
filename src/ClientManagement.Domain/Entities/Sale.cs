namespace ClientManagement.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<SaleItem> Items { get; set; }
    }
}
