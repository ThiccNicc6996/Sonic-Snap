using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

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
        "Infinite",
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
        int topIndex = cardList.Length-1;
        for (int i = index; i < topIndex; i++) {
            cardList[i] = cardList[i + 1];
        }

        List<string> newList = cardList.ToList();
        newList.RemoveAt(topIndex);

        cardList = newList.ToArray();
    }
}
