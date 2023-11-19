using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.Dtos
{
    public class GateDto
    {
        public int Id { get; set; }
        public int Number { get; set; }

        public TerminalShortDto? Terminal { get; set; }
        public int TerminalId { get; set; }
    }
}
