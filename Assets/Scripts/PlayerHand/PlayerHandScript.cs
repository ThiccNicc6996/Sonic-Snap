using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static HandUtilities.CardSlot;

public class PlayerHandScript : MonoBehaviour
{
    public GameObject deckObject;
    private PlayerDeckScript deck;

    private bool canDraw = true;

    //Variables for managing position of cards
    private Vector3 centerPos;
    private List<HandUtilities.CardSlot> slots = new List<HandUtilities.CardSlot>();
    
    void Start()
    {
        centerPos = this.gameObject.transform.localPosition;

        deck = deckObject.gameObject.GetComponent<PlayerDeckScript>();

        drawOpeningHand();
    }

    public void drawCard() {
        if (canDraw && slots.Count <= 7) {
            string cardName = deck.drawCard();
            string pathName = "GameObjects/Cards/" + cardName + "Card";

            GameObject card = (GameObject)Instantiate(Resources.Load(pathName), transform.position, Quaternion.identity);

            addToSlots(card);
        } else {
            Debug.Log("HAND FULL");
        }
    }

    private void drawOpeningHand() {
        drawCard();
        drawCard();
        drawCard();
    }

    private void addToSlots(GameObject card) {
        addToTop(card);
        shiftSlots();
    }

    private void addToTop(GameObject card) {
        slots.Add(new HandUtilities.CardSlot(card));
    }

    private void shiftSlots() {
        float spaceLength = 2.0f;
        float offsetLength;

        if (isEven()) {
            offsetLength = spaceLength/2;
        } else {
            offsetLength = 0;
        }

        float centerNum = (float)Math.Ceiling(slots.Count/2.0);

        for (int i=0; i<slots.Count; i++) {
            float xPos;
            float difference = (float)Math.Floor(Math.Abs(centerNum-i));
            float centerOffset = ((difference*spaceLength) + offsetLength);

            if (i < centerNum) {
                xPos = centerPos.x - centerOffset;
            } else if (i > centerNum) {
                xPos = centerPos.x + centerOffset;
            } else {
                xPos = centerPos.x;
            }

            slots[i].changePosition(new Vector3(xPos, centerPos.y, centerPos.z));
        }
    }

    public bool containsCard(GameObject card) {
        bool contains = false;
        
        for (int i=0; i< slots.Count; i++) {
            if (slots[i].compareCard(card)) {
                contains = true;
                break;
            }
        }

        return contains;
    }

    public void removeCard(GameObject card) {
        for (int i=0; i< slots.Count; i++) {
            if (slots[i].compareCard(card)) {
                slots.RemoveAt(i);
            }
        }

        shiftSlots();
    }

    private bool isEven() {
        bool even;

        if (slots.Count%2 == 0) {
            even = true;
        } else {
            even = false;
        }

        return even;
    }
}
