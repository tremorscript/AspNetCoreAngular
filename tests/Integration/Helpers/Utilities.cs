// <copyright file="Utilities.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AspNetCoreAngular.Integration.Tests.Helpers
{
    using AspNetCoreAngular.Infrastructure.Persistence;

    public static class Utilities
    {
        public static void InitializeDbForTests(ApplicationDbContext db)
        {
            // db.Messages.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        // public static List<Message> GetSeedingMessages()
        // {
        //    return new List<Message>()
        //    {
        //        new Message(){ Text = "TEST RECORD: You're standing on my scarf." },
        //        new Message(){ Text = "TEST RECORD: Would you like a jelly baby?" },
        //        new Message(){ Text = "TEST RECORD: To the rational mind, " +
        //            "nothing is inexplicable; only unexplained." }
        //    };
        // }
    }
}
