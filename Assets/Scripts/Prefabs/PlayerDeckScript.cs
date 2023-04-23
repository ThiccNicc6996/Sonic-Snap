using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeckScript : MonoBehaviour
{
    private string[] cardList = {
        "Ray",
        "Laser",
        "Amy",
        "NegaWisp",
        "Tails",
        "Gamma",
        "Knuckles",
        "Rouge",
        "Sonic",
        "Eggman",
        "Mephiles",
        "Giganto"
    };

    public string drawCard() {
        System.Random random = new System.Random();

        int cardNum = random.Next(cardList.Length);
        string drawnCard = cardList[cardNum];

        removeCard(cardNum);
        return drawnCard;
    }

    public void removeCard(int index) {
        for (int i = index; i < cardList.Length-1; i++) {
            cardList[i] = cardList[i + 1];
        }
    }
}
