using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PowerUtils.Modifier;

public class NegaWispCard : Card
{
    PowerUtils.Modifier addTwo = new PowerUtils.Modifier("NegaWisp", "Add", 2);
    // Start is called before the first frame update
    public override void Awake()
    {
        cardName = "NegaWisp";
        base.Awake();
    }

    public override void onReveal() {
        List<Card> otherCards = currentZoneField.GetComponent<ZoneField>().getOtherCards(this);

        foreach (Card card in otherCards) {
            card.onDestroy();
            addPowerMod(addTwo);
        }
    }

    public override void updateCard() {
        base.updateCard();
    }
}
