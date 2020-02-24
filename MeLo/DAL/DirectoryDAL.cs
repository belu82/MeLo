using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MeLo.Domain;
using MeLo.DAL;

namespace MeLo.Core
{
    public class DirectoryDAL : BaseDal
    {
        public int Insert(Directory directory)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@Name", directory.name, DbType.String));
            parameters.Add(sqlHelper.CreateParameter("@Path", directory.path, DbType.String));

            int lastId = 0;
            sqlHelper.Insert("DAH_Directory_Insert", CommandType.StoredProcedure, parameters.ToArray(), out lastId);
            return lastId;
        }
        public IEnumerable<Directory> GetAll()
        {
            var paramters = new List<SqlParameter>();
            var dataReader = sqlHelper.GetDataReader("DAH_Directory_GetAll", CommandType.StoredProcedure, null, out connection);

            try
            {
                var directories = new List<Directory>();
                while (dataReader.Read())
                {
                    var dir = new Directory();
                    dir.name = dataReader["Name"].ToString();
                    dir.path = dataReader["Path"].ToString();
                    dir.id = (int)(dataReader["id"]);

                    directories.Add(dir);

                }
                return directories;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }

        }

        public void Delete(int id)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(sqlHelper.CreateParameter("@id", id, DbType.Int32));
            sqlHelper.Delete("DAH_Directory_Delete", CommandType.StoredProcedure, parameters.ToArray());
        }
    }
}
