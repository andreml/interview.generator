﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using interview.generator.infraestructure.Context;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("interview.generator.domain.Entidade.Alternativa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Correta")
                        .HasColumnType("bit");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1000)");

                    b.Property<Guid>("PerguntaId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PerguntaId");

                    b.ToTable("Alternativa");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.AreaConhecimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AreaConhecimento");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Avaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CandidatoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataAplicacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("ObservacaoAplicador")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)");

                    b.Property<Guid>("QuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Pergunta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaConhecimentoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(1000)");

                    b.Property<Guid>("UsuarioCriacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AreaConhecimentoId");

                    b.ToTable("Pergunta");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.PerguntaQuestionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrdemApresentacao")
                        .HasColumnType("int");

                    b.Property<Guid>("PerguntaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.Property<Guid>("QuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("PerguntaQuestionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Questionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.Property<Guid>("TipoQuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioCriacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Questionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.RespostaAvaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AlternativaEscolhidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AvaliacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PerguntaQuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.ToTable("RespostaAvaliacao");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.TipoQuestionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TipoQuestionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("VARCHAR(11)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Alternativa", b =>
                {
                    b.HasOne("interview.generator.domain.Entidade.Pergunta", null)
                        .WithMany("Alternativas")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Pergunta", b =>
                {
                    b.HasOne("interview.generator.domain.Entidade.AreaConhecimento", "AreaConhecimento")
                        .WithMany("Perguntas")
                        .HasForeignKey("AreaConhecimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AreaConhecimento");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.RespostaAvaliacao", b =>
                {
                    b.HasOne("interview.generator.domain.Entidade.Avaliacao", null)
                        .WithMany("Respostas")
                        .HasForeignKey("AvaliacaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.AreaConhecimento", b =>
                {
                    b.Navigation("Perguntas");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Avaliacao", b =>
                {
                    b.Navigation("Respostas");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Pergunta", b =>
                {
                    b.Navigation("Alternativas");
                });
#pragma warning restore 612, 618
        }
    }
}
