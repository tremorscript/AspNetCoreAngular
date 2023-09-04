// <copyright file="MachineDateTime.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using AspNetCoreAngular.Common;

namespace AspNetCoreAngular.Infrastructure.Services
{
    public class MachineDateTime : IDateTime
    {
        public static int CurrentYear => DateTime.Now.Year;

        public DateTime Now => DateTime.Now;
    }
}
