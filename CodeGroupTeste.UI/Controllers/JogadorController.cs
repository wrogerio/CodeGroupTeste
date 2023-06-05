using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeGroupTeste.UI.Controllers;

public class JogadorController : Controller
{
    private readonly IJogadorService _jogadorService;
    private readonly IJogoService _jogoService;

    public JogadorController(IJogadorService jogadorService, IJogoService jogoService)
    {
        _jogadorService = jogadorService;
        _jogoService = jogoService;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit(Guid? jogadorId, JogoViewModel jogoViewModel)
    {
        try
        {
            if (jogadorId != null && jogadorId != Guid.Empty)
            {
                jogoViewModel.Jogador.Id = jogadorId.Value;

                if (jogoViewModel.Jogador.IsGoleiro && !ValidarGoleiro(jogoViewModel.Jogador.JogoId))
                {
                    return RedirectToAction("Index", "Partida", new { id = jogoViewModel.Jogador.JogoId });
                }

                await _jogadorService.Update(jogoViewModel.Jogador);
                return RedirectToAction("Index", "Partida", new { id = jogoViewModel.Jogador.JogoId });

                // add mensagem to session
                Request.HttpContext.Session.SetString("Mensagem", "Não é possível adicionar o goleiro. Já tem goleiro suficiente");

                return RedirectToAction("Index", "Partida", new { id = jogoViewModel.Jogador.JogoId });
            }
            else
            {
                await _jogadorService.Create(jogoViewModel.Jogador);
                return RedirectToAction("Index", "Partida", new { id = jogoViewModel.Jogador.JogoId });
            }
        }
        catch (Exception)
        {
            return RedirectToAction("Index", "Partida", new { id = jogoViewModel.Jogador.JogoId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(JogadorDto jogadorDto)
    {
        if (ModelState.IsValid)
        {
            await _jogadorService.Update(jogadorDto);
            return RedirectToAction("Index", "Partida", new { id = jogadorDto.JogoId });
        }
        return RedirectToAction("AddOrEdit", new { id = jogadorDto.Id });
    }

    //Delete
    [HttpPost]
    public JsonResult Delete(Guid jogadorId)
    {
        try
        {
            var resposta = _jogadorService.Delete(jogadorId).Result;
            return Json(new { success = resposta, message = "Delete Successful" });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Delete Failed" });
        }
    }

    public bool ValidarGoleiro(Guid timeId)
    {
        var partidaDetail = _jogoService.GetById(timeId).Result;
        var qtdPorTime = partidaDetail.QtdPorTime;
        var jogadoresConfirmados = _jogadorService.GetAll().Result.Where(x => x.JogoId == timeId && x.IsConfirmado == true).ToList().Count;
        var qtdTimesCriados = Math.Ceiling((double)jogadoresConfirmados / qtdPorTime);
        var qtdGoleirosConfirmados = _jogadorService.GetAll().Result.Where(x => x.JogoId == timeId && x.IsConfirmado == true && x.IsGoleiro == true).ToList().Count;

        return qtdGoleirosConfirmados < qtdTimesCriados;
    }
}
