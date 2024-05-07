using InterviewGenerator.Domain.Entidade.Common;

namespace InterviewGenerator.Domain.Entidade;

public class AreaConhecimento : EntidadeBase
{
    public string Descricao { get; set; } = default!;
    public ICollection<Pergunta>? Perguntas { get; set; }

    public Guid UsuarioCriacaoId { get; set; }

    public AreaConhecimento()
    {
    }

    public void AlterarDescricao(string descricao) =>
        Descricao = descricao;
}
