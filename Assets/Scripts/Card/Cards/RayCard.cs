using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCard : Card
{
    private bool cardPlayed = false;
    private bool hasMoved = false;
    private ZoneField originalZoneField;

    private Card cardScript;

    public override void Start() {
        base.Start();
    }

    public override void playCard() {
        cardPlayed = true;
        originalZoneField = currentZoneField;

        base.playCard();
    }

    public override void updateCard() {
        if (!hasMoved) {
            zoneChangeCheck();
        }

        base.updateCard();
    }

    private void zoneChangeCheck() {
        if (currentZoneField != originalZoneField) {
            movable = false;
            hasMoved = true;
        }
    }
}
