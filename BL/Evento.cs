using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Evento
    {
        public static ML.Result AddEvento(ML.Evento evento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "addEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = evento.Nombre;
                        collection[1] = new SqlParameter("IdLugar", SqlDbType.Int);
                        collection[1].Value = evento.Lugar.idLugar;
                        collection[2] = new SqlParameter("FechaHora", SqlDbType.DateTime);
                        collection[2].Value = evento.FechaHora;
                        collection[3] = new SqlParameter("Precio", SqlDbType.Decimal);
                        collection[3].Value = evento.Precio;
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
                            result.ErrorMessage = "Ocurrió un error al insertar un error.";
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
        public static ML.Result UpdateEvento(ML.Evento evento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "UpdateEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = evento.IdEvento;
                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = evento.Nombre;
                        collection[2] = new SqlParameter("IdLugar", SqlDbType.Int);
                        collection[2].Value = evento.Lugar.idLugar;
                        collection[3] = new SqlParameter("FechaHora", SqlDbType.DateTime);
                        collection[3].Value = evento.FechaHora;
                        collection[4] = new SqlParameter("Precio", SqlDbType.Decimal);
                        collection[4].Value = evento.Precio;
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
                            result.ErrorMessage = "Ocurrió un error al actualizar el evento ";
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
        public static ML.Result GetByIdEvento(ML.Evento evento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetByIdEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = evento.IdEvento;

                        cmd.Parameters.AddRange(collection);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableEvento = new DataTable();

                            da.Fill(tableEvento);

                            cmd.Connection.Open();

                            if (tableEvento.Rows.Count > 0)
                            {
                                DataRow row = tableEvento.Rows[0];

                                ML.Evento eventodata = new ML.Evento();
                                evento.IdEvento = int.Parse(row[0].ToString());
                                evento.Nombre = row[1].ToString();
                                evento.Lugar.idLugar = int.Parse(row[2].ToString());
                                evento.Lugar.Nombre = row[3].ToString();
                                evento.FechaHora = (DateTime)row[4];
                                evento.Precio = decimal.Parse(row[5].ToString());

                                result.Object = evento;
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
        public static ML.Result GetAllEvento()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetAllEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableEvento = new DataTable();

                            da.Fill(tableEvento);

                            cmd.Connection.Open();

                            if (tableEvento.Rows.Count > 0)
                            {

                                result.Objects = new List<object>();

                                foreach (DataRow row in tableEvento.Rows)
                                {
                                    ML.Evento evento = new ML.Evento();
                                    evento.IdEvento = int.Parse(row[0].ToString());
                                    evento.Nombre = row[1].ToString();
                                    evento.Lugar = new ML.Lugar(); 
                                    evento.Lugar.idLugar = int.Parse(row[2].ToString());
                                    evento.Lugar.Nombre = row[3].ToString();
                                    evento.FechaHora = (DateTime)row[4];
                                    evento.Precio = decimal.Parse(row[5].ToString());

                                    result.Objects.Add(evento);
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
        public static ML.Result DeleteEvento(ML.Evento evento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "DeleteEvento";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdEvento", SqlDbType.Int);
                        collection[0].Value = evento.IdEvento;

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
                            result.ErrorMessage = "Ocurrió un error al eliminar el evento ";
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
