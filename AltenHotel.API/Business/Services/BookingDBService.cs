using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Business.Services
{
    public class BookingDBService
    {
        public BookingDBService()
        {

        }

        public dynamic GetAvailability()
        {
            // TODO get availability of the room starting from today and ending in 30 days
            // esse para passar na proc, tenho que colocar 3 dias a menos que hoje mas ele só vai poder reservar a partir de hoje, usa os tres
            // dias para tras para pegar uma reserva que possa envolver hoje
            DateTime initialDate = DateTime.Now.AddDays(-3);
            DateTime finalDate = initialDate.AddDays(30);
            return null;
        }

        public dynamic PlaceReservation()
        {
            // TODO insert a reservation 
            return null;
        }

        // Change the date of the reservation
        public dynamic UpdateReservation(int id)
        {
            // TODO update a reservation
            // stay cannot be longer than 3 days
            return null;
        }

        public dynamic CancelReservation(int id)
        {
            // TODO remove line of this id reservation
            return null;
        }
    }
}
