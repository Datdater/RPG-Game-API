using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DataLoadDTO
    {
        public string ScreenName { get; set; }
        public int Gold { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public Dictionary<string, int> Inventory { get; set; }
        public List<string> EquipmentId { get; set; }
        public string CloseCheckpointId { get; set; }
        public Dictionary<string, bool> CheckPoints { get; set; }

    }

    
}
