using InterviewGenerator.Domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewGenerator.Infra.Mapping
{
    public class LinhasArquivoMapping : IEntityTypeConfiguration<LinhasArquivo>
    {
        public void Configure(EntityTypeBuilder<LinhasArquivo> builder)
        {
            builder.HasKey(x => x.Id);

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

           



        }
    }
}
