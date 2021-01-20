using Autuas.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Dtos
{
    public class DailyCheckInCreateDto
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public PhysicalState? PhysicalState { get; set; }

        public MentalState? MentalState { get; set; }

        public PositiveFeelings? PositiveFeelings { get; set; }

        public NegativeFeelings? NegativeFeelings { get; set; }

        //to know which user submitted the daily check-In
        public int UserID { get; set; }

    }
}
