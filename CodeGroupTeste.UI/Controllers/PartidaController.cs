using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.Domain.Enums;
using CodeGroupTeste.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CodeGroupTeste.UI.Controllers;

public class PartidaController : Controller
{
    private readonly IJogoService _jogoService;
    private readonly IJogadorService _jogadorService;

    public PartidaController(IJogoService jogoService, IJogadorService jogadorService)
    {
        _jogoService = jogoService;
        _jogadorService = jogadorService;
    }

    public async Task<IActionResult> Index(Guid id)
    {
        var mensagem = "";

        if (id == Guid.Empty)
            return RedirectToAction("Index", "Home");

        var jogo = await _jogoService.GetById(id);

        if (jogo == null)
            return RedirectToAction("Index", "Home");

        var jogadores = await _jogadorService.GetAll();
        var jogadoresPartida = jogadores.Where(x => x.JogoId == id).ToList();

        var listaNiveis = new List<NivelEnum>();
        listaNiveis.Add(NivelEnum.Regular);
        listaNiveis.Add(NivelEnum.Excelente);
        listaNiveis.Add(NivelEnum.Fraco);
        listaNiveis.Add(NivelEnum.Bom);
        listaNiveis.Add(NivelEnum.Mestre);
        ViewBag.Niveis = new SelectList(listaNiveis);

        // if Request HttpContext Session has Mensagem
        if (Request.HttpContext.Session != null)
            mensagem = Request.HttpContext.Session.GetString("Mensagem");

        if (!string.IsNullOrWhiteSpace(mensagem))
        {
            ViewBag.Mensagem = mensagem;
            Request.HttpContext.Session?.Remove("Mensagem");
        }

        return View(new JogoViewModel { Jogo = jogo, Jogadores = jogadoresPartida });
    }

    public IActionResult Create()
    {
        return View();
    }

    // Create jogoDto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(JogoDto jogoDto)
    {
        if (string.IsNullOrWhiteSpace(jogoDto.Placar))
        {
            jogoDto.Placar = "0x0";
            ModelState.Remove("Placar");
        }

        if (ModelState.IsValid)
        {
            await _jogoService.Create(jogoDto);
            return RedirectToAction(nameof(Index));
        }
        return View(jogoDto);
    }


    public async Task<IActionResult> Edit(Guid id)
    {
        var jogoDto = await _jogoService.GetById(id);

        if (jogoDto == null)
            return NotFound();

        return View(jogoDto);
    }

    // Edit jogoDto
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(JogoDto jogoDto)
    {
        if (ModelState.IsValid)
        {
            await _jogoService.Update(jogoDto);
            return RedirectToAction(nameof(Index));
        }
        return View(jogoDto);
    }

    [HttpPost]
    public JsonResult Delete(Guid id)
    {
        try
        {
            var resposta = _jogoService.Delete(id).Result;
            return Json(new { success = resposta, message = "Delete Successful" });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Delete Failed" });
        }
    }

    [HttpPost]
    public async Task<JsonResult> GetPartidaDetails(Guid jogoId)
    {
        var jogo = await _jogoService.GetById(jogoId);
        var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        var result = JsonConvert.SerializeObject(jogo, Formatting.Indented, settings);

        return Json(result);
    }
}
