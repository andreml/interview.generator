﻿using interview.generator.domain.Entidade.Common;
using interview.generator.domain.Enum;
using interview.generator.domain.Utils;

namespace interview.generator.domain.Entidade
{
    public class Usuario : EntidadeBase
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public Perfil Perfil { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {
        }

        public Usuario(string cpf, string nome, Perfil perfil, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Perfil = perfil;
            Login = login;
            Senha = Encryptor.Encrypt(senha);
        }

        public DateTime VerificaValidadeTokenCandidato()
        {
            switch (this.Perfil)
            {
                case Perfil.Avaliador:
                    return DateTime.Now.AddYears(1);
                case Perfil.Candidato:
                    return DateTime.Now.AddDays(1);
                default:
                    return DateTime.Now;
            }
        }

        public void Atualizar(string cpf, string nome, string login, string senha)
        {
            Cpf = cpf;
            Nome = nome;
            Login = login;
            Senha = Encryptor.Encrypt(senha);
        }
    }
}