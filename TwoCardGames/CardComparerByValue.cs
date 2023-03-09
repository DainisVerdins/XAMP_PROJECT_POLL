using System.Collections.Generic;

namespace TwoCardGames
{
    internal class CardComparerByValue : IComparer<Card>
    {
        public int Compare(Card? x, Card? y)
        {
            if (x.Value > y.Value)
                return 1;
            if (x.Value < y.Value)
                return -1;
            return 0;

        }
    }
}