using System.Data.SqlClient;
using System.Data;
using TiendaApi.Conexion;
using TiendaApi.Models;

namespace TiendaApi.Data
{
    public class ProductosData
    {
        ConexionDB cn = new ConexionDB();
        public async Task<List<ProductosModel>> MostrarProductos()
        {
            var lista = new List<ProductosModel>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var productosmodel = new ProductosModel();
                            productosmodel.Id = (int)item["id"];
                            productosmodel.Descripcion = (string)item["descripcion"];
                            productosmodel.Precio = (decimal)item["precio"];
                            lista.Add(productosmodel);
                        }
                    }
                }
            }
            return lista;
        }
        public async Task InsertarProductos(ProductosModel productos)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", productos.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", productos.Precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EditarProductos(ProductosModel productos)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", productos.Id);
                    cmd.Parameters.AddWithValue("@descripcion", productos.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", productos.Precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task EliminarProductos(ProductosModel productos)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", productos.Id);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
