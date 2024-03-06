using InterviewGenerator.Application.Dto;
using InterviewGenerator.Domain.Enum;
using System.Text.Json.Serialization;

namespace InterviewGenerator.CrossCutting.Eventos
{
    public class EventoImportacaoPerguntas
    {
        public AdicionarPergunta? Pergunta { get; set; }
        public int NumeroLinha { get; set; }
        public Guid IdArquivo { get; set; }
    }

    public class AdicionarPergunta
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; } = default!;
        public string AreaConhecimento { get; set; } = default!;
        public ICollection<Alternativa> Alternativas { get; set; } = default!;
        private int NumeroLinha { get; set; }

        

    }

    public class Alternativa
    {
        public Alternativa(string descricao, bool correta)
        {
            Descricao = descricao;
            Correta = correta;
        }

        public string Descricao { get; set; } = default!;
        public bool Correta { get; set; }
    }
}
