﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ControlStock
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public int Stock { get; set; }
        public int StockMax { get; set; }
        public int StockMin { get; set; }
    }

}
