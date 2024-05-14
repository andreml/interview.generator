using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping;

public class AvaliacaoMapping : IEntityTypeConfiguration<Avaliacao>
{
    public void Configure(EntityTypeBuilder<Avaliacao> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.DataEnvio)
            .IsRequired();

        builder
            .Property(x => x.Respondida)
            .IsRequired();

        builder
            .Property(x => x.DataResposta)
            .IsRequired(false);

        builder
            .Property(x => x.ObservacaoAplicador)
            .HasColumnType("VARCHAR(500)");

        builder
            .Property(x => x.Nota)
            .IsRequired(false)
            .HasPrecision(10, 2);

        builder
            .HasOne(x => x.Candidato);
    }
}
