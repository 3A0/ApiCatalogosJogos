using ApiCatalogosJogos.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogosJogos.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IjogoService _JogoService;

        public JogosController(IJogosService jogoService)
        {
            _JogoService = jogoService;
        }

        [HttpGet]
        //public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter((FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQueryAttribute, Range(1, 50)] int quantidade = 5)
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Obter((FromQuery, Range(1, int.MaxValue)])
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);
            if (jogos.Count() == 0) 
                return NoContent();

            return Ok(result);
    }

    [HttpGet("{IdJogo:guid}")]
    public async Task<ActionResult<JogoViewModel>> Obter([FromRoute] Guid IdJogo)
    {
        var jogo = await _jogoService.Obter(IdJogo);

        if (jogo == null)
            return NoContentResult();

        return Ok(jogo);

    }


    [HttpPost]
    public async Task<ActionResult<JogoviewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
    {
        try
        {
            var jogo = await _jogoService.Inserir(jogoInputModel);

            return Ok(jogo);


        }
        catch (JogoJaCadastradoException ex)
        
        {
            return UnprocessableEntity("Já existe um jogo com este nome para esta produtora");
        }
    }

    [HttpPut("{idJogo:guid}")]
    public async Task<ActionResult> AtualizarJogo([FromRoute] Guid IdJogo, [FromBody] JogoInputModel jogoInputModel)
    {
        try
        {
            await _jogoService.Atualizar(IdJogo, JogoInputModel);

        }
        catch (JogoNaoCadastradoException ex)
        {
            return NotFoundObjectResult("Não existe este jogo");
        }

        return Ok();
    }

    [HttpPatch("{idJogo:guid}/preco/{preco:double}")]

    public async Task<ActionResult> AtualizarJogo(Guid idJogo, double preco)
    {
        try
        {
            await _jogoService.Atualizar(idJogo, preco);
            return Ok();
        }
        catch (JogoNaoCadastradoException ex)
        {
            return NotFound("Não extiste este jogo");

        }


    }

    [HttpDelete] ("{idJogo:guid}")]
            public async Task<ActionResult> ApagarJogo([FromRoute] Guid idJogo)
    {
        try
        {
            await _jogoService.Remover(idJogo);

            return Ok();

        }
            catch (JogoNaoCadastradoException ex)
       
        {
            return NotFoundObjectResult("Não existe este jogo");
        }

    }
} 

