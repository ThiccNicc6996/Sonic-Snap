using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PowerUtils.Modifier;

public class NegaWispCardScript : CardScript
{
    PowerUtils.Modifier addTwo = new PowerUtils.Modifier("NegaWisp", "Add", 2);
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void onReveal() {
        List<GameObject> otherCards = currentZone.GetComponent<ZoneScript>().getOtherCards(this.gameObject);

        foreach (GameObject card in otherCards) {
            card.GetComponent<CardScript>().onDestroy();
            addPowerMod(addTwo);
        }
    }

    public override void updateCard() {
        base.updateCard();
    }
}
