using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interview.generator.application.Dto
{
    public class GeraTokenUsuario
    {
        public GeraTokenUsuario(string login, string senha)
        {
            this.Login = login;
            this.Senha = senha;
        }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
