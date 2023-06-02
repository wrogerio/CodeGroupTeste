using AutoMapper;
using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Domain.Entities;

namespace CodeGroupTeste.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Jogador, JogadorDto>().ReverseMap();
        CreateMap<Jogo, JogoDto>().ReverseMap();
    }
}
