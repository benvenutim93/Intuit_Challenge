using Intuit.API.Validadores;
using Intuit.EF;
using Intuit.Models;
using Intuit.Negocio;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Intuit.API.Controllers
{
    [Route("api/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public Serilog.ILogger Logger { get; set; }
        public ClientesController(Serilog.ILogger _logger)
        {
            Logger = _logger;
        }
        /// <summary>
        /// Obtiene todos los clientes de la BD
        /// </summary>
        /// <response code="200">OK. Se obtuvieron los clientes.</response>      
        /// <response code="204">No Content. No hay clientes cargados en la BD.</response>    
        /// <response code="500">Internal Server Error. Error en el acceso a los datos.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                Logger.Debug("Entre al Get All");
                var listaClientes = new ClienteNegocio().GetAll();

                if (listaClientes.Count() > 0)
                {
                    return Ok(listaClientes);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Error al obtener los datos --> {ex.Message}");
                return StatusCode(500, ModelState);
            }
            
        }

        /// <summary>
        /// Obtiene un cliente segun su Id desde la BD
        /// </summary>
        /// <response code="200">OK. Devuelve el cliente buscado.</response>      
        /// <response code="204">No Content. No existe el cliente con ese ID solicitado.</response>    
        /// <response code="500">Internal Server Error. Error en el acceso a los datos.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("id/{pId}")]
        public IActionResult GetById(int pId)
        {
            try
            {
                var cliente = new ClienteNegocio().GetById(pId);

                if (cliente != null)
                {
                    return Ok(cliente);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Error al obtener los datos --> {ex.Message}");
                return StatusCode(500, ModelState);
            }
           
        }

        /// <summary>
        /// Busca clientes en la BD, haciendo coincidir el parametro buscado con el Nombre, 
        /// Apellido, Cuit, telefono o mail
        /// </summary>
        /// <response code="200">OK. Devuelve una lista de clientes que coincidan con el parametro buscado.</response>      
        /// <response code="204">No Content. No existen clientes según el parámetro buscado.</response> 
        /// <response code="400">Bad Request. El parámetro a buscar se encuentra vacio o es null</response> 
        /// <response code="500">Internal Server Error. Error en el acceso a los datos.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("search/{pSearch}")]
        public IActionResult Search(string pSearch)
        {
            try
            {
                if (string.IsNullOrEmpty(pSearch))
                {
                    return BadRequest();
                }

                string searchMin = pSearch.ToLower().Trim();
                var listaClientesBuscados = new ClienteNegocio().GetAllByCondition(c => c.Nombres.ToLower().Contains(searchMin) ||
                                                                                        c.Apellidos.ToLower().Contains(searchMin) ||
                                                                                        c.Cuit.ToLower().Contains(searchMin) ||
                                                                                        c.Domicilio.ToLower().Contains(searchMin) ||
                                                                                        c.Telefono!.ToLower().Contains(searchMin) ||
                                                                                        c.Email!.ToLower().Contains(searchMin));

                if (listaClientesBuscados.Count() > 0)
                {
                    return Ok(listaClientesBuscados);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Error al obtener los datos --> {ex.Message}");
                return StatusCode(500, ModelState);
            }
           
        }

        /// <summary>
        /// Inserta un cliente en la BD.
        /// </summary>
        /// <response code="200">OK. Devuelve el cliente con el Id Correspondiente.</response>      
        /// <response code="400">Bad Request. El Modelo del cliente a insertar es incorrecto</response> 
        /// <response code="500">Internal Server Error. Error en el acceso a los datos.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Insert([FromBody]Cliente pCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clienteValidador = new ClienteValidador();

                var validacion = clienteValidador.Validate(pCliente);

                if (validacion.IsValid)
                {
                    var insertado = new ClienteNegocio().Insert(pCliente);

                    return insertado ? Ok(pCliente) :
                                    StatusCode(500, "Error al insertar el cliente");
                }

                var sb = new StringBuilder();

                validacion.Errors.ForEach(error => sb.Append($"{error}\n"));

                return BadRequest(sb.ToString());
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Error al insertar los datos --> {ex.Message}");
                return StatusCode(500, ModelState); 
            }
        }

        /// <summary>
        /// Actualiza un cliente en la BD.
        /// </summary>
        /// <response code="200">OK. Devuelve el cliente actualizado.</response>      
        /// <response code="400">Bad Request. El Modelo del cliente a actualizar es incorrecto</response> 
        /// <response code="500">Internal Server Error. Error en el acceso a los datos.</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        public IActionResult Update([FromBody]Cliente pCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var clienteValidador = new ClienteValidador();

                var validacion = clienteValidador.Validate(pCliente);

                if (validacion.IsValid)
                {
                    var clienteNegocio = new ClienteNegocio();

                    if(clienteNegocio.Existe(pCliente.Id))
                    {
                        var actualizado = new ClienteNegocio().Update(pCliente);
                        return actualizado ? Ok(pCliente) :
                                    StatusCode(500, "Error al actualizar el cliente");
                    }
                }

                var sb = new StringBuilder();

                validacion.Errors.ForEach(error => sb.Append($"{error}\n"));

                return BadRequest(sb.ToString());
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"Error al actualizar los datos --> {ex.Message}");
                return StatusCode(500, ModelState); 
            }
        }
        
    }
}
