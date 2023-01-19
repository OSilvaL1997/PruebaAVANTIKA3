using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ML;

namespace BL
{
    public class Lugar
    {
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
        public static ML.Result GetByIdLugar(ML.Lugar lugar)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "GetByIdLugar";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];
                        collection[0] = new SqlParameter("IdLugar", SqlDbType.Int);
                        collection[0].Value = lugar.idLugar;

                        cmd.Parameters.AddRange(collection);
                        
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablelugar = new DataTable();

                            da.Fill(tablelugar);

                            cmd.Connection.Open();

                            if (tablelugar.Rows.Count > 0)
                            {
                                DataRow row = tablelugar.Rows[0];

                                ML.Lugar luga = new ML.Lugar();
                                luga.idLugar = int.Parse(row[0].ToString());
                                luga.Nombre = row[1].ToString();
                                luga.Calle = row[2].ToString();
                                luga.Numero = row[3].ToString();
                                luga.Localidad = row[4].ToString();
                                luga.Capacidad = int.Parse(row[5].ToString());

                                result.Object = luga;
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
