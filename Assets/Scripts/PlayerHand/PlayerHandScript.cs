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
    private float center;
    private HandUtilities.CardSlot[] slots = new HandUtilities.CardSlot[7];
    
    void Start()
    {
        center = this.gameObject.transform.localPosition.x;
        deck = deckObject.gameObject.GetComponent<PlayerDeckScript>();

        drawOpeningHand();
    }

    public void drawCard() {
        if (canDraw) {
            string cardName = deck.drawCard();
            string pathName = "GameObjects/Cards/" + cardName + "Card";

            GameObject card = (GameObject)Instantiate(Resources.Load(pathName), transform.position, Quaternion.identity);
            // addToSlots(card);
        }
    }

    private void drawOpeningHand() {
        drawCard();
        drawCard();
        drawCard();
    }

    private void addToSlots(GameObject card) {
        addToTop(card);
        // shiftSlots();
    }

    private void addToTop(GameObject card) {
        for (int i=0; i<slots.Length; i++) {
            if (slots[i] == null) {
                slots[i].changeCard(card);
                break;
            }
        }
    }

    // private void shiftSlots() {
    //     int slotNum = 0;
    //     bool moreSlots = true;

    //     while(moreSlots && slotNum < slots.Length) {
    //         if (slots[slotNum].isEmpty()) {
    //             moreSlots = false;
    //         }

    //         slotNum++;
    //     }

    //     if (slotNum % 2 == 0) {
    //         shiftSlotsForEven(slotNum);
    //     } else {
    //         shiftSlotsForOdd(slotNum);
    //     }
    // }

    // private void shiftSlotsForOdd(int slotNum) {
    //     float centerNum = (float)Math.Ceiling(slotNum/2.0);

    //     for (int i=0; i<slotNum; i++) {
    //         HandUtilities.CardSlot currSlot = slots[i];

    //         float xPos = center + 20f*(centerNum-(float)i);
    //         Vector3 newPosition = new Vector3(xPos, currSlot.position.y, currSlot.position.z);

    //         slots[i].changePosition(newPosition);
    //     }
    // }

    // private void makeXPosition(int slotNum) {
    //     float xPos;
    //     if (slotNum % 2 == 0) {
    //         xPos = center + 20f()
    //     }
    // }
}
