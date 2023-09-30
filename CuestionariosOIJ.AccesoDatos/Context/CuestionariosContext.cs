using System;
using System.Collections.Generic;
using CuestionariosOIJ.API.Models;
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

        public virtual DbSet<CategoriaEF> Categoria { get; set; } = null!;
        public virtual DbSet<CuestionarioEF> Cuestionarios { get; set; } = null!;
        public virtual DbSet<OficinaEF> Oficinas { get; set; } = null!;
        public virtual DbSet<OpcionRespuestaEF> OpcionRespuesta { get; set; } = null!;
        public virtual DbSet<PermisoEF> Permisos { get; set; } = null!;
        public virtual DbSet<PreguntaEF> Pregunta { get; set; } = null!;
        public virtual DbSet<RespuestaEF> Respuesta { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<SubcategoriaEF> Subcategoria { get; set; } = null!;
        public virtual DbSet<TipoCuestionarioEF> TipoCuestionarios { get; set; } = null!;
        public virtual DbSet<TipoPregunta> TipoPregunta { get; set; } = null!;
        public virtual DbSet<UsuarioEF> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=localhost; database=db_cuestionarios; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaEF>(entity =>
            {
                entity.ToTable("Categoria", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuestionarioEF>(entity =>
            {
                entity.ToTable("Cuestionario", "Cuestionarios");

                entity.HasIndex(e => e.Codigo, "UQ__Cuestion__06370DAC62654E72")
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

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.OficinaId).HasColumnName("OficinaID");

                entity.Property(e => e.TipoCuestionarioId).HasColumnName("TipoCuestionarioID");

                entity.HasOne(d => d.Oficina)
                    .WithMany(p => p.Cuestionarios)
                    .HasForeignKey(d => d.OficinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuestiona__Ofici__4D94879B");

                entity.HasOne(d => d.TipoCuestionario)
                    .WithMany(p => p.Cuestionarios)
                    .HasForeignKey(d => d.TipoCuestionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cuestiona__TipoC__4CA06362");
            });

            modelBuilder.Entity<OficinaEF>(entity =>
            {
                entity.ToTable("Oficina", "Administracion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

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
                    .WithMany(p => p.OpcionRespuesta)
                    .HasForeignKey(d => d.PreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OpcionRes__Pregu__5CD6CB2B");
            });

            modelBuilder.Entity<PermisoEF>(entity =>
            {
                entity.ToTable("Permiso", "Administracion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Entity)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
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
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPreguntaId).HasColumnName("TipoPreguntaID");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK__Pregunta__Catego__571DF1D5");

                entity.HasOne(d => d.Cuestionario)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.CuestionarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pregunta__Cuesti__59FA5E80");

                entity.HasOne(d => d.Subcategoria)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.SubcategoriaId)
                    .HasConstraintName("FK__Pregunta__Subcat__5812160E");

                entity.HasOne(d => d.TipoPregunta)
                    .WithMany(p => p.Pregunta)
                    .HasForeignKey(d => d.TipoPreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Pregunta__TipoPr__59063A47");
            });

            modelBuilder.Entity<RespuestaEF>(entity =>
            {
                entity.ToTable("Respuesta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaEliminada).HasColumnType("datetime");

                entity.Property(e => e.FechaRespondida).HasColumnType("datetime");

                entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");

                entity.Property(e => e.TextoRespuesta)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

                entity.HasOne(d => d.Pregunta)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.PreguntaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Respuesta__Pregu__5FB337D6");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Respuesta)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Respuesta__Usuar__60A75C0F");

                entity.HasMany(d => d.OpcionRespuesta)
                    .WithMany(p => p.Respuesta)
                    .UsingEntity<Dictionary<string, object>>(
                        "RespuestaOpcionRespuestum",
                        l => l.HasOne<OpcionRespuestaEF>().WithMany().HasForeignKey("OpcionRespuestaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Respuesta__Opcio__6477ECF3"),
                        r => r.HasOne<RespuestaEF>().WithMany().HasForeignKey("RespuestaId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Respuesta__Respu__6383C8BA"),
                        j =>
                        {
                            j.HasKey("RespuestaId", "OpcionRespuestaId").HasName("PK__Respuest__640A7CCF87B1ED7F");

                            j.ToTable("Respuesta_OpcionRespuesta", "Cuestionarios");

                            j.IndexerProperty<int>("RespuestaId").HasColumnName("RespuestaID");

                            j.IndexerProperty<int>("OpcionRespuestaId").HasColumnName("OpcionRespuestaID");
                        });
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol", "Administracion");

                entity.HasIndex(e => e.Nombre, "UQ__Rol__75E3EFCF7DC28CAA")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasMany(d => d.Permisos)
                    .WithMany(p => p.Rols)
                    .UsingEntity<Dictionary<string, object>>(
                        "RolPermiso",
                        l => l.HasOne<PermisoEF>().WithMany().HasForeignKey("PermisoId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Rol_Permi__Permi__4316F928"),
                        r => r.HasOne<Rol>().WithMany().HasForeignKey("RolId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Rol_Permi__RolID__4222D4EF"),
                        j =>
                        {
                            j.HasKey("RolId", "PermisoId").HasName("PK__Rol_Perm__D04D0EA14568C50A");

                            j.ToTable("Rol_Permiso", "Administracion");

                            j.IndexerProperty<int>("RolId").HasColumnName("RolID");

                            j.IndexerProperty<int>("PermisoId").HasColumnName("PermisoID");
                        });

                entity.HasMany(d => d.Usuarios)
                    .WithMany(p => p.Rols)
                    .UsingEntity<Dictionary<string, object>>(
                        "UsuarioRol",
                        l => l.HasOne<UsuarioEF>().WithMany().HasForeignKey("UsuarioId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Usuario_R__Usuar__46E78A0C"),
                        r => r.HasOne<Rol>().WithMany().HasForeignKey("RolId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Usuario_R__RolID__45F365D3"),
                        j =>
                        {
                            j.HasKey("RolId", "UsuarioId").HasName("PK__Usuario___6B90DCA8B91B8C9D");

                            j.ToTable("Usuario_Rol", "Administracion");

                            j.IndexerProperty<int>("RolId").HasColumnName("RolID");

                            j.IndexerProperty<int>("UsuarioId").HasColumnName("UsuarioID");
                        });
            });

            modelBuilder.Entity<SubcategoriaEF>(entity =>
            {
                entity.ToTable("Subcategoria", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Subcategoria)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subcatego__Categ__5441852A");
            });

            modelBuilder.Entity<TipoCuestionarioEF>(entity =>
            {
                entity.ToTable("TipoCuestionario", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPregunta>(entity =>
            {
                entity.ToTable("TipoPregunta", "Cuestionarios");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioEF>(entity =>
            {
                entity.ToTable("Usuario", "Administracion");

                entity.HasIndex(e => e.NombreUsuario, "UQ__Usuario__6B0F5AE0124DB9E5")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OficinaId).HasColumnName("OficinaID");

                entity.Property(e => e.PrimerApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Oficina)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.OficinaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_oficina_usuario");

                entity.HasMany(d => d.Cuestionarios)
                    .WithMany(p => p.Revisadores)
                    .UsingEntity<Dictionary<string, object>>(
                        "RevisadorCuestionario",
                        l => l.HasOne<CuestionarioEF>().WithMany().HasForeignKey("CuestionarioId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Revisador__Cuest__03F0984C"),
                        r => r.HasOne<UsuarioEF>().WithMany().HasForeignKey("RevisadorId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Revisador__Revis__02FC7413"),
                        j =>
                        {
                            j.HasKey("RevisadorId", "CuestionarioId").HasName("PK__Revisado__92BB75290CC4BA36");

                            j.ToTable("Revisador_Cuestionario", "Cuestionarios");

                            j.IndexerProperty<int>("RevisadorId").HasColumnName("RevisadorID");

                            j.IndexerProperty<int>("CuestionarioId").HasColumnName("CuestionarioID");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
