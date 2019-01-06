using System;
using System.Collections.Generic;

namespace atmps.domain.appServices
{
    /// <summary>
    /// Money generator.
    /// </summary>
    public class MoneyGenerator : IMoneyGenerator
    {
        /// <summary>
        /// Generates the money to be dispensed as set.
        /// </summary>
        /// <returns>The money.</returns>
        /// <param name="amount">Amount.</param>
        public Money GenerateMoney(int amount)
        {
            int tempAmount = amount;
            int[] notes = { 50, 20, 10, 5 };
            int[] noteCounter = new int[4];
            Dictionary<PaperNote, int> paperNotes = new Dictionary<PaperNote, int>();

            for (int i = 0; i < noteCounter.Length; i++)
            {
                if (tempAmount >= notes[i])
                {
                    noteCounter[i] = tempAmount / notes[i];
                    tempAmount = tempAmount - noteCounter[i] * notes[i];
                }
            }

            for (int i = 0; i < noteCounter.Length; i++)
            {
                if (noteCounter[i] != 0)
                {
                    PaperNote paperNote = (PaperNote)notes[i];
                    int counter = noteCounter[i];
                    paperNotes.Add(paperNote, counter);
                }
            }

            return new Money() { Amount = amount, Notes = paperNotes };
        }
    }
}
