using AltenHotel.API.Business;
using AltenHotel.API.Controllers;
using AltenHotel.API.Entities;
using AltenHotel.API.Infra.DAL;
using AltenHotel.API.Infra.DAO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace AltenHotel.API.Test
{
    public class BookingDBServiceTest
    {
        [Fact]
        public void GetAvailability_OK()
        {
            Moq.Mock<IDBCommunication> mock = new Mock<IDBCommunication>();
            mock.Setup(x => x.ExecuteGet<ReservationDAO>(It.IsAny<string>(), It.IsAny<Dictionary<string, dynamic>>())).Returns(SetupListReservationDAO());


        }

        private List<ReservationDAO> SetupListReservationDAO()
        {
            List<ReservationDAO> response = new List<ReservationDAO>();

            int counter = 1;

            while(counter <= 5)
            {
                ReservationDAO obj = new ReservationDAO
                {
                    ID = counter,
                    NAME_CLIENT = "Test " + counter,
                    ACTIVE = true,
                    DT_RESERVATION = DateTime.Now,
                    DT_START = DateTime.Now.AddDays(counter)
                };
                obj.DT_END = obj.DT_START.AddDays(7);

                response.Add(obj);

                counter++;
            }

            return response;
        } 
    }
}
