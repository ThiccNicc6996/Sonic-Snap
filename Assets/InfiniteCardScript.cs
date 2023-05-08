using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteCardScript : CardScript
{
    PowerUtils.Modifier negativeValue = new PowerUtils.Modifier("Infinite", "Multiply", -1);

    public override void Start() {
        base.Start();
    }

    public override void onReveal()
    {
        List<GameObject> allCards = getOtherCards();
        List<GameObject> oppositeCards = getOpposingCards();
        allCards.AddRange(oppositeCards);

        foreach (GameObject card in allCards) {
            card.GetComponent<CardScript>().addPowerMod(negativeValue);
        }
    }
}
