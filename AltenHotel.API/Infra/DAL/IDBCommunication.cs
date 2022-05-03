using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Infra.DAL
{
    public interface IDBCommunication
    {
        public dynamic ExecuteOperation(string name, Dictionary<string, object> param);
        public List<T> ExecuteGet<T>(string name, Dictionary<string, dynamic> param);

    }
}
