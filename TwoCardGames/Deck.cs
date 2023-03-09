using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwoCardGames.Enums;

namespace TwoCardGames;

public class Deck : ObservableCollection<Card>
{
    private static Random random = new Random();
    public Deck()
    {
        Reset();
    }

    public void Reset()
    {
        /* Call Clear() to remove all cards from the deck, then use two for loops to add
         * 
        * all combinations of suit and value, calling Add(new Card(...)) to add each card */
        base.Clear();
        foreach (Suits suit in Enum.GetValues(typeof(Suits)))
        {
            foreach (Values val in Enum.GetValues(typeof(Values)))
            {
                base.Add(new Card(val, suit));
            }
        }
    }
    public Card Deal(int index)
    {
        Card cardToDeal = base[index];
        RemoveAt(index);
        return cardToDeal;

    }
    public void Shuffle()
    {
        var copy = new List<Card>(this);
        Clear();
        while (copy.Count > 0)
        {
            int index = random.Next(copy.Count);
            Card card = copy[index];
            base.Add(card);
            copy.RemoveAt(index);
        }


    }
    public void Sort()
    {
        List<Card> sortedCards = new List<Card>(this);
        sortedCards.Sort(new CardComparerByValue());
        
        base.Clear ();

        foreach (var card in sortedCards)
        {
            base.Add(card);
        }
    }
}
