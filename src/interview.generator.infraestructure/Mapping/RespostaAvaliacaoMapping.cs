using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class RespostaAvaliacaoMapping : IEntityTypeConfiguration<RespostaAvaliacao>
    {
        public void Configure(EntityTypeBuilder<RespostaAvaliacao> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
