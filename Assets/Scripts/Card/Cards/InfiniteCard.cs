using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteCard : Card
{
    PowerUtils.Modifier negativeValue = new PowerUtils.Modifier("Infinite", "Multiply", -1);

    public override void Start() {
        cardName = "Infinite";
        base.Start();
    }

    public override void onReveal()
    {
        List<Card> allCards = getOtherCards();
        List<Card> oppositeCards = getOpposingCards();
        allCards.AddRange(oppositeCards);

        foreach (Card card in allCards) {
            card.addPowerMod(negativeValue);
        }
    }
}
