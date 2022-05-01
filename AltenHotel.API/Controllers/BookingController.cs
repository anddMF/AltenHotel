using AltenHotel.API.Business.Services;
using AltenHotel.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenHotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private BookingDBService _bookingSvc;
        public BookingController()
        {
            _bookingSvc = new BookingDBService();
        }

        // GET: api/<BookingController>
        /// <summary>
        /// Returns the available dates for the room to be booked.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<DateTime>> GetAvailability()
        {
            try
            {
                var response = _bookingSvc.GetAvailability();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public string GetSingleReservation(int id)
        {
            return "value";
        }

        // POST api/<BookingController>
        /// <summary>
        /// Places the reservertation for the room.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PlaceReservation([FromBody] Reservation obj)
        {
            try
            {
                var response = _bookingSvc.PlaceReservation(obj);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<BookingController>
        /// <summary>
        /// Modifies the reservation.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult EditReservation([FromBody] Reservation obj)
        {
            try
            {
                var response = _bookingSvc.UpdateReservation(obj);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<BookingController>/5
        /// <summary>
        /// Cancels a reservation.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult CancelReservation(int id)
        {
            try
            {
                var response = _bookingSvc.CancelReservation(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
