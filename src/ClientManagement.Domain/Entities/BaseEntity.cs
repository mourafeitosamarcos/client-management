using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
