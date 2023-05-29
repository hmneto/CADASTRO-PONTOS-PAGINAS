using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace bahmapi.Entities
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Concessionaria> Concessionaria { get; set; }
        public virtual DbSet<Contato> Contato { get; set; }
        public virtual DbSet<EmailEnvio> EmailEnvio { get; set; }
        public virtual DbSet<Icone> Icone { get; set; }
        public virtual DbSet<Imagem> Imagem { get; set; }
        public virtual DbSet<LogMapa> LogMapa { get; set; }
        public virtual DbSet<LogPagina> LogPagina { get; set; }
        public virtual DbSet<LogPonto> LogPonto { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<PaginaContato> PaginaContato { get; set; }
        public virtual DbSet<PaginaSite> PaginaSite { get; set; }
        public virtual DbSet<Ponto> Ponto { get; set; }
        public virtual DbSet<Site> Site { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "test_db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PRIMARY");

                entity.ToTable("cliente");

                entity.HasIndex(e => e.NomeCliente)
                    .HasName("empresa")
                    .IsUnique();

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ChaveGoogleMaps)
                    .IsRequired()
                    .HasColumnName("chave_google_maps")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.IpCliente)
                    .IsRequired()
                    .HasColumnName("ip_cliente")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.NomeCliente)
                    .IsRequired()
                    .HasColumnName("nome_cliente")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<Concessionaria>(entity =>
            {
                entity.HasKey(e => e.IdConcessionaria)
                    .HasName("PRIMARY");

                entity.ToTable("concessionaria");

                entity.HasIndex(e => new { e.InfoConcessionaria, e.NomeConcessionaria })
                    .HasName("nome")
                    .IsUnique();

                entity.Property(e => e.IdConcessionaria)
                    .HasColumnName("id_concessionaria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InfoConcessionaria)
                    .IsRequired()
                    .HasColumnName("info_concessionaria")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.NomeConcessionaria)
                    .IsRequired()
                    .HasColumnName("nome_concessionaria")
                    .HasColumnType("varchar(60)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<Contato>(entity =>
            {
                entity.HasKey(e => e.IdContato)
                    .HasName("PRIMARY");

                entity.ToTable("contato");

                entity.Property(e => e.IdContato)
                    .HasColumnName("id_contato")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InfoContato)
                    .IsRequired()
                    .HasColumnName("info_contato")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<EmailEnvio>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("email_envio");

                entity.Property(e => e.Acao)
                    .IsRequired()
                    .HasColumnName("acao")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Host)
                    .IsRequired()
                    .HasColumnName("host")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IdEmailEnviado)
                    .HasColumnName("id_email_enviado")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Porta)
                    .HasColumnName("porta")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ssl)
                    .HasColumnName("ssl")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasColumnName("usuario")
                    .HasColumnType("varchar(70)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Icone>(entity =>
            {
                entity.HasKey(e => e.IdIcone)
                    .HasName("PRIMARY");

                entity.ToTable("icone");

                entity.HasIndex(e => e.LinkIcone)
                    .HasName("link")
                    .IsUnique();

                entity.HasIndex(e => e.NomeIcone)
                    .HasName("nome")
                    .IsUnique();

                entity.Property(e => e.IdIcone)
                    .HasColumnName("id_icone")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AcaoIcone)
                    .IsRequired()
                    .HasColumnName("acao_icone")
                    .HasColumnType("enum('SIM','NÃO')")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.LinkIcone)
                    .IsRequired()
                    .HasColumnName("link_icone")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.NomeIcone)
                    .IsRequired()
                    .HasColumnName("nome_icone")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");
            });

            modelBuilder.Entity<Imagem>(entity =>
            {
                entity.HasKey(e => e.IdImagem)
                    .HasName("PRIMARY");

                entity.ToTable("imagem");

                entity.HasIndex(e => e.NomeImagem)
                    .HasName("nome_imagem")
                    .IsUnique();

                entity.Property(e => e.IdImagem)
                    .HasColumnName("id_imagem")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BinarioImagem)
                    .IsRequired()
                    .HasColumnName("binario_imagem");

                entity.Property(e => e.ExtensaoImagem)
                    .IsRequired()
                    .HasColumnName("extensao_imagem")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeImagem)
                    .IsRequired()
                    .HasColumnName("nome_imagem")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<LogMapa>(entity =>
            {
                entity.HasKey(e => e.IdLogMapa)
                    .HasName("PRIMARY");

                entity.ToTable("log_mapa");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("usuario_id");

                entity.Property(e => e.IdLogMapa)
                    .HasColumnName("id_log_mapa")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHoraLogMapa)
                    .HasColumnName("data_hora_log_mapa")
                    .HasColumnType("datetime");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.LogMapa)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_mapa_ibfk_1");
            });

            modelBuilder.Entity<LogPagina>(entity =>
            {
                entity.HasKey(e => e.IdLogPagina)
                    .HasName("PRIMARY");

                entity.ToTable("log_pagina");

                entity.HasIndex(e => e.PaginaId)
                    .HasName("pagina_id");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("usuario_id");

                entity.Property(e => e.IdLogPagina)
                    .HasColumnName("id_log_pagina")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHora)
                    .HasColumnName("data_hora")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("current_timestamp()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.PaginaId)
                    .HasColumnName("pagina_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Pagina)
                    .WithMany(p => p.LogPagina)
                    .HasForeignKey(d => d.PaginaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_pagina_ibfk_2");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.LogPagina)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_pagina_ibfk_3");
            });

            modelBuilder.Entity<LogPonto>(entity =>
            {
                entity.HasKey(e => e.IdLogPonto)
                    .HasName("PRIMARY");

                entity.ToTable("log_ponto");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("usuario_id");

                entity.Property(e => e.IdLogPonto)
                    .HasColumnName("id_log_ponto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DataHora)
                    .HasColumnName("data_hora")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("current_timestamp()")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.LatLong)
                    .IsRequired()
                    .HasColumnName("lat_long")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.QtdPontos)
                    .HasColumnName("qtd_pontos")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UsuarioId)
                    .HasColumnName("usuario_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.LogPonto)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("log_ponto_ibfk_1");
            });

            modelBuilder.Entity<Pagina>(entity =>
            {
                entity.HasKey(e => e.IdPagina)
                    .HasName("PRIMARY");

                entity.ToTable("pagina");

                entity.HasIndex(e => e.ConcessionariaId)
                    .HasName("FK_paginas_concessionarias");

                entity.HasIndex(e => e.PaginaUsuarioId)
                    .HasName("pagina_usuario_id");

                entity.Property(e => e.IdPagina)
                    .HasColumnName("id_pagina")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ConcessionariaId)
                    .HasColumnName("concessionaria_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EnderecoPagina)
                    .IsRequired()
                    .HasColumnName("endereco_pagina")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.NomePagina)
                    .IsRequired()
                    .HasColumnName("nome_pagina")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.PaginaUsuarioId)
                    .HasColumnName("pagina_usuario_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Concessionaria)
                    .WithMany(p => p.Pagina)
                    .HasForeignKey(d => d.ConcessionariaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_paginas_concessionarias");

                entity.HasOne(d => d.PaginaUsuario)
                    .WithMany(p => p.Pagina)
                    .HasForeignKey(d => d.PaginaUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pagina_ibfk_1");
            });

            modelBuilder.Entity<PaginaContato>(entity =>
            {
                entity.HasKey(e => e.IdPaginaContato)
                    .HasName("PRIMARY");

                entity.ToTable("pagina_contato");

                entity.HasIndex(e => e.ContatoId)
                    .HasName("contato_id");

                entity.HasIndex(e => e.PaginaId)
                    .HasName("pagina_id_");

                entity.Property(e => e.IdPaginaContato)
                    .HasColumnName("id_pagina_contato")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContatoId)
                    .HasColumnName("contato_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaginaId)
                    .HasColumnName("pagina_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Contato)
                    .WithMany(p => p.PaginaContato)
                    .HasForeignKey(d => d.ContatoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pagina_contato_ibfk_1");

                entity.HasOne(d => d.Pagina)
                    .WithMany(p => p.PaginaContato)
                    .HasForeignKey(d => d.PaginaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pagina_contato_ibfk_2");
            });

            modelBuilder.Entity<PaginaSite>(entity =>
            {
                entity.HasKey(e => e.IdPaginaSite)
                    .HasName("PRIMARY");

                entity.ToTable("pagina_site");

                entity.HasIndex(e => e.PaginaId)
                    .HasName("FK_pagina_site_paginas");

                entity.HasIndex(e => e.SiteId)
                    .HasName("FK_pagina_site_sites");

                entity.Property(e => e.IdPaginaSite)
                    .HasColumnName("id_pagina_site")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PaginaId)
                    .HasColumnName("pagina_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SiteId)
                    .HasColumnName("site_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Pagina)
                    .WithMany(p => p.PaginaSite)
                    .HasForeignKey(d => d.PaginaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pagina_site_paginas");

                entity.HasOne(d => d.Site)
                    .WithMany(p => p.PaginaSite)
                    .HasForeignKey(d => d.SiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pagina_site_sites");
            });

            modelBuilder.Entity<Ponto>(entity =>
            {
                entity.HasKey(e => e.IdPonto)
                    .HasName("PRIMARY");

                entity.ToTable("ponto");

                entity.HasIndex(e => e.IconeId)
                    .HasName("icone_id");

                entity.HasIndex(e => e.PaginaId)
                    .HasName("FK_pontos_paginas");

                entity.HasIndex(e => e.PontoUsuarioId)
                    .HasName("usuario_id");

                entity.Property(e => e.IdPonto)
                    .HasColumnName("id_ponto")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IconeId)
                    .HasColumnName("icone_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LatitudePonto)
                    .HasColumnName("latitude_ponto")
                    .HasColumnType("float(8,6)");

                entity.Property(e => e.LongitudePonto)
                    .HasColumnName("longitude_ponto")
                    .HasColumnType("float(8,6)");

                entity.Property(e => e.NomePonto)
                    .IsRequired()
                    .HasColumnName("nome_ponto")
                    .HasColumnType("varchar(250)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.ObservacaoPonto)
                    .IsRequired()
                    .HasColumnName("observacao_ponto")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.PaginaId)
                    .HasColumnName("pagina_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PontoUsuarioId)
                    .HasColumnName("ponto_usuario_id")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Icone)
                    .WithMany(p => p.Ponto)
                    .HasForeignKey(d => d.IconeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ponto_ibfk_2");

                entity.HasOne(d => d.Pagina)
                    .WithMany(p => p.Ponto)
                    .HasForeignKey(d => d.PaginaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ponto_ibfk_1");

                entity.HasOne(d => d.PontoUsuario)
                    .WithMany(p => p.Ponto)
                    .HasForeignKey(d => d.PontoUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ponto_ibfk_3");
            });

            modelBuilder.Entity<Site>(entity =>
            {
                entity.HasKey(e => e.IdSite)
                    .HasName("PRIMARY");

                entity.ToTable("site");

                entity.HasIndex(e => e.LinkSite)
                    .HasName("link")
                    .IsUnique();

                entity.HasIndex(e => e.NomeSite)
                    .HasName("nome")
                    .IsUnique();

                entity.HasIndex(e => e.SiteUsuarioId)
                    .HasName("site_usuario_id");

                entity.Property(e => e.IdSite)
                    .HasColumnName("id_site")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LinkSite)
                    .IsRequired()
                    .HasColumnName("link_site")
                    .HasColumnType("varchar(287)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.NomeSite)
                    .IsRequired()
                    .HasColumnName("nome_site")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.Property(e => e.SiteUsuarioId)
                    .HasColumnName("site_usuario_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TipoSite)
                    .IsRequired()
                    .HasColumnName("tipo_site")
                    .HasColumnType("enum('STREET','FOTO','WIKIMAPIA_SAT','WIKIMAPIA_FRIO','PM','SITE','ABCR','CONCESSIONARIA','FOTO_MAPA','WIKIPEDIA','LEI')")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_unicode_ci");

                entity.HasOne(d => d.SiteUsuario)
                    .WithMany(p => p.Site)
                    .HasForeignKey(d => d.SiteUsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("site_ibfk_1");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.ClienteId)
                    .HasName("cliente_id");

                entity.HasIndex(e => e.EmailUsuario)
                    .HasName("email_usuario")
                    .IsUnique();

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .HasColumnType("int(11)")
                    .HasComment("ID DO USUÁRIO");

                entity.Property(e => e.ClienteId)
                    .HasColumnName("cliente_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EmailUsuario)
                    .IsRequired()
                    .HasColumnName("email_usuario")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PerfilUsuario)
                    .IsRequired()
                    .HasColumnName("perfil_usuario")
                    .HasColumnType("enum('admin','user')")
                    .HasDefaultValueSql("'user'")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SenhaUsuario)
                    .IsRequired()
                    .HasColumnName("senha_usuario")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("usuario_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
