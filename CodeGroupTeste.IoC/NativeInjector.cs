using CodeGroupTeste.Application.AutoMapper;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.Application.Services;
using CodeGroupTeste.Infra.Context;
using CodeGroupTeste.Infra.Interfaces;
using CodeGroupTeste.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGroupTeste.IoC;

public class NativeInjector
{
    public static void RegisterApp(IServiceCollection service, IConfiguration configuration)
    {
        // Context
        service.AddDbContext<CodeGroupContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repository
        service.AddScoped<IJogadorRepository, JogadorRepository>();
        service.AddScoped<IJogoRepository, JogoRepository>();

        // Services
        service.AddScoped<IJogoService, JogoService>();
        service.AddScoped<IJogadorService, JogadorService>();

        // AutoMapper
        service.AddAutoMapper(typeof(MappingProfile));
    }
}
