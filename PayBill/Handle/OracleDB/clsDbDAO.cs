using Oracle.ManagedDataAccess.Client;
using PayBill.Models;
using System.Data;
using System.Reflection;

namespace PayBill.Handle.OracleDB
{
    public class clsDbDAO
    {
        public string _strConn = "Data Source=localhost:1521/db11g;User ID=myschema;Password=1234;pooling=false;";

        public DataTable GetData_Dish(int p_typeCust)
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection connection = new OracleConnection(_strConn))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(Constants.spmyDishGet, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    var arrParam = new OracleParameter[2];
                    arrParam[0] = new OracleParameter("p_typeCust", OracleDbType.Int32) { Direction = ParameterDirection.Input, Value = p_typeCust };
                    arrParam[1] = new OracleParameter("rs", OracleDbType.RefCursor) { Direction = ParameterDirection.Output, Value = DBNull.Value };

                    foreach (OracleParameter item in arrParam)
                    {
                        command.Parameters.Add(item);
                    }
                    command.ExecuteNonQuery();

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    { adapter.Fill(dt); }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetData_Employee()
        {
            try
            {
                DataTable dt = new DataTable();
                using (OracleConnection connection = new OracleConnection(_strConn))
                {
                    connection.Open();

                    OracleCommand command = new OracleCommand(Constants.spmyEmployeeGet, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter arrParam = new OracleParameter("rs", OracleDbType.RefCursor) { Direction = ParameterDirection.Output, Value = DBNull.Value };

                    command.Parameters.Add(arrParam);
                    command.ExecuteNonQuery();

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    { adapter.Fill(dt); }
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int ReceiptDetail_i(List<Receipt_Details> list)
        {
            int totalReturn = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(_strConn))
                {
                    connection.Open();
                    foreach (var model in list)
                    {
                        try
                        {
                            OracleCommand command = new OracleCommand(Constants.spmyReceiptDetail_i, connection);
                            command.CommandType = CommandType.StoredProcedure;
                            var arrParam = new OracleParameter[6];
                            arrParam[0] = new OracleParameter("p_idReceiptDetail", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Receipt_Details };
                            arrParam[1] = new OracleParameter("p_idReceipt", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Receipt };
                            arrParam[2] = new OracleParameter("p_idDish", OracleDbType.Int32) { Direction = ParameterDirection.Input, Value = model.ID_Dish };
                            arrParam[3] = new OracleParameter("p_quantity", OracleDbType.Int32) { Direction = ParameterDirection.Input, Value = model.Quantity };
                            arrParam[4] = new OracleParameter("p_createdate", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.Create_Date };
                            arrParam[5] = new OracleParameter("p_result_out", OracleDbType.Int32) { Direction = ParameterDirection.Output, Value = DBNull.Value };

                            foreach (OracleParameter item in arrParam)
                            {
                                command.Parameters.Add(item);
                            }
                            command.ExecuteNonQuery();

                            totalReturn += int.Parse(arrParam[5].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    return totalReturn;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int Receipt_i(Receipt model)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_strConn))
                {
                    connection.Open();
                    OracleCommand command = new OracleCommand(Constants.spmyReceipt_i, connection);
                    command.CommandType = CommandType.StoredProcedure;
                    var arrParam = new OracleParameter[4];
                    arrParam[0] = new OracleParameter("p_idReceipt", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Receipt };
                    arrParam[1] = new OracleParameter("p_createdate", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.Create_Date };
                    arrParam[2] = new OracleParameter("p_totalPrice", OracleDbType.Decimal) { Direction = ParameterDirection.Input, Value = model.Total_Price };
                    arrParam[3] = new OracleParameter("p_result_out", OracleDbType.Int32) { Direction = ParameterDirection.Output, Value = DBNull.Value };

                    foreach (OracleParameter item in arrParam)
                    {
                        command.Parameters.Add(item);
                    }
                    command.ExecuteNonQuery();

                    return int.Parse(arrParam[3].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int ReceiptEmployee_i(List<Receipt_Employee> list)
        {
            int totalReturn = 0;
            try
            {
                using (OracleConnection connection = new OracleConnection(_strConn))
                {
                    connection.Open();
                    foreach (var model in list)
                    {
                        try
                        {
                            OracleCommand command = new OracleCommand(Constants.spmyReceiptEmployee_i, connection);
                            command.CommandType = CommandType.StoredProcedure;
                            var arrParam = new OracleParameter[7];
                            arrParam[0] = new OracleParameter("p_idReceiptDetail", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Receipt_Details };
                            arrParam[1] = new OracleParameter("p_idReceipt", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Receipt };
                            arrParam[2] = new OracleParameter("p_idDish", OracleDbType.Int32) { Direction = ParameterDirection.Input, Value = model.ID_Dish };
                            arrParam[3] = new OracleParameter("p_idEmployee", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.ID_Employee };
                            arrParam[4] = new OracleParameter("p_quantity", OracleDbType.Int32) { Direction = ParameterDirection.Input, Value = model.Quantity };
                            arrParam[5] = new OracleParameter("p_createdate", OracleDbType.Varchar2) { Direction = ParameterDirection.Input, Value = model.Create_Date };
                            arrParam[6] = new OracleParameter("p_result_out", OracleDbType.Int32) { Direction = ParameterDirection.Output, Value = DBNull.Value };

                            foreach (OracleParameter item in arrParam)
                            {
                                command.Parameters.Add(item);
                            }
                            command.ExecuteNonQuery();

                            totalReturn += int.Parse(arrParam[6].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    return totalReturn;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}