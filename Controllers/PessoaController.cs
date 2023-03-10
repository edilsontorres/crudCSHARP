using System.Threading.Tasks;
using FirstApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstApi.Models;


namespace FirstApi.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        
        private DataContext dc;

        public PessoaController(DataContext context)
        {
            this.dc = context;
        }

        //Rota Cadastro
        [HttpPost("api")]
        public async Task<ActionResult> cadastrar([FromBody] Pessoa p)
        {
            dc.pessoa?.Add(p);
            await dc.SaveChangesAsync();

            return Created("Objeto Criado", p);
        }

        //Rota lista
        [HttpGet("api")]
        public async Task<ActionResult> listar()
        {
            var dados = await dc.pessoa.ToListAsync();
            return Ok(dados);
        }

        //Rota filtro
        [HttpGet("api/{id}")]
        public Pessoa filtrar(int id)
        {
            Pessoa p = dc.pessoa.Find(id);
            return p;
        }

        //Rota edição
        [HttpPut("api")]
        public async Task<ActionResult> editar([FromBody] Pessoa p)
        {
            dc.pessoa.Update(p);
            await dc.SaveChangesAsync();

            return Ok(p);
        }

        //Rota deletar
        [HttpDelete("api/{id}")]
        public async Task<ActionResult> remover(int id)
        {
            Pessoa p = filtrar(id);

            if(p != null)
            {
                dc.pessoa?.Remove(p);
                await dc.SaveChangesAsync();
                return Ok("Usuario deletado com sucesso");
            }
                
            return NotFound("Usuario não encontrado");    
        }

       
    }
}