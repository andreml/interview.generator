using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
{
    public class AlterarQuestionarioDto
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public Guid QuestionarioId { get; set; }
        public string Nome { get; set; } = default!;
        public ICollection<PerguntaQuestionarioDto> Perguntas { get; set; }
    }
}
