using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Infra.Context;
using CodeGroupTeste.Infra.Interfaces;

namespace CodeGroupTeste.Infra.Repository
{
    public class JogadorRepository : RepositoryBase<Jogador>, IJogadorRepository
    {
        public JogadorRepository(CodeGroupContext context) : base(context)
        {
        }
    }
}
