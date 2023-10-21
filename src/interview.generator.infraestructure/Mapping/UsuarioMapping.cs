using interview.generator.domain.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace interview.generator.infraestructure.Mapping
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

            builder.Property(u => u.Perfil)
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(x => x.Login)
                .IsRequired()
                .HasColumnType("VARCHAR(30)");

            builder
                .Property(x => x.Senha)
                .IsRequired()
                .HasColumnType("VARCHAR(500)");
        }
    }
}
