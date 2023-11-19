using BLL.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IGateService
    {
        Task<GateDto?> CreateAsync(GateDto gate);

        Task<IEnumerable<GateDto>> GetAllAsync();


    }
}
