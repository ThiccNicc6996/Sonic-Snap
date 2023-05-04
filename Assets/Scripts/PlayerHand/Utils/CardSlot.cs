using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandUtilities {
    public class CardSlot
    {
        public Vector3 position;
        public GameObject card;

        public CardSlot(GameObject initCard) {
            card = initCard;
        }

        public void changeCard(GameObject newCard) {
            card = newCard;
        }

        public void changePosition(Vector3 newPosition) {
            position = newPosition;
            card.transform.position = position;
        }

        public bool compareCard (GameObject otherCard) {
            bool isCard = false;

            if (otherCard == card) {
                isCard = true;
            }

            return isCard;
        }
    }
}
