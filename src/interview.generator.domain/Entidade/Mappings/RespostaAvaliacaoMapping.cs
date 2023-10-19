using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.domain.Entidade.Mappings
{
    public class RespostaAvaliacaoMapping : IEntityTypeConfiguration<RespostaAvaliacao>
    {
        public void Configure(EntityTypeBuilder<RespostaAvaliacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.AvaliacaoId)
                .IsRequired();

            builder
                .Property(x => x.PerguntaQuestionarioId)
                .IsRequired();

            builder
                .Property(x => x.AlternativaEscolhidaId)
                .IsRequired();
        }
    }
}
