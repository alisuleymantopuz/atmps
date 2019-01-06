using System.Collections.Generic;

namespace atmps.domain
{
    /// <summary>
    /// Money.
    /// </summary>
    public class Money
    {
        public int Amount { get; set; }
        public Dictionary<PaperNote,int> Notes { get; set; }
    }
}
