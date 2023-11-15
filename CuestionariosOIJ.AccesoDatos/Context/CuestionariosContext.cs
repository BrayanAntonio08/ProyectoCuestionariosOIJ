using System;
using System.Collections.Generic;
using CuestionariosOIJ.AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CuestionariosOIJ.AccesoDatos.Context
{
    public partial class CuestionariosContext : DbContext
    {
        public CuestionariosContext()
        {
        }

        public CuestionariosContext(DbContextOptions<CuestionariosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaEF> Categorias { get; set; } = null!;
        public virtual DbSet<CuestionarioEF> Cuestionarios { get; set; } = null!;
        public virtual DbSet<JustiicacionRespuestaEF> JustiicacionesRespuesta { get; set; } = null!;
        public virtual DbSet<OficinaEF> Oficinas { get; set; } = null!;
        public virtual DbSet<OpcionRespuestaEF> OpcionesRespuesta { get; set; } = null!;
        public virtual DbSet<PreguntaEF> Preguntas { get; set; } = null!;
        public virtual DbSet<RespuestaEF> Respuestas { get; set; } = null!;
        public virtual DbSet<RevisadorCuestionarioEF> RevisadoresCuestionarios { get; set; } = null!;
        public virtual DbSet<SubcategoriaEF> Subcategorias { get; set; } = null!;
        public virtual DbSet<TipoCuestionarioEF> TipoCuestionarios { get; set; } = null!;
        public virtual DbSet<TipoPreguntaEF> TiposPregunta { get; set; } = null!;
        public virtual DbSet<UsuarioRespuestaEF> UsuariosRespuesta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=163.178.107.10; Initial Catalog=DataBaseCuestionarios; Persist Security Info=False; User ID=laboratorios; Password=TUy&)&nfC7QqQau.%278UQ24/=%;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaEF>(entity =>
            {
                entity.ToTable("Categoria", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuestionarioEF>(entity =>
            {
                entity.ToTable("Cuestionario", "Cuestionarios");

                entity.HasIndex(e => e.Codigo, "UQ__Cuestion__06370DAC9010A7B0")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCreacion).HasColumnType("datetime");

                entity.Property(e => e.FechaUltimaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");

                entity.Property(e => e.Eliminado).HasColumnType("bit");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OficinaId).HasColumnName("OficinaID");

                entity.Property(e => e.TipoCuestionarioId).HasColumnName("TipoCuestionarioID");

                entity.HasOne(d => d.Oficina)
                    .WithMany(p => p.Cuestionarios)
                    .HasForeignKey(d => d.OficinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuestiona__Ofici__2B3F6F97");

                entity.HasOne(d => d.TipoCuestionario)
                    .WithMany(p => p.Cuestionarios)
                    .HasForeignKey(d => d.TipoCuestionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuestiona__TipoC__2A4B4B5E");
            });

            modelBuilder.Entity<JustiicacionRespuestaEF>(entity =>
            {
                entity.HasKey(e => e.RespuestaId)
                    .HasName("PK__Justiica__31F7FC316BE33AB2");

                entity.ToTable("Justiicacion_Respuesta", "Cuestionarios");

                entity.Property(e => e.RespuestaId)
                    .ValueGeneratedNever()
                    .HasColumnName("RespuestaID");

                entity.Property(e => e.Justificacion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Respuesta)
                    .WithOne(p => p.JustiicacionRespuesta)
                    .HasForeignKey<JustiicacionRespuestaEF>(d => d.RespuestaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Justiicac__Respu__4D94879B");
            });

            modelBuilder.Entity<OficinaEF>(entity =>
            {
                entity.ToTable("Oficina", "Administracion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OpcionRespuestaEF>(entity =>
            {
                entity.ToTable("OpcionRespuesta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");

                entity.Property(e => e.TextoOpcion)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.OpcionesRespuesta)
                    .HasForeignKey(d => d.PreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OpcionRes__Pregu__3C69FB99");
            });

            modelBuilder.Entity<PreguntaEF>(entity =>
            {
                entity.ToTable("Pregunta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.CuestionarioId).HasColumnName("CuestionarioID");

                entity.Property(e => e.Etiqueta)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SubcategoriaId).HasColumnName("SubcategoriaID");

                entity.Property(e => e.TextoPregunta)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPreguntaId).HasColumnName("TipoPreguntaID");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Preguntas)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Pregunta__Catego__36B12243");

                entity.HasOne(d => d.Cuestionario)
                    .WithMany(p => p.Preguntas)
                    .HasForeignKey(d => d.CuestionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pregunta__Cuesti__398D8EEE");

                entity.HasOne(d => d.Subcategoria)
                    .WithMany(p => p.Preguntas)
                    .HasForeignKey(d => d.SubcategoriaId)
                    .HasConstraintName("FK__Pregunta__Subcat__37A5467C");

                entity.HasOne(d => d.TipoPregunta)
                    .WithMany(p => p.Preguntas)
                    .HasForeignKey(d => d.TipoPreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pregunta__TipoPr__38996AB5");
            });

            modelBuilder.Entity<RespuestaEF>(entity =>
            {
                entity.ToTable("Respuesta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaEliminada).HasColumnType("datetime");

                entity.Property(e => e.FechaRespondida).HasColumnType("datetime");

                entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");

                entity.Property(e => e.RespuestaCuestionarioId).HasColumnName("RespuestaCuestionarioID");

                entity.Property(e => e.TextoRespuesta)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.Respuestas)
                    .HasForeignKey(d => d.PreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Respuesta__Pregu__3F466844");

                entity.HasMany(d => d.OpcionRespuesta)
                    .WithMany(p => p.Respuestas)
                    .UsingEntity<Dictionary<string, object>>(
                        "RespuestaOpcionRespuestum",
                        l => l.HasOne<OpcionRespuestaEF>().WithMany().HasForeignKey("OpcionRespuestaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Respuesta__Opcio__47DBAE45"),
                        r => r.HasOne<RespuestaEF>().WithMany().HasForeignKey("RespuestaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Respuesta__Respu__46E78A0C"),
                        j =>
                        {
                            j.HasKey("RespuestaId", "OpcionRespuestaId").HasName("PK__Respuest__640A7CCFB85AF468");

                            j.ToTable("Respuesta_OpcionRespuesta", "Cuestionarios");

                            j.IndexerProperty<int>("RespuestaId").HasColumnName("RespuestaID");

                            j.IndexerProperty<int>("OpcionRespuestaId").HasColumnName("OpcionRespuestaID");
                        });
            });

            modelBuilder.Entity<RevisadorCuestionarioEF>(entity =>
            {
                entity.HasKey(e => new { e.Revisador, e.CuestionarioId })
                    .HasName("PK__Revisado__68332923B6A7C3F3");

                entity.ToTable("Revisador_Cuestionario", "Cuestionarios");

                entity.Property(e => e.Revisador)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CuestionarioId).HasColumnName("CuestionarioID");

                entity.HasOne(d => d.Cuestionario)
                    .WithMany(p => p.RevisadorCuestionarios)
                    .HasForeignKey(d => d.CuestionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Revisador__Cuest__4AB81AF0");
            });

            modelBuilder.Entity<SubcategoriaEF>(entity =>
            {
                entity.ToTable("Subcategoria", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Subcategorias)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subcatego__Categ__33D4B598");
            });

            modelBuilder.Entity<TipoCuestionarioEF>(entity =>
            {
                entity.ToTable("TipoCuestionario", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPreguntaEF>(entity =>
            {
                entity.ToTable("TipoPregunta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioRespuestaEF>(entity =>
            {
                entity.HasKey(e => e.RespuestaId)
                    .HasName("PK__Usuario___31F7FC318D4AB363");

                entity.ToTable("Usuario_Respuesta", "Cuestionarios");

                entity.Property(e => e.RespuestaId)
                    .ValueGeneratedNever()
                    .HasColumnName("RespuestaID");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Respuesta)
                    .WithOne(p => p.UsuarioRespuesta)
                    .HasForeignKey<UsuarioRespuestaEF>(d => d.RespuestaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario_R__Respu__440B1D61");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
