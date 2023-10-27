﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Terminal
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Location { get; set; }


        public List<Gate> Gates { get; set; }

    }
}
