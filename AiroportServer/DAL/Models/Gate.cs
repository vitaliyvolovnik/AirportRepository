using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Gate
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public Terminal? Terminal { get; set; }
        public int TerminalId { get; set; }
    }
}
