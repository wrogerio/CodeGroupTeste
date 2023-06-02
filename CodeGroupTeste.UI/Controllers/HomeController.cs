using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Infra.Interfaces;
using CodeGroupTeste.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeGroupTeste.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJogoService _jogoService;

        public HomeController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        public async Task<IActionResult> Index()
        {
            var jogos = await _jogoService.GetAll();
            jogos = jogos.ToList().OrderByDescending(x => x.DtPartida).ToList();
            return View(jogos);
        }
    }
}