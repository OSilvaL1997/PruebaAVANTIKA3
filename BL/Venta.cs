using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Venta
    {
        public static ML.Result AddVenta(ML.Venta venta)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "AddVenta";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[3];

                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = venta.Evento.IdEvento;
                        collection[1] = new SqlParameter("IdLugar", SqlDbType.Int);
                        collection[1].Value = venta.Lugar.idLugar;
                        collection[2] = new SqlParameter("NumeroAsiento", SqlDbType.Int);
                        collection[2].Value = venta.NumeroAsiento;
                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al insertar.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAllLugar()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllLugar";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableLugar = new DataTable();

                            da.Fill(tableLugar);

                            cmd.Connection.Open();

                            if (tableLugar.Rows.Count > 0)
                            {

                                result.Objects = new List<object>();

                                foreach (DataRow row in tableLugar.Rows)
                                {
                                    ML.Lugar lugar = new ML.Lugar();
                                    lugar.idLugar = int.Parse(row[0].ToString());
                                    lugar.Nombre = row[1].ToString();
                                    lugar.Calle = row[2].ToString();
                                    lugar.Numero = row[3].ToString();
                                    lugar.Localidad = row[4].ToString();
                                    lugar.Capacidad = int.Parse(row[5].ToString());

                                    result.Objects.Add(lugar);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ocurrió un error al momento de obtener los registros";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetLugarDisponible(int idEvento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetLugarOcupado";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = idEvento;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablelugar = new DataTable();

                            da.Fill(tablelugar);

                            cmd.Connection.Open();


                            if (tablelugar.Rows.Count > 0)
                            {

                                result.Objects = new List<object>();
                                int cuenta = 1;
                                int capacidadTotal = 0;
                                foreach (DataRow row in tablelugar.Rows)
                                {
                                    ML.Venta ventaGet = new ML.Venta();
                                    ventaGet.NumeroAsiento = int.Parse(row[0].ToString());
                                    ventaGet.Lugar = new ML.Lugar();
                                    ventaGet.Lugar.Capacidad = int.Parse(row[1].ToString());
                                    capacidadTotal = ventaGet.Lugar.Capacidad;
                                    for (int i = cuenta; cuenta < ventaGet.NumeroAsiento; i++)
                                    {

                                        if (cuenta != ventaGet.NumeroAsiento)
                                        {
                                            result.Objects.Add(i);
                                        }
                                        cuenta++;
                                    }
                                    cuenta = cuenta + 1;
                                }
                                for (int i = cuenta; i <= capacidadTotal; i++)
                                {
                                    result.Objects.Add(i);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ocurrió un error al momento de obtener un registro";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetLugarVendido(int idEvento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetLugarOcupado";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = idEvento;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablelugar = new DataTable();

                            da.Fill(tablelugar);

                            cmd.Connection.Open();


                            if (tablelugar.Rows.Count > 0)
                            {

                                result.Objects = new List<object>();
                                foreach (DataRow row in tablelugar.Rows)
                                {
                                    ML.Venta ventaGet = new ML.Venta();
                                    ventaGet.NumeroAsiento = int.Parse(row[0].ToString());
                                    ventaGet.Lugar = new ML.Lugar();
                                    ventaGet.Lugar.Capacidad = int.Parse(row[1].ToString());
                                    result.Objects.Add(ventaGet);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ocurrió un error al momento de obtener un registro";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result TotalVentas()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "CantidadEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableVentas = new DataTable();

                            da.Fill(tableVentas);

                            cmd.Connection.Open();


                            if (tableVentas.Rows.Count > 0)
                            {
                                decimal suma = 0;
                                result.Objects = new List<object>();
                                foreach (DataRow row in tableVentas.Rows)
                                {
                                    ML.Venta ventaGet = new ML.Venta();
                                    ventaGet.Evento = new ML.Evento();
                                    ventaGet.Evento.IdEvento = int.Parse(row[0].ToString());
                                    ventaGet.Evento.Nombre = row[1].ToString();
                                    ventaGet.NumeroAsiento = int.Parse(row[2].ToString());
                                    ventaGet.Evento.Precio = decimal.Parse(row[3].ToString());
                                    ventaGet.Total = decimal.Parse(row[4].ToString());
                                    suma = suma + ventaGet.Total;
                                    ventaGet.TotalSuma = suma;
                                    result.Objects.Add(ventaGet);
                                }

                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ocurrió un error al momento de obtener un registro";
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
