using AutoMapper;
using LudusGestao.Domain.DTOs.evento.Local;
using LudusGestao.Domain.DTOs.evento.Recebivel;
using LudusGestao.Domain.DTOs.reserva.Cliente;
using LudusGestao.Domain.DTOs.reserva.Reservas;
using LudusGestao.Domain.Entities.eventos;

namespace LudusGestao.Application.Mappings.evento
{
    public class EventoMappingProfile : Profile
    {
        public EventoMappingProfile()
        {
            // Reserva
            CreateMap<CreateReservaDTO, Reserva>();
            CreateMap<UpdateReservaDTO, Reserva>();
            CreateMap<Reserva, ReservaDTO>()
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao.ToString()));

            // Cliente
            CreateMap<CreateClienteDTO, Cliente>();
            CreateMap<UpdateClienteDTO, Cliente>();
            CreateMap<Cliente, ClienteDTO>()
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao.ToString()));

            // Local
            CreateMap<CreateLocalDTO, Local>();
            CreateMap<UpdateLocalDTO, Local>();
            CreateMap<Local, LocalDTO>()
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao.ToString()));

            // Recebivel
            CreateMap<CreateRecebivelDTO, Recebivel>();
            CreateMap<UpdateRecebivelDTO, Recebivel>();
            CreateMap<Recebivel, RecebivelDTO>()
                .ForMember(dest => dest.Situacao, opt => opt.MapFrom(src => src.Situacao.ToString()));
        }
    }
}
