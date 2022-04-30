using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Infra.DAO
{
    public class ReservationDAO
    {
        public int ID { get; set; }
        public string NAME_CLIENT { get; set; }
        public DateTime DT_START { get; set; }
        public DateTime DT_END { get; set; }
        public DateTime DT_RESERVATION { get; set; }
        public bool ACTIVE { get; set; }
    }
}
