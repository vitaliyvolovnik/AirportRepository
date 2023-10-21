﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<bool> IsEmailExistAsync(string email);
    }
}
