namespace ClientManagement.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataEdicao { get; set; }
        public bool Ativo { get; set; }
    }
}
