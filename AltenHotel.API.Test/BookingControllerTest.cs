using AltenHotel.API.Business;
using AltenHotel.API.Controllers;
using AltenHotel.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AltenHotel.API.Test
{
    public class BookingControllerTest
    {
        [Fact]
        public void GetAvailability_OK()
        {
            List<DateTime> response = new List<DateTime> { DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(7) };
            Moq.Mock<IBookingService> mock = new Moq.Mock<IBookingService>();
            mock.Setup(x => x.GetAvailability()).Returns(response);

            BookingController controller = new BookingController(mock.Object);

            //Act
            var act = controller.GetAvailability();
            var res = act.Result as OkObjectResult;

            //Assert
            Assert.Equal(response, res.Value);
        }

        [Fact]
        public void GetAvailability_NOK()
        {
            Moq.Mock<IBookingService> mock = new Mock<IBookingService>();
            mock.Setup(x => x.GetAvailability()).Throws(new Exception("Error"));

            BookingController controller = new BookingController(mock.Object);

            //Act
            var act = controller.GetAvailability();
            var res = act.Result as ObjectResult;

            //Assert
            Assert.Equal(500, res.StatusCode);
            Assert.Equal("Error", res.Value);
        }

        [Fact]
        public void PlaceReservation_OK()
        {
            Reservation obj = new Reservation { Active = true, ClientName = "Test", ReservationDate = DateTime.Now, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) };
            Moq.Mock<IBookingService> mock = new Moq.Mock<IBookingService>();
            mock.Setup(x => x.PlaceReservation(It.IsAny<Reservation>())).Returns(null);

            BookingController controller = new BookingController(mock.Object);

            //Act
            var act = controller.PlaceReservation(obj) as OkObjectResult;

            //Assert
            Assert.NotNull(act);
            Assert.Equal(200, act.StatusCode);
        }
    }
}
