using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.domain.Entidade.Mappings
{
    public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
    {
        public void Configure(EntityTypeBuilder<Avaliacao> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.CandidatoId)
                .IsRequired();

            builder
                .Property(x => x.QuestionarioId)
                .IsRequired();

            builder
                .Property(x => x.DataAplicacao)
                .IsRequired();

            builder
                .Property(x => x.ObservacaoAplicador)
                .HasColumnType("VARCHAR(500)");

            builder
                .HasMany(x => x.Respostas)
                .WithOne()
                .HasForeignKey(x => x.AvaliacaoId);
        }
    }
}
