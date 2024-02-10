using AutoMapper;
using BLL.Models.Dtos;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TerminalService : ITerminalService
    {
        private readonly TerminalRepository _terminalRepository;
        private readonly IMapper _mapper;

        public TerminalService(TerminalRepository terminalRepository,IMapper maper)
        {
            this._terminalRepository = terminalRepository;
            this._mapper = maper;
        }

        public async Task<TerminalDto?> CreateAsync(TerminalDto terminalDto)
        {
            var terminal = _mapper.Map<Terminal>(terminalDto);
            
            return _mapper.Map<TerminalDto?>(await _terminalRepository.CreateAsync(terminal));

        }

        public async Task<IEnumerable<TerminalDto>> GetAllAsync()
        {
            return (await  _terminalRepository.GetAllAsync()).Select(_mapper.Map<TerminalDto>);
        }
    }
}
