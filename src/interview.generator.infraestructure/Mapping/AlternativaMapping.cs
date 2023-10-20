using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class AlternativaMapping : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PerguntaId)
                .IsRequired();

            builder.Property(x => x.Correta)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(1000)");
        }
    }
}
