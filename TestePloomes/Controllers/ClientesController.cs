using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestePloomes.Models;
using TestePloomes.Services;

namespace TestePloomes.Controllers
{
    [Route("api/Clientes")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _clientesService;

        public ClientesController(ClientesService clientesService)
        {
            _clientesService = clientesService;
        }

        // lista todos clientes
        [HttpGet("~/api/Clientes/Todos")]
        public ActionResult<List<Cliente>> Get() =>
            _clientesService.Get();

        // lista apenas clientes habilitados
        [HttpGet("~/api/Clientes/Habilitados")]
        public ActionResult<List<Cliente>> GetHabilitados()
        {
            var clientes = _clientesService.GetHabilitados();
            if (clientes == null)
            {
                return NotFound();
            }
            return clientes;
        }

        // lista apenas clientes desabilitados
        [HttpGet("~/api/Clientes/Desabilitados")]
        public ActionResult<List<Cliente>> GetDesabilitados()
        {
            var clientes = _clientesService.GetDesabilitados();
            if (clientes == null)
            {
                return NotFound();
            }
            return clientes;
        }
        // listar clientes habilitados com forma de contato específica
        [HttpGet("~/api/Clientes/{formaDeContato}")]
        public ActionResult<List<Cliente>> GetPorFormaDeContato(string formaDeContato)
        {
            var clientes = _clientesService.GetPorFormaDeContato(formaDeContato);

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }
        // listar formas de contato de um cliente específico
        [HttpGet("~/api/Clientes/Contatos/{id:length(24)}")]
        public ActionResult<List<Contato>> GetContatos(string id)
        {
            var contatos = _clientesService.GetContatos(id);

            if (contatos == null)
            {
                return NotFound();
            }

            return contatos;
        }

        // mostra 1 cliente pela ID
        [HttpGet("{id:length(24)}", Name = "GetCliente")]
        public ActionResult<Cliente> Get(string id)
        {
            var cliente = _clientesService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // criar cliente
        [HttpPost("~/api/Clientes/Criar")]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            if (cliente.Contatos == null || cliente.Contatos.Length == 0)
            {
                return BadRequest("Insira formas de contato.");
            }

            _clientesService.Create(cliente);

            return CreatedAtRoute("GetCliente", new { id = cliente.Id.ToString() }, cliente);
        }
        // atualizar cliente
        [HttpPut("~/api/Clientes/Atualizar/{id:length(24)}")]
        public IActionResult Update(string id, Cliente clienteIn)
        {
            var cliente = _clientesService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clientesService.Update(id, clienteIn);

            return NoContent();
        }
        // habilitar cliente
        [HttpPatch("~/api/Clientes/Habilitar/{id:length(24)}")]
        public IActionResult UpdateHabilitar(string id)
        {
            var cliente = _clientesService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clientesService.UpdateHabilitar(id);

            return NoContent();
        }
        // desabilitar cliente
        [HttpPatch("~/api/Clientes/Desabilitar/{id:length(24)}")]
        public IActionResult UpdateDesabilitar(string id)
        {
            var cliente = _clientesService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clientesService.UpdateDesabilitar(id);

            return NoContent();
        }
        // deleta registro
        [HttpDelete("~/api/Clientes/Deletar/{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var cliente = _clientesService.Get(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _clientesService.Remove(id);

            return NoContent();
        }

    }
}
