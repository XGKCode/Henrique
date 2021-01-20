using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autuas.Dtos
{
    public class GoalUpdateDto
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public string GoalType { get; set; }
    }
}
