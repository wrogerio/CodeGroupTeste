using AutoMapper;
using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Infra.Interfaces;

namespace CodeGroupTeste.Application.Services;

public class JogoService : IJogoService
{
    private readonly IJogoRepository _repository;
    private readonly IMapper _mapper;

    public JogoService(IJogoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<JogoDto>> GetAll()
    {
        var jogoes = await _repository.GetAll();
        return _mapper.Map<List<JogoDto>>(jogoes);
    }

    public async Task<JogoDto> GetById(Guid id)
    {
        var jogo = await _repository.GetById(id);
        return _mapper.Map<JogoDto>(jogo);
    }

    public async Task<JogoDto> Create(JogoDto entityDto)
    {
        var jogoEntity = _mapper.Map<Jogo>(entityDto);
        await _repository.Create(jogoEntity);
        return _mapper.Map<JogoDto>(jogoEntity);
    }

    public async Task<JogoDto> Update(JogoDto entityDto)
    {
        var jogoEntity = _mapper.Map<Jogo>(entityDto);
        await _repository.Update(jogoEntity);
        return _mapper.Map<JogoDto>(jogoEntity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var jogo = await _repository.GetById(id);

        if (jogo == null)
            return false;

        await _repository.Delete(jogo.Id);
        return true;
    }
}
