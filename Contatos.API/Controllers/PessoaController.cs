using Contatos.Domain.DTOs;
using Contatos.Domain.Entities;
using Contatos.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contatos.API.Controllers
{
    [ApiController]
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

        [HttpPost("Excluir")]
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

        [HttpGet("ListaPessoa")]
        [AllowAnonymous]
        public async Task<JsonResult> ListaPessoa()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var lista = _pessoaService.ListaPessoa();
                    return Json(RetornoApi.Sucesso(lista));
                });
            }
            catch (Exception e)
            {
                return Json(RetornoApi.Erro(e.Message));
            }
        }




    }
}
