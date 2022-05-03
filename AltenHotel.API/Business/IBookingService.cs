using AltenHotel.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Business
{
    public interface IBookingService
    {
        public List<DateTime> GetAvailability();
        public dynamic PlaceReservation(Reservation obj);
        public dynamic UpdateReservation(Reservation obj);
        public dynamic CancelReservation(int id);
    }
}
