using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SIGESHA.Models.Entities.DBSIGESHA;

public partial class SIGESHADbContext : DbContext
{
    public SIGESHADbContext()
    {
    }

    public SIGESHADbContext(DbContextOptions<SIGESHADbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Clasificacion> Clasificacions { get; set; }

    public virtual DbSet<Instalacione> Instalaciones { get; set; }

    public virtual DbSet<Sha1> Sha1s { get; set; }

    public virtual DbSet<Sha2> Sha2s { get; set; }

    public virtual DbSet<Sha3> Sha3s { get; set; }

    public virtual DbSet<Sha4> Sha4s { get; set; }

    public virtual DbSet<Sha5> Sha5s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Clasificacion>(entity =>
        {
            entity.ToTable("Clasificacion");

            entity.Property(e => e.CodigoSha)
                .HasMaxLength(255)
                .HasColumnName("CodigoSHA");
            entity.Property(e => e.CodigoShaId).HasColumnName("CodigoSHA_Id");
            entity.Property(e => e.DescripciónSha)
                .HasMaxLength(255)
                .HasColumnName("Descripción SHA ");
            entity.Property(e => e.F6).HasMaxLength(255);
            entity.Property(e => e.InstalacionesMinsaCssPanamá)
                .HasMaxLength(255)
                .HasColumnName("Instalaciones MINSA - CSS Panamá");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .HasColumnName("Observaciones ");
        });

        modelBuilder.Entity<Instalacione>(entity =>
        {
            entity.Property(e => e.ClasificacionCdSSha)
                .HasMaxLength(255)
                .HasColumnName("clasificacionCdS-SHA");
            entity.Property(e => e.ClasificacionCtasNacId).HasColumnName("clasificacionCtasNac_id");
            entity.Property(e => e.CodigoInstalacion).HasColumnName("codigoInstalacion");
            entity.Property(e => e.CodigoSha1)
                .HasMaxLength(255)
                .HasColumnName("codigoSHA1");
            entity.Property(e => e.CodigoSha2)
                .HasMaxLength(255)
                .HasColumnName("codigoSHA2");
            entity.Property(e => e.CodigoSha2Id).HasColumnName("codigoSHA2_Id");
            entity.Property(e => e.CodigoSha3)
                .HasMaxLength(255)
                .HasColumnName("codigoSHA3");
            entity.Property(e => e.CodigoSha4)
                .HasMaxLength(255)
                .HasColumnName("codigoSHA4");
            entity.Property(e => e.CodigoShaId).HasColumnName("codigoSHA_Id");
            entity.Property(e => e.Columna1).HasMaxLength(255);
            entity.Property(e => e.Columna2).HasMaxLength(255);
            entity.Property(e => e.DependenciaInstalacion)
                .HasMaxLength(255)
                .HasColumnName("dependenciaInstalacion");
            entity.Property(e => e.DependenciaInstalacionId).HasColumnName("dependenciaInstalacion_id");
            entity.Property(e => e.InstalacionSalud)
                .HasMaxLength(255)
                .HasColumnName("instalacionSalud");
            entity.Property(e => e.InstalacionSaludId).HasColumnName("instalacionSalud_Id");
            entity.Property(e => e.NivelInstalacion)
                .HasMaxLength(255)
                .HasColumnName("nivelInstalacion");
            entity.Property(e => e.NivelInstalacionId).HasColumnName("nivelInstalacion_id");
            entity.Property(e => e.NomenclaturaSha)
                .HasMaxLength(255)
                .HasColumnName("nomenclaturaSHA");
            entity.Property(e => e.TipoInstalacion)
                .HasMaxLength(255)
                .HasColumnName("tipoInstalacion");
            entity.Property(e => e.TipoInstalacionId).HasColumnName("tipoInstalacion_id");
            entity.Property(e => e.TitularidadInstalacion)
                .HasMaxLength(255)
                .HasColumnName("titularidadInstalacion");
            entity.Property(e => e.TitularidadInstalacionId).HasColumnName("titularidadInstalacion_id");
        });

        modelBuilder.Entity<Sha1>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("SHA1");

            entity.Property(e => e.IdPk).HasColumnName("Id_PK");
            entity.Property(e => e.Codigo).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Sha2>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("SHA2");

            entity.Property(e => e.IdPk).HasColumnName("Id_PK");
            entity.Property(e => e.Codigo).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sha1Id).HasColumnName("sha1_id");
        });

        modelBuilder.Entity<Sha3>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("SHA3");

            entity.Property(e => e.IdPk).HasColumnName("Id_PK");
            entity.Property(e => e.Codigo).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sha1Id).HasColumnName("sha1_id");
            entity.Property(e => e.Sha2Id).HasColumnName("sha2_id");
        });

        modelBuilder.Entity<Sha4>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("SHA4");

            entity.Property(e => e.IdPk).HasColumnName("Id_PK");
            entity.Property(e => e.Codigo).HasMaxLength(255);
            entity.Property(e => e.F6).HasMaxLength(255);
            entity.Property(e => e.F7).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sha1Id).HasColumnName("sha1_id");
            entity.Property(e => e.Sha2Id).HasColumnName("sha2_id");
            entity.Property(e => e.Sha3Id).HasColumnName("sha3_id");
        });

        modelBuilder.Entity<Sha5>(entity =>
        {
            entity.HasKey(e => e.IdPk);

            entity.ToTable("SHA5");

            entity.Property(e => e.IdPk).HasColumnName("Id_PK");
            entity.Property(e => e.Codigo).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Sha1Id).HasColumnName("sha1_id");
            entity.Property(e => e.Sha2Id).HasColumnName("sha2_id");
            entity.Property(e => e.Sha3Id).HasColumnName("sha3_id");
            entity.Property(e => e.Sha4Id).HasColumnName("sha4_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
