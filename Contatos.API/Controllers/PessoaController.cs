using Contatos.Domain.DTOs;
using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers
{
    [Route("[controller]")]
    public class PessoaController : Controller
    {
        private IPessoaService _pessoaService;
        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet("BuscaPorId")]
        [AllowAnonymous]
        public async Task<JsonResult> BuscaPorId(int id)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var obj = _pessoaService.BuscaPorId(id);
                    return Json(RetornoApi.Sucesso(obj));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

        [HttpDelete("Excluir")]
        [AllowAnonymous]
        public async Task<JsonResult> Excluir(int id)
        {
            try
            {
                _pessoaService.Excluir(id);
                return await Task.Run(() =>
                {
                    return Json(RetornoApi.Sucesso(true));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

        [HttpPost("Salvar")]
        [AllowAnonymous]
        public async Task<JsonResult> Salvar([FromBody] Pessoa pessoa)
        {
            try
            {
                _pessoaService.Salvar(pessoa);
                return await Task.Run(() =>
                {
                    return Json(RetornoApi.Sucesso(true));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

        [HttpPost("ListaPessoas")]
        [AllowAnonymous]
        public async Task<JsonResult> ListaPessoas(int draw, int start, int length, string nome, string cpf)
        {
            try
            {
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                return await Task.Run(() =>
                {
                    var lista = _pessoaService.ListaPessoas(start, length, nome, cpf, sortColumn, sortColumnDirection);
                    return Json(new
                    {
                        status = true,
                        draw = draw,
                        recordsFiltered = lista.TotalRegistros,
                        recordsTotal = lista.TotalRegistros,
                        data = lista.Lista,
                        dataCounts = lista.TotalRegistros,
                    });
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }

    }
}
