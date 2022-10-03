using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.BD_KevinManuelGarciaVegaContext context=new DL.BD_KevinManuelGarciaVegaContext())
                {
                    var query = context.Productos.FromSqlRaw($"productoGetAll").ToList();

                    result.Objects = new List<object>();

                    if(query != null)
                    {

                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.Precio =(double) obj.Precio;
                            producto.fabricante = new ML.Fabricante();
                            producto.fabricante.IdFabricante = obj.IdFabricante.Value;

                            result.Objects.Add(producto);

                        }
                        result.correct = true;

                    }


                }
            }catch(Exception ex)
            {
                result.correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }


        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.BD_KevinManuelGarciaVegaContext context=new DL.BD_KevinManuelGarciaVegaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"productoAdd '{producto.Nombre}',{producto.Precio},{producto.fabricante.IdFabricante}");

                    if(query>0)
                    {
                        result.correct = true;

                    }
                    else
                    {
                        result.correct = false;
                    }
                }
            }catch(Exception ex)
            {
                result.correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}