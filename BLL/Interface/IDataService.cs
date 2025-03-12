using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDataService 
    {
        public Task<DataLoadDTO> LoadData(string username);
        public Task SaveData(DataSaveDTO data, string username);
    }
}
