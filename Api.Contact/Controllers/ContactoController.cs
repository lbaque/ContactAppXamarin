using Api.Contact.Domain.ContactoProfile;
using Api.Contact.Models;
using Api.Contact.Repository;
using AutoMapper;
using Infrastructure.Domain;
using Infrastructure.DTO.V1.Response;
using Infrastructure.Helpers;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text.Json;

namespace Api.Contact.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("Api/{version:apiVersion}/[controller]")]
    public class ContactoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVouchersWrapper _context;
        private readonly ILogger<ContactoController> _logger;

        public ContactoController(IMapper mapper, IVouchersWrapper wrapper, ILogger<ContactoController> logger)
        {
            _mapper = mapper;           
            _context = wrapper;
            _logger = logger;
        }

        /// <summary>
        /// Consulta todos los Contacto
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ContactoDTO>> GetAll(
            [FromHeader] List<Sort> orderby = null
             , [FromQuery] ContactoQuery filter = null
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

           
                var predicate = PredicateBuilder.New<Contacto>(true);
                if (filter != null)
                {

                    if (!string.IsNullOrEmpty(filter.TextSearch))
                        predicate = predicate.And(a =>
                                            a.Nombre.ToLower().Contains(filter.TextSearch.Trim().ToLower())
                                            || a.Apellido.ToLower().Contains(filter.TextSearch.Trim().ToLower())
                                            || a.Telefono.ToLower().Contains(filter.TextSearch.Trim().ToLower())
                                            );

                    if (Guid.Empty != filter.UsuarioId)
                        predicate = predicate.And(a => a.UsuarioId == filter.UsuarioId);


                }

                if (orderby == null || orderby.Count == 0)
                    orderby = new List<Sort>() { new Sort("Id", false) };

                var response = await _context.Contacto.GetAllAsync(orderby, predicate);



                return Ok(_mapper.Map<List<ContactoDTO>>(response));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Parametros.AuditoriaError(_logger, ex));
            }
        }

        /// <summary>
        /// Consulta un Contacto especifico
        /// </summary>
        /// <param name="id">
        /// Representa la clave un Contacto
        /// </param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactoDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<ActionResult<ContactoDTO>> GetId(Guid id)
        {
            try
            {
                var data = await _context.Contacto.FirstOrDefaultAsync(expression: x => x.Id == id );
                var response = _mapper.Map<ContactoDTO>(data);

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
        /// Crea un nuevo Contacto
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ContactoDTO>> Create([FromBody] ContactoDTORequest ContactoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var obj = _mapper.Map<Contacto>(ContactoDTO);

                    await _context.Contacto.InsertAsync(obj);
                    _context.Save();

                    return CreatedAtAction(nameof(GetId), new { id = obj.Id }, _mapper.Map<ContactoDTO>(obj));
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
        /// Actualiza un Contacto
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] ContactoDTOUpdate ContactoDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (id != ContactoDTO.Id)
                        return BadRequest("El id no coincide con el objeto enviado");


                    var _obj = await _context.Contacto.FirstOrDefaultAsync(a => a.Id == id);
                    if (_obj is null)
                        return NotFound();                    

                    _obj.Nombre = ContactoDTO.Nombre;                
                    _obj.Apellido = ContactoDTO.Apellido;                
                    _obj.Telefono = ContactoDTO.Telefono;                
                    _obj.Celular = ContactoDTO.Celular;                
                    _obj.Foto = ContactoDTO.Foto;                
                    _obj.UsuarioId = ContactoDTO.UsuarioId;                
                    

                    await _context.Contacto.UpdateAsync(_obj);
                    await _context.Contacto.SaveChangeAsync();
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
                    var _obj = await _context.Contacto.FirstOrDefaultAsync(a => a.Id == id );
                    if (_obj is null)
                        return NotFound();

                    _obj.Deleted = true;
                    _obj.CreatedAt = DateTime.Now;
                    await _context.Contacto.UpdateAsync(_obj);
                    await _context.Contacto.SaveChangeAsync();
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
