using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
{
    public class AdicionarQuestionarioDto
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = default!;
        public ICollection<Guid> Perguntas { get; set; } = default!;
    }

    
}
