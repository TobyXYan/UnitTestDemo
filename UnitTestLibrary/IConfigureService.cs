﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestLibrary
{
    public interface IConfigureService
    {
        void Configure(SimpleContainer container);
    }
}
