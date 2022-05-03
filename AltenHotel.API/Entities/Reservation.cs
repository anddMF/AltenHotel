using AltenHotel.API.Infra.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Entities
{
    /// <summary>
    /// Reservation class is an anemic domain because of the simplicity of the code, all the changes in Reservation on the current scope of the project are 
    /// made on the DB e it is not used con the scope of execution.
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool Active { get; set; }

        public Reservation()
        {

        }

        public Reservation(ReservationDAO dao)
        {
            if (dao.ID < 0)
                throw new ArgumentException("Reservation ID cannot be negative");

            Id = dao.ID;
            ClientName = dao.NAME_CLIENT;
            StartDate = dao.DT_START;
            EndDate = dao.DT_END;
            ReservationDate = dao.DT_RESERVATION;
            Active = dao.ACTIVE;
        }
    }
}
