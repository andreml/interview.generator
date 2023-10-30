using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping
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

            builder
                .HasMany(x => x.RespostasAvaliacao)
                .WithOne(x => x.Pergunta)
                .IsRequired(false);
        }
    }
}
