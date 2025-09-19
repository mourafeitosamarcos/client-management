namespace ClientManagement.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
