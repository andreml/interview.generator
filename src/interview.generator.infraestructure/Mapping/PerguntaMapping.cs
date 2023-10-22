using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
{
    public class PerguntaMapping : IEntityTypeConfiguration<Pergunta>
    {
        public void Configure(EntityTypeBuilder<Pergunta> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Descricao)
                .IsRequired()
                .HasColumnType("VARCHAR(1000)");

            builder
                .Property(x => x.UsuarioCriacaoId)
                .IsRequired();

            builder
                .HasMany(x => x.Alternativas)
                .WithOne()
                .HasForeignKey(x => x.PerguntaId)
                .IsRequired();
        }
    }
}
