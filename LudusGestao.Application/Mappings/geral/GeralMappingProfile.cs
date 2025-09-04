using AutoMapper;
using LudusGestao.Domain.DTOs.Empresa;
using LudusGestao.Domain.DTOs.Filial;
using LudusGestao.Domain.DTOs.geral.GrupoPermissao;
using LudusGestao.Domain.DTOs.Usuario;
using LudusGestao.Domain.Entities.geral;
using LudusGestao.Domain.Entities.geral.permissao;

namespace LudusGestao.Application.Mappings.geral
{
    public class GeralMappingProfile : Profile
    {
        public GeralMappingProfile()
        {
            // Usuario
            CreateMap<CreateUsuarioDTO, Usuario>()
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
            CreateMap<UpdateUsuarioDTO, Usuario>()
                .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha));
            CreateMap<Usuario, UsuarioDTO>();

            // Empresa
            CreateMap<CreateEmpresaDTO, Empresa>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => $"{src.Rua}, {src.Numero}"))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.CEP));
            CreateMap<UpdateEmpresaDTO, Empresa>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => $"{src.Rua}, {src.Numero}"))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.CEP));
            CreateMap<Empresa, EmpresaDTO>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Rua, opt => opt.MapFrom(src => src.Endereco))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Cep, opt => opt.MapFrom(src => src.Cep));

            // Filial
            CreateMap<CreateFilialDTO, Filial>();
            CreateMap<UpdateFilialDTO, Filial>();
            CreateMap<Filial, FilialDTO>();

            // GrupoPermissao
            CreateMap<CreateGrupoPermissaoDTO, GrupoPermissao>();
            CreateMap<UpdateGrupoPermissaoDTO, GrupoPermissao>();
            CreateMap<GrupoPermissao, GrupoPermissaoDTO>();
        }
    }
}
