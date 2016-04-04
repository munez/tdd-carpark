using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace CarPark.Facts
{
    public class TicketFacts
    {

        public class General
        {

            [Fact]
            public void BasicUsage()
            {
                Ticket t = new Ticket();
                t.PlateNo = "1707";
                t.DateIn = new DateTime(2016, 1, 1, 9, 0, 0);
                t.DateOut = DateTime.Parse("13:30");

             

                Assert.Equal("1707", t.PlateNo);
                Assert.Equal(9, t.DateIn.Hour);
                Assert.Equal(13, t.DateOut.Value.Hour);
            }   

            [Fact]
            public void NewTicket_HasNoDateOut()
            {

                var t = new Ticket();

                Assert.Null(t.DateOut);

            }
        }

        public class ParkingFeeProperty
        {
            [Fact]
            public void First15Minutes_Free() {

                //arrange
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15");

                // act
                decimal fee = t.ParkingFee.Value;

                // assert
                Assert.Equal(0m, fee);

                
            }

            [Fact]
            public void WithInFirst3Hours_50Baht()
            {
                //arrange
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("9:15:01");

                // act
                decimal fee = t.ParkingFee.Value;

                // assert
                Assert.Equal(50m, fee);
            }

            [Fact]
            public void WithInFirst3HoursII_50Baht()
            {
                //arrange
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("12:11:01");

                // act
                decimal fee = t.ParkingFee.Value;

                // assert
                Assert.Equal(50m, fee);
            }

            [Fact]
            public void WithInFirst4Hours_50Baht()
            {
                //arrange
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("15:11");

                // act
                decimal fee = t.ParkingFee.Value;

                // assert
                Assert.Equal(110m, fee);
            }

            [Fact]
            public void NewTicket_DontKnowParkingFee()
            {
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = null;
                // act


                // assert
                Assert.Null(t.ParkingFee);

            }


            [Fact]
            public void DateOutIsBeforeDateIN_ThrowsException()
            {
                var t = new Ticket();
                t.PlateNo = "155";
                t.DateIn = DateTime.Parse("9:00");
                t.DateOut = DateTime.Parse("8:00"); ;
                // act


                // assert
                var ex = Assert.Throws<Exception>(() =>
               {
                   var fee = t.ParkingFee;
               });

                Assert.Equal("Invalid date", ex.Message);
            }


        }
    }
}