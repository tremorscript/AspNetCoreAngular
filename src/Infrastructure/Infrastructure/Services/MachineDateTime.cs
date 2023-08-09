// <copyright file="MachineDateTime.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Infrastructure.Services
{
    using System;
    using AspNetCoreAngular.Common;

    public class MachineDateTime : IDateTime
    {
        public static int CurrentYear => DateTime.Now.Year;

        public DateTime Now => DateTime.Now;
    }
}
