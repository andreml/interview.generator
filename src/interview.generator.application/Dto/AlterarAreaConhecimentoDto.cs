using System.Text.Json.Serialization;

namespace interview.generator.application.Dto
{
    public class AlterarAreaConhecimentoDto
    {
        [JsonIgnore]
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; } = default!;

        public Guid Id { get; set; }
    }
}
