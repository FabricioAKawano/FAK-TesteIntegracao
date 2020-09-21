using FAK.TesteIntegracao.Core.Commands;
using FAK.TesteIntegracao.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FAK.TesteIntegracao.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : Controller
    {
        [HttpPost]
        public IActionResult EndpointCadastraTarefa(CadastraTarefaVM model)
        {
            var cmdObtemCateg = new ObtemCategoriaPorId(model.IdCategoria);
            //var categoria = new ObtemCategoriaPorIdHandler().Execute(cmdObtemCateg);
            //if (categoria == null)
            //{
            //    return NotFound("Categoria não encontrada");
            //}

            //var comando = new CadastraTarefa(model.Titulo, categoria, model.Prazo);
            //var handler = new CadastraTarefaHandler();
            //handler.Execute(comando);
            return Ok();
        }
    }
}
