using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.domain.Entidade.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Cpf)
                .IsRequired()
                .HasColumnType("VARCHAR(11)");

            builder
                .Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder
                .Property(x => x.PerfilId)
                .IsRequired();
        }
    }
}
