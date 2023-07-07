using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRegistry : MonoBehaviour
{
    public GameObject[] cardList;
    private Dictionary<string, GameObject> registry = new Dictionary<string, GameObject>();

    void Start()
    {
        foreach (GameObject card in cardList) {
            string cardName = card.name.Replace("Card", "");

            registry.Add(cardName, card);
        }
    }

    public GameObject retreiveCard(string cardName, string variantName) {
        GameObject card;

        if (registry.ContainsKey(cardName)) {
            card = registry[cardName];
        }
        else {
            card = registry["Basic"];
        }

        return card;
    }
}
