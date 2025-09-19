namespace ClientManagement.Application.Dto
{
    public class ClientDTO : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }
    }
}
