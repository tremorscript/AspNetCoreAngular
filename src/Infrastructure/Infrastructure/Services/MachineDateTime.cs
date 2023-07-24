using System;
using AspNetCoreAngular.Common;

namespace AspNetCoreAngular.Infrastructure.Services
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;
    }
}
