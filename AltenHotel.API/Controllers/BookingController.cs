using AltenHotel.API.Business.Services;
using AltenHotel.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public ActionResult<IEnumerable<DateTime>> GetAllAvailability()
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
        [HttpGet("{date}")]
        public string GetSingleDate(DateTime date)
        {
            return "value";
        }

        // POST api/<BookingController>
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

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void EditReservation(int id, [FromBody] string value)
        {
            // before calling the update, needs to check the availability of the new date
        }

        // DELETE api/<BookingController>/5
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
