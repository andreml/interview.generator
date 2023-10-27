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
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("interview.generator.domain.Entidade.Alternativa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

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
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<Guid>("UsuarioCriacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("AreaConhecimento");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Avaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    b.Property<Guid>("CandidatoId")
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "CandidatoId");

                    b.Property<DateTime>("DataAplicacao")
                        .HasColumnType("datetime2")
                        .HasAnnotation("Relational:JsonPropertyName", "DataAplicacao");

                    b.Property<string>("ObservacaoAplicador")
                        .IsRequired()
                        .HasColumnType("VARCHAR(500)")
                        .HasAnnotation("Relational:JsonPropertyName", "ObservacaoAplicador");

                    b.Property<Guid>("QuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionarioId");

                    b.ToTable("Avaliacao");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Pergunta", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

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
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    b.Property<int>("OrdemApresentacao")
                        .HasColumnType("int");

                    b.Property<Guid>("PerguntaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PerguntaId");

                    b.HasIndex("QuestionarioId");

                    b.ToTable("PerguntaQuestionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Questionario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.Property<Guid>("UsuarioCriacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Questionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.RespostaAvaliacao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

                    b.Property<Guid>("AlternativaEscolhidaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AvaliacaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PerguntaQuestionarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AvaliacaoId");

                    b.ToTable("RespostaAvaliacao");

                    b.HasAnnotation("Relational:JsonPropertyName", "Respostas");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasAnnotation("Relational:JsonPropertyName", "Id");

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

            modelBuilder.Entity("interview.generator.domain.Entidade.Avaliacao", b =>
                {
                    b.HasOne("interview.generator.domain.Entidade.Questionario", "Questionario")
                        .WithMany("Avaliacoes")
                        .HasForeignKey("QuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questionario");
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

            modelBuilder.Entity("interview.generator.domain.Entidade.PerguntaQuestionario", b =>
                {
                    b.HasOne("interview.generator.domain.Entidade.Pergunta", "Pergunta")
                        .WithMany("PerguntasQuestionario")
                        .HasForeignKey("PerguntaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("interview.generator.domain.Entidade.Questionario", "Questionario")
                        .WithMany("PerguntasQuestionario")
                        .HasForeignKey("QuestionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pergunta");

                    b.Navigation("Questionario");
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

                    b.Navigation("PerguntasQuestionario");
                });

            modelBuilder.Entity("interview.generator.domain.Entidade.Questionario", b =>
                {
                    b.Navigation("Avaliacoes");

                    b.Navigation("PerguntasQuestionario");
                });
#pragma warning restore 612, 618
        }
    }
}
