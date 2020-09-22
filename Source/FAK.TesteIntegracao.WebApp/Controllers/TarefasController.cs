using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.Infrastructure;
using FAK.TesteIntegracao.Services.Handlers;
using FAK.TesteIntegracao.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FAK.TesteIntegracao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : Controller
    {
        private readonly IRepositorioTarefas _repositorioTarefas;
        private readonly ILogger<CadastraTarefaHandler> _logger;

        public TarefasController(IRepositorioTarefas repositorioTarefas, ILogger<CadastraTarefaHandler> logger)
        {
            _repositorioTarefas = repositorioTarefas;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult EndpointCadastraTarefa(CadastraTarefaVM model)
        {
            var cmdObtemCateg = new ObtemCategoriaPorId(model.IdCategoria);
            var categoria = new ObtemCategoriaPorIdHandler(_repositorioTarefas).Execute(cmdObtemCateg);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            var comando = new CadastraTarefa(model.Titulo, categoria, model.Prazo);
            var handler = new CadastraTarefaHandler(_repositorioTarefas, _logger);
            var resultado = handler.Execute(comando);
            if (resultado.IsSucceded)
            {
                return Ok();
            }

            return StatusCode(500);
        }
    }
}
