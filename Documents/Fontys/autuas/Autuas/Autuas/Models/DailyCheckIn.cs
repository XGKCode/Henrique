using Autuas.Enums;
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Models
{
    public class DailyCheckIn
    {

        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public PhysicalState? PhysicalState { get; set; }

        public MentalState? MentalState { get; set; }

        public PositiveFeelings? PositiveFeelings { get; set; }

        public NegativeFeelings? NegativeFeelings { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

    }
}
