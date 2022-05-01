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

        /// <summary>
        /// Gets the available dates for the room to be booked, in a range of 30 days starting from today + 1.
        /// </summary>
        /// <returns>list of DateTime</returns>
        public List<DateTime> GetAvailability()
        {
            // initial date has the -3 because if the search start with the current date, a stay that initiates before today and ends after will not get capture
            DateTime initialDate = DateTime.Today.AddDays(-3);
            DateTime finalDate = initialDate.AddDays(33);

            var reservations = GetReservations(initialDate, finalDate);

            List<DateTime> fullDateList = ExtractMiddleDates(DateTime.Today.AddDays(1), finalDate);
            fullDateList = RemoveBookedDays(fullDateList, reservations);

            return fullDateList;
        }

        /// <summary>
        /// After validations, places the reservertation for the room.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public dynamic PlaceReservation(Reservation obj)
        {
            List<DateTime> stay = ExtractMiddleDates(obj.StartDate, obj.EndDate);
            if (stay.Count > 3)
                throw new Exception("The maximum number of days from a stay are 3");

            // check if dates are available and inside the maximum of 30 days
            var reservations = GetReservations(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(33));
            var alreadyBooked = CheckAvailability(stay, reservations);
            if (alreadyBooked.Count > 0)
                throw new Exception("Some or all days are not available: " + NotAvailableDatesToString(alreadyBooked));

            Dictionary<string, dynamic> param = PrepParamInsert(obj);
            var response = _dbComm.ExecuteOperation("STP_ALT2022_INSERT_BOOKING", param);

            return null;
        }

        /// <summary>
        /// After validations, modifies the reservation.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public dynamic UpdateReservation(Reservation obj)
        {
            List<DateTime> stay = ExtractMiddleDates(obj.StartDate, obj.EndDate);
            if (stay.Count > 3)
                throw new Exception("The maximum number of days from a stay are 3");

            // checks if dates are available and inside the maximum of 30 days, removing the booking that it is being updated
            var reservations = GetReservations(DateTime.Today.AddDays(-3), DateTime.Today.AddDays(33));
            reservations.RemoveAll(x => x.Id == obj.Id);
            var alreadyBooked = CheckAvailability(stay, reservations);
            if (alreadyBooked.Count > 0)
                throw new Exception("Some or all days are not available: " + NotAvailableDatesToString(alreadyBooked));

            Dictionary<string, dynamic> param = PrepParamUpdate(obj);
            var response = _dbComm.ExecuteOperation("STP_ALT2022_UPDATE_BOOKING", param);

            return null;
        }

        /// <summary>
        /// Cancels a reservation, setting the ACTIVE property to false.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic CancelReservation(int id)
        {
            Dictionary<string, dynamic> param = PrepParamCancel(id);
            var response = _dbComm.ExecuteOperation("STP_ALT2022_DELETE_BOOKING", param);
            return response;
        }

        /// <summary>
        /// Checks if the interval provided is available in the list of reservations.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="reservations"></param>
        /// <returns>list with the not available dates in the interval provided</returns>
        private List<DateTime> CheckAvailability(List<DateTime> interval, List<Reservation> reservations)
        {
            //var availableDays = GetAvailability();
            List<DateTime> full30Days = ExtractMiddleDates(DateTime.Today.AddDays(1), DateTime.Today.AddDays(31));
            List<DateTime> availableDays = RemoveBookedDays(full30Days, reservations);

            List<DateTime> response = new List<DateTime>();

            // loop for the return of the specific days that are already booked
            var notAvailable = from inter in interval
                    where !availableDays.Any(x => x == inter)
                    select inter;

            return notAvailable.ToList();
        }

        /// <summary>
        /// Gets the reservations on the booking.
        /// </summary>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns></returns>
        private List<Reservation> GetReservations(DateTime initialDate, DateTime finalDate)
        {
            Dictionary<string, dynamic> param = PrepParamGet(initialDate, finalDate);
            List<ReservationDAO> reservations = _dbComm.ExecuteGet<ReservationDAO>("STP_ALT2022_GET_AVAILABILITY", param);

            List<Reservation> response = TransformFromDAO(reservations);

            return response;
        }

        /// <summary>
        /// Removes already booked days from the 30 days range of possible reservations.
        /// </summary>
        /// <param name="fullDatesInterval"></param>
        /// <param name="reservations"></param>
        /// <returns></returns>
        private List<DateTime> RemoveBookedDays(List<DateTime> fullDatesInterval, List<Reservation> reservations)
        {
            List<DateTime> response = fullDatesInterval;

            for (int i = 0; i < reservations.Count; i++)
            {
                Reservation current = reservations[i];
                if (current.Active)
                {
                    var currentInterval = ExtractMiddleDates(current.StartDate, current.EndDate);
                    response = response.Except(currentInterval).ToList();
                }
            }

            return response;
        }

        /// <summary>
        /// Extracts and return the dates between two dates.
        /// </summary>
        /// <param name="initialDate"></param>
        /// <param name="finalDate"></param>
        /// <returns>list of DateTime</returns>
        private List<DateTime> ExtractMiddleDates(DateTime initialDate, DateTime finalDate)
        {
            return Enumerable.Range(0, (finalDate - initialDate).Days + 1).Select(d => initialDate.AddDays(d)).ToList();
        }

        /// <summary>
        /// Transforms the not available dates in string.
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        private string NotAvailableDatesToString(List<DateTime> dates)
        {
            string response = "";

            for(int i = 0; i < dates.Count; i++)
                response = response + dates[i].Date.ToShortDateString() + "; ";

            return response;
        }

        /// <summary>
        /// Changes the DAO object to the one with the business handles.
        /// </summary>
        /// <param name="daoList"></param>
        /// <returns></returns>
        private List<Reservation> TransformFromDAO(List<ReservationDAO> daoList)
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

        #region Preparation methods for the parameters on stored procedures

        private Dictionary<string, dynamic> PrepParamGet(DateTime start, DateTime end)
        {
            return new Dictionary<string, dynamic>
            {
                { "Pinitial_date", start },
                { "Pfinal_date", end }
            };
        }

        private Dictionary<string, dynamic> PrepParamInsert(Reservation obj)
        {
            return new Dictionary<string, dynamic>
            {
                { "Pname_client", obj.ClientName },
                { "Pstart_date", obj.StartDate },
                { "Pend_date", obj.EndDate },
                { "Preservation_date", DateTime.Today },
                { "Pactive", obj.Active }
            };
        }

        private Dictionary<string, dynamic> PrepParamUpdate(Reservation obj)
        {
            return new Dictionary<string, dynamic>
            {
                { "Pid", obj.Id },
                { "Pname_client", obj.ClientName },
                { "Pstart_date", obj.StartDate },
                { "Pend_date", obj.EndDate },
                { "Preservation_date", DateTime.Today },
                { "Pactive", obj.Active }
            };
        }

        private Dictionary<string, dynamic> PrepParamCancel(int id)
        {
            return new Dictionary<string, dynamic>
            {
                { "Pid", id }
            };
        }

        #endregion
    }
}
