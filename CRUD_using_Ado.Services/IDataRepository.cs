using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD_using_Ado.Models;

namespace CRUD_using_Ado.Services
{
    public interface IDataRepository
    {
        public Responce GetDataUsingDirectQuery(string sqlQuery);

        public Responce GetDataWithoutParaValues(string strProcudure);

        public Responce GetDataWithParaMetersInHeader(string strProcedure, string strParaNames, string strParaValues);

        public Responce GetDataWithParaMetersInBody(ProData proData);

        public Responce InsertDatawithHeader(string strProcedure, string strParaNames, string strParaValues);

        public Responce InsertDatawithBody(ProData proData);
    }
}
