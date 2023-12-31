using Api.Contact.Domain.UsuarioProfile;
using Api.Contact.Models;
using Api.Contact.Repository;
using AutoMapper;
using Infrastructure.Domain;
using Infrastructure.DTO.V1.Response;
using Infrastructure.Helpers;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Api.Contact.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Api/{version:apiVersion}/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVouchersWrapper _context;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IMapper mapper, IVouchersWrapper wrapper, ILogger<UsuarioController> logger)
        {
            _mapper = mapper;           
            _context = wrapper;
            _logger = logger;
        }

        /// <summary>
        /// Consulta todos los Usuario
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioDTO>> GetAll(
            [FromHeader] List<Sort> orderby = null
             , [FromQuery] UsuarioQuery filter = null
           )
        {
            try
            {

                if (orderby is not null)
                {
                    try
                    {
                        StringValues value;
                        Request.Headers.TryGetValue("orderby", out value);
                        if (value.Count > 0)
                            orderby = JsonSerializer.Deserialize<List<Sort>>(value);
                    }
                    catch
                    {
                    }

                }

           
                var predicate = PredicateBuilder.New<Usuario>(true);
                if (filter != null)
                {

                    if (!string.IsNullOrEmpty(filter.TextSearch))
                        predicate = predicate.And(a =>
                                            a.User.ToLower().Contains(filter.TextSearch.Trim().ToLower())                                            
                                            );

                    if (!string.IsNullOrEmpty(filter.User))
                        predicate = predicate.And(a =>
                                            a.User.ToLower().Contains(filter.User.Trim().ToLower())
                                            );


                }

                if (orderby == null || orderby.Count == 0)
                    orderby = new List<Sort>() { new Sort("Id", false) };

                var response = await _context.Usuario.GetAllAsync(orderby, predicate, x=>x.Contacto);



                return Ok(_mapper.Map<List<UsuarioDTO>>(response));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }
        }

        /// <summary>
        /// Consulta un Usuario especifico
        /// </summary>
        /// <param name="id">
        /// Representa la clave un Usuario
        /// </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<UsuarioDTO>> GetId(Guid id)
        {
            try
            {
                var data = await _context.Usuario.FirstOrDefaultAsync(expression: x => x.Id == id
                                               , x => x.Contacto
                                                );
                var response = _mapper.Map<UsuarioDTO>(data);

                if (response == null)
                    return NotFound();
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }

        }

        /// <summary>
        /// Crea un nuevo Usuario
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioDTORequest UsuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _mapper.Map<Usuario>(UsuarioDTO);
                    obj.Id = Guid.NewGuid();

                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(obj.Password);
                        byte[] hashBytes = sha256Hash.ComputeHash(bytes);
                        obj.Password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    }
                    obj.Contacto.ForEach(x => x.UsuarioId = obj.Id);

                    await _context.Usuario.InsertAsync(obj);
                    _context.Save();                   

                    return CreatedAtAction(nameof(GetId), new { id = obj.Id }, _mapper.Map<UsuarioDTO>(obj));
                }
                else
                    return BadRequest(ModelState.ValidationState);


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }
        }

        /// <summary>
        /// Actualiza un Usuario
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UsuarioDTOUpdate UsuarioDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (id != UsuarioDTO.Id)
                        return BadRequest("El id no coincide con el objeto enviado");


                    var _obj = await _context.Usuario.FirstOrDefaultAsync(a => a.Id == id);
                    if (_obj is null)
                        return NotFound();

                    using (SHA256 sha256Hash = SHA256.Create())
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(UsuarioDTO.Password);
                        byte[] hashBytes = sha256Hash.ComputeHash(bytes);
                        _obj.Password = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                    }                     

                    await _context.Usuario.UpdateAsync(_obj);
                    await _context.Usuario.SaveChangeAsync();
                    return NoContent();


                }
                else
                    return BadRequest(ModelState.ValidationState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var _obj = await _context.Usuario.FirstOrDefaultAsync(a => a.Id == id );
                    if (_obj is null)
                        return NotFound();

                    _obj.Deleted = true;
                    await _context.Usuario.UpdateAsync(_obj);
                    await _context.Usuario.SaveChangeAsync();
                    return NoContent();

                }
                else
                    return BadRequest(ModelState.ValidationState);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }
        }
    }
}
