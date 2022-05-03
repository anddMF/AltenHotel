using AltenHotel.API.Business;
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
        private IBookingService _bookingSvc;
        public BookingController(IBookingService bookingSvc)
        {
            _bookingSvc = bookingSvc;
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
                List<DateTime> response = _bookingSvc.GetAvailability();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<BookingController>
        /// <summary>
        /// Places the reservation for the room.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PlaceReservation([FromBody] Reservation obj)
        {
            try
            {
                dynamic response = _bookingSvc.PlaceReservation(obj);

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
                if (obj.Id < 0)
                    return BadRequest("ID cannot be a negative number");

                dynamic response = _bookingSvc.UpdateReservation(obj);

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
                if (id < 0)
                    return BadRequest("ID cannot be a negative number");

                dynamic response = _bookingSvc.CancelReservation(id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
