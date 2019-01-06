using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace atmps.domain.appServices.tests
{
    /// <summary>
    /// Money generator tests.
    /// </summary>
    public class MoneyGeneratorTests
    {
        /// <summary>
        /// Generates the money should return correct money set for correct amount.
        /// </summary>
        [Fact]
        public void GenerateMoney_ShouldReturnCorrectMoneySetForCorrectAmount()
        {
            var amount = 85;

            Dictionary<PaperNote, int> expectedNotes = new Dictionary<PaperNote, int>
            {
                {PaperNote.Fifty,1},
                {PaperNote.Twenty,1},
                {PaperNote.Ten,1},
                {PaperNote.Five,1}
            };

            var moneyGenerator = new MoneyGenerator();

            var result = moneyGenerator.GenerateMoney(amount);

            result.Should().NotBeNull();

            result.Notes.Count.Should().Be(expectedNotes.Count);

            result.Amount.Should().Be(amount);
        }
    }
}
