using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using meta.challengeWebApi.Context;
using meta.challengeWebApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using meta.challengeWebApi.ViewModel;
using AutoMapper;

namespace meta.challengeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SqlContext _context;

        public ContatoController(IMapper mapper,
            SqlContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Retorna uma lista de registros
        /// de acordo com o informado nos parâmetros page e size.
        /// Se estes parâmetros não forem passados na consulta,
        /// os seguintes valores padrão serão utilizados: page = 0 e size = 10
        /// </summary>
        /// <param name="size"> Quantidade de registros a ser retornada em uma única página. </param>
        /// <param name="page"> Página onde se encontra o subconjunto de registros desejado. </param>
        // GET: api/Contato
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos(
            [FromQuery]int size = 10, [FromQuery]int page = 0)
        {
            try
            {
                var response = await _context.Contatos.ToListAsync();

                return Ok(response.OrderBy(c => c.Id)
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        /// <summary>
        /// Retorna um único objeto do tipo Contato.
        /// </summary>
        /// <param name="id">Id do contato cadastrado no banco.</param>
        // GET: api/Contato/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            try
            {
                var contato = await _context.Contatos.FindAsync(id);
                
                if (contato == null) return NotFound();

                return Ok(contato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Altera um objeto do tipo Contato.
        /// </summary>
        /// <param name="id">Identificador único de objetos do tipo Contato.</param>
        /// <param name="contatoPut">Objeto modificado que será atualizado no banco.</param>
        /// <returns></returns>
        // PUT: api/Contato/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutContato(int id, ContatoPut contatoPut)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var contato = _mapper.Map<Contato>(contatoPut);
                contato.Id = id;
                _context.Entry(contato).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (!ContatoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    Console.WriteLine(ex.Message);
                    return BadRequest();
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Cria um novo objeto do tipo Contato.
        /// </summary>
        /// <param name="contato">Objeto que será cadstrado.</param>
        // POST: api/Contato
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Contato>> PostContato(ContatoCreate request)
        {
            try
            {
                var contato = _mapper.Map<Contato>(request);
                _context.Contatos.Add(contato);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetContato", new { id = contato.Id }, contato);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        /// <summary>
        /// Apaga um objeto do tipo Contato.
        /// </summary>
        /// <param name="id">Identificador único de objetos do tipo Contato.</param>
        // DELETE: api/Contato/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteContato(int id)
        {
            try
            {
                var contato = await _context.Contatos.FindAsync(id);

                if (contato == null) return NotFound();

                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }

        }

        private bool ContatoExists(int id)
        {
            return _context.Contatos.Any(e => e.Id == id);
        }
    }
}
