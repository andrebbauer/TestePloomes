using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestePloomes.Models;
using TestePloomes.Services;

namespace TestePloomes.Controllers
{
    [Route("api/Comunicações")]
    [ApiController]
    [Authorize]
    public class ComunicacaoController : ControllerBase
    {
        private readonly ComunicacaoService _comunicacaoServices;

        public ComunicacaoController(ComunicacaoService comunicacaoServices)
        {
            _comunicacaoServices = comunicacaoServices;
        }

        // lista todas comunicacoes
        [HttpGet("~/api/Comunicações/Todas")]
        public ActionResult<List<Comunicacao>> Get() =>
            _comunicacaoServices.Get();

        // lista comunicacoes de pelo menos 5 caracteres
        [HttpGet("~/api/Comunicações/TituloMaiorQueCinco")]
        public ActionResult<List<Comunicacao>> GetComunicacoesTituloMaiorQueCinco() =>
            _comunicacaoServices.GetComunicacoesTituloMaiorQueCinco();

        // lista comunicacoes realizadas por 1 cliente usando IdCliente
        [HttpGet("~/api/Comunicações/Cliente/{idCliente:length(24)}")]
        public ActionResult<List<Comunicacao>> GetComunicacoesCliente(string idCliente)
        {
            var comunicacoes = _comunicacaoServices.GetComunicacoesCliente(idCliente);
            if (comunicacoes == null)
            {
                return NotFound();
            }

            return comunicacoes;
        }

        // lista comunicacoes de 1 cliente (idCliente) por 1 forma de contato (formaDeContato)
        [HttpGet("~/api/Comunicações/Cliente/{idCliente:length(24)}%{formaDeContato}")]
        public ActionResult<List<Comunicacao>> GetComunicacoesForma(string idCliente, string formaDeContato)
        {
            var comunicacoes = _comunicacaoServices.GetComunicacoesForma(idCliente, formaDeContato);
            if (comunicacoes == null)
            {
                return NotFound();
            }

            return comunicacoes;
        }

        // lista comunicacoes com mesmo {titulo}
        [HttpGet("~/api/Comunicações/Titulo/{titulo}")]
        public ActionResult<List<Comunicacao>> GetComunicacoesTitulo(string titulo)
        {
            var comunicacoes = _comunicacaoServices.GetComunicacoesTitulo(titulo);
            if (comunicacoes == null)
            {
                return NotFound();
            }

            return comunicacoes;
        }

        // mostra 1 comunicacao pela Id
        [HttpGet("~/api/Comunicações/{id:length(24)}", Name = "GetComunicacao")]
        public ActionResult<Comunicacao> Get(string id)
        {
            var comunicacao = _comunicacaoServices.Get(id);

            if (comunicacao == null)
            {
                return NotFound();
            }

            return comunicacao;
        }
        // criar comunicacao
        [HttpPost("~/api/Comunicações/Criar")]
        public ActionResult<Comunicacao> Create(Comunicacao comunicacao)
        {
            _comunicacaoServices.Create(comunicacao);

            return CreatedAtRoute("GetComunicacao", new { id = comunicacao.Id.ToString() }, comunicacao);
        }

    }
}
