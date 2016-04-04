using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPark.Models
{
    public class Ticket

    {
      
        public DateTime DateIn { get; set; }
        public DateTime ? DateOut  { get; set; }
        public decimal ? ParkingFee
        {
            get
            {
                if (DateOut == null)
                    return null;

                if (DateOut < DateIn)
                    throw new Exception("Invalid date");

                TimeSpan ans = DateOut.Value - DateIn;

                if (ans.TotalSeconds <= (15 * 60))
                    return 0;
                else if (ans.TotalMinutes <= (3 * 60) + 15)
                    return 50;
                else
                {
                    ans = ans - new TimeSpan(3, 0, 0);
                    Int32 fee = 30;
                    if (ans.Minutes > 15  )
                        return (Convert.ToInt32(ans.TotalHours) * fee) + 50;
                    else
                        return ((Convert.ToInt32(ans.TotalHours) - 1) * fee) + 50;
                }
            }
        }
        public string PlateNo
        {
            get; set;


        }
    }
}
