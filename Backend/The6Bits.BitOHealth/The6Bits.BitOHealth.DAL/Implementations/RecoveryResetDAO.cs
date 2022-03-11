using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.DAL.Contract;

namespace The6Bits.BitOHealth.DAL.Implementations
{
    public class RecoveryResetDAO : IRecoveryResetDatabase
    {
        private string _connect;

        public RecoveryResetDAO(string con)
        {
            _connect = con;
        }
       
        public bool Delete()
        {
            try
            {
                string query = "delete from Recovery;";
                using (SqlConnection conn = new SqlConnection(_connect))
                {
                    conn.Open();
                    int reset = conn.Execute(query);
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
    }
}
