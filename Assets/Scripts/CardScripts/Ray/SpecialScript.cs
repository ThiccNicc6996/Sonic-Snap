using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialScript : MonoBehaviour
{
    private bool cardPlayed = false;
    private bool hasMoved = false;
    private GameObject originalZone;

    private CardScript cardScript;

    void Start() {
        cardScript = this.gameObject.GetComponent<CardScript>();
    }

    public void playCard() {
        cardPlayed = true;
        originalZone = cardScript.getZone();
    }

    public void updateCard() {
        if (!hasMoved) {
            zoneChangeCheck();
        }
    }

    private void zoneChangeCheck() {

        if (cardScript.getZone() != originalZone) {
            cardScript.updateMovable();
            hasMoved = true;
        }
    }
}
