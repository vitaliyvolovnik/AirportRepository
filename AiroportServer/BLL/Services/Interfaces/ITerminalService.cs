using BLL.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface ITerminalService
    {
        Task<IEnumerable<TerminalDto>> GetAllAsync();
        Task<TerminalDto?> CreateAsync(TerminalDto terminalDto);

    }
}
