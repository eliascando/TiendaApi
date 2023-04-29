using Microsoft.AspNetCore.Mvc;
using TiendaApi.Data;
using TiendaApi.Models;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController :ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductosModel>>> Get()
        {
            var funcion = new ProductosData();
            var lista = await funcion.MostrarProductos();
            return Ok(lista);
        }
        [HttpPost]
        public async Task Post([FromBody] ProductosModel productos)
        {
            var funcion = new ProductosData();
            await funcion.InsertarProductos(productos);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]ProductosModel productos)
        {
            var funcion = new ProductosData();
            productos.Id = id;
            await funcion.EditarProductos(productos);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var funcion = new ProductosData();
            var productos = new ProductosModel();
            productos.Id = id;
            await funcion.EliminarProductos(productos);
            return NoContent();
        }
    }
}
