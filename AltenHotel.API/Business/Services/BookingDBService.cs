using AltenHotel.API.Entities;
using AltenHotel.API.Infra.DAL;
using AltenHotel.API.Infra.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Business.Services
{
    public class BookingDBService
    {
        private DBCommunication _dbComm;
        public BookingDBService()
        {
            _dbComm = new DBCommunication();
        }

        public List<DateTime> GetAvailability()
        {
            DateTime initialDate = DateTime.Today.AddDays(-3);
            DateTime finalDate = initialDate.AddDays(33);
            var param = PrepParamGet(initialDate, finalDate);
            var reservations = _dbComm.ExecuteGet<ReservationDAO>("STP_ALT2022_GET_AVAILABILITY", param);

            List<DateTime> fullDateList = ExtractMiddleDates(DateTime.Today.AddDays(1), finalDate);
            fullDateList = RemoveBookedDays(fullDateList, reservations);

            return fullDateList;
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

        /// <summary>
        /// Removes already booked days from the 30 days range of possible reservations.
        /// </summary>
        /// <param name="fullDatesInterval"></param>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private List<DateTime> RemoveBookedDays(List<DateTime> fullDatesInterval, List<ReservationDAO> reservations)
        {
            List<DateTime> response = fullDatesInterval;

            for (int i = 0; i < reservations.Count; i++)
            {
                ReservationDAO current = reservations[i];
                if (current.ACTIVE)
                {
                    var currentInterval = ExtractMiddleDates(current.DT_START, current.DT_END);
                    response = response.Except(currentInterval).ToList();
                }
            }

            return response;
        }

        private List<DateTime> ExtractMiddleDates(DateTime initialDate, DateTime finalDate)
        {
            return Enumerable.Range(0, (finalDate - initialDate).Days + 1).Select(d => initialDate.AddDays(d)).ToList();
        }

        private List<Reservation> TransformReservation(List<ReservationDAO> daoList)
        {
            List<Reservation> response = new List<Reservation>();

            for (int i = 0; i < daoList.Count; i++)
            {
                ReservationDAO current = daoList[i];
                Reservation converted = new Reservation(current);
                response.Add(converted);
            }

            return response;
        }

        private Dictionary<string, dynamic> PrepParamGet(DateTime start, DateTime end)
        {
            return new Dictionary<string, dynamic>
            {
                { "Pinitial_date", start },
                { "Pfinal_date", end }
            };
        }
    }
}
