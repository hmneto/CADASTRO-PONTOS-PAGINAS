using AutoMapper;
using bahmapi.Dtos;
using bahmapi.Entities;

namespace AutoMapperUserProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ClienteDto, Cliente>();

            CreateMap<Cliente, ClienteDto>();

            CreateMap<Concessionaria, ConcessionariaDto>();

            CreateMap<ConcessionariaDto, Concessionaria>();

            CreateMap<ContatoDto, Contato>();

            CreateMap<Contato, ContatoDto>();

            CreateMap<IconeDto, Icone>();

            CreateMap<Icone, IconeDto>();

            CreateMap<ImagemDto, Imagem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Imagem, ImagemDto>()
                .ForMember(x => x.BinarioImagem, opt => opt.Ignore())
                .ForMember(x => x.TamanhoImagem, opt => opt.Ignore())
                .ForMember(x => x.ExtensaoImagem, opt => opt.Ignore())
                .ForMember(x => x.FotoString, opt => opt.Ignore());


            CreateMap<PaginaContato, PaginaContatoDto>();

            CreateMap<PaginaContatoDto, PaginaContato>();

            CreateMap<PaginaDto, Pagina>()
                .ForMember(x => x.Concessionaria, opt => opt.Ignore())
                .ForMember(x => x.IdPagina, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Pagina, PaginaDto>();
                //.ForMember(x => x.DataHora, opt => opt.Ignore());

            CreateMap<PaginaSite, PaginaSiteDto>();

            CreateMap<PaginaSiteDto, PaginaSite>();

            CreateMap<PontoDto, Ponto>()
                .ForMember(x => x.Icone, opt => opt.Ignore())
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Ponto, PontoDto>();

            CreateMap<SiteDto, Site>();

            CreateMap<Site, SiteDto>();

            CreateMap<UsuarioDto, Usuario>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(x => x.SenhaUsuario, opt => opt.Ignore());

        }
    }
}