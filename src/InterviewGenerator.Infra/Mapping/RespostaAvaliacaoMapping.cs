using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping
{
    public class RespostaAvaliacaoMapping : IEntityTypeConfiguration<RespostaAvaliacao>
    {
        public void Configure(EntityTypeBuilder<RespostaAvaliacao> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
