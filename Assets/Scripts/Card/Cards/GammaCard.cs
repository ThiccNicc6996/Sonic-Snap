using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaCard: Card {
    
    public override void Awake() {
        cardName = "Gamma";
        base.Awake();
    }
    
     public override void onGoing() {
        int oppositeCardsCount = getOpposingCards().Count;
        List<Card> opposingCards = getOpposingCards();

        PowerUtils.Modifier addXPower = new PowerUtils.Modifier("Gamma", "Add", oppositeCardsCount);

        addPowerMod(addXPower);
    }

}