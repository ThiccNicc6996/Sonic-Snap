using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PowerUtils.Modifier;

public class StardustSpeedwayScript : ZoneScript
{
    private string zoneName = "Stardust Speedway";
    private string effectText = "Cards here have +5 power";
    private PowerUtils.Modifier effect;

    private ZoneScript playerZone;
    private ZoneScript enemyZone;

    public override void Start() {
        effect = new PowerUtils.Modifier(zoneName, "Add", 5);

        playerZone = this.transform.parent.Find("PlayerZone").GetComponent<ZoneScript>();
        enemyZone = this.transform.parent.Find("EnemyZone").GetComponent<ZoneScript>();
    }

    public override void onGoing() {
        addFivePowerToCards(playerZone);
        addFivePowerToCards(enemyZone);
    }

    private void addFivePowerToCards(ZoneScript targetZone) {
        foreach (GameObject card in targetZone.getCards()) {
            CardScript cardScript = card.GetComponent<CardScript>();
            if (!cardScript.hasPowerMod(effect)) {
                cardScript.addPowerMod(effect);
            }
        }
    }
}
