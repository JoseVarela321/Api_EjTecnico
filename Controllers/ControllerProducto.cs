using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaApi.Models;
using Microsoft.AspNetCore.Cors;
namespace PruebaTecnicaApi.Controllers
{
    [EnableCors("ReglasCors")]
    public class ControllerProducto : Controller
    {

        public readonly MercadoContext _dbcontext;


        public ControllerProducto(MercadoContext _context)
        {
            _dbcontext = _context;
        }


        [HttpGet]
        [Route("Inventario")]

        public IActionResult Inventario()
        {
            List<Producto> list = new List<Producto>();

            try
            {
                list = _dbcontext.Productos.Include(c => c.ObjCategoria).ToList();

                return StatusCode(StatusCodes.Status200OK, new { response = list });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { mensaje = "ok", response = list });
            }
        }


        [HttpGet]
        [Route("BuscarProducto/{idProducto:int}")]

        public IActionResult BuscarProducto(int idProducto)
        {
            Producto produc = _dbcontext.Productos.Find(idProducto);

            if (produc == null)
            {
                return BadRequest("EL Producto no se encontro");
            }

            try
            {
                produc = _dbcontext.Productos.Include(c => c.ObjCategoria).Where(p => p.IdProducto == idProducto).FirstOrDefault();

                return StatusCode(StatusCodes.Status200OK, new { reponse = produc });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { mensaje = "ok", reponse = produc });
            }
        }

        [HttpPost]
        [Route("GuardarProducto")]

        public IActionResult GuardarProducto([FromBody] Producto product)
        {
            try
            {
                _dbcontext.Productos.Add(product);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ElProducto se guardo de manera correcta!." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { mensaje = ex.Message });
            }
        }

        [HttpPut]
        [Route("EditarProducto")]

        public IActionResult EditarProducto([FromBody] Producto producto)
        {
            Producto produc = _dbcontext.Productos.Find(producto.IdProducto);

            if (produc == null)
            {
                return BadRequest("EL Producto no se encontro");
            }

            try
            {

                produc.CodigoBarra = producto.CodigoBarra is null ? produc.CodigoBarra : producto.CodigoBarra;
                produc.Marca = producto.Marca is null ? produc.Marca : producto.Marca;
                produc.Descripcion = producto.Descripcion is null ? produc.Descripcion : producto.Descripcion;
                produc.IdCategoria = producto.IdCategoria is null ? produc.IdCategoria : producto.IdCategoria;
                produc.Precio = producto.Precio is null ? produc.Precio : producto.Precio;


                _dbcontext.Productos.Update(produc);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "El Producto se actualizo de manera correcta!." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { mensaje = ex.Message });
            }
        }


        [HttpDelete]
        [Route("EliminarProducto/{idProducto:int}")]

        public IActionResult Eliminar(int idProducto)
        {


            Producto produc = _dbcontext.Productos.Find(idProducto);

            if (produc == null)
            {
                return BadRequest("EL Producto no se encontro");
            }

            try
            {
                _dbcontext.Productos.Remove(produc);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "El Producto se elimino de manera correcta!." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status204NoContent, new { mensaje = ex.Message });
            }

        }


    }
}
