using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewGenerator.Infra.Mapping;

public class LinhaArquivoMapping : IEntityTypeConfiguration<LinhaArquivo>
{
    public void Configure(EntityTypeBuilder<LinhaArquivo> builder)
    {
        builder.HasKey(x => x.Id);

        builder.
            ToTable("LinhaArquivo");

        builder
            .Property(x => x.Erro)
            .HasColumnType("VARCHAR(500)");

        builder
            .Property(x => x.DataProcessamento)
            .HasColumnType("DATETIME");

        builder
            .Property(x => x.NumeroLinha)
            .IsRequired()
            .HasColumnType("INT");

        builder.Property(x => x.StatusImportacao)
            .HasConversion<int>()
            .IsRequired();
    }
}
