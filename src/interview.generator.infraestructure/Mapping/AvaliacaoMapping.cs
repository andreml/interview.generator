using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.DataAplicacao)
                .IsRequired();

            builder
                .Property(x => x.ObservacaoAplicador)
                .HasColumnType("VARCHAR(500)");

            builder
                .Property(x => x.Nota)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .HasOne(x => x.Candidato);
        }
    }
}
