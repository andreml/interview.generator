namespace InterviewGenerator.CrossCutting.Eventos
{
    public class EventoImportacaoPerguntas
    {
        public Guid UsuarioId { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; } = new List<Pergunta>();
    }

    public class Pergunta
    {
        public Guid Id { get; set; }
        public string Areaconhecimento { get; set; } = default!;
        public string Descricao { get; set; } = default!;
        public ICollection<Alternativa> Alternativas { get; set; } = default!;
    }

    public class Alternativa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; } = default!;
        public bool Correta { get; set; }
    }
}
