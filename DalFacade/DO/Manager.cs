﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
