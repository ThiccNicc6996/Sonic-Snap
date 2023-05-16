using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandUtilities {
    public class CardSlot
    {
        public Vector3 position;
        public Card card;

        public CardSlot(Card initCard) {
            card = initCard;
        }

        public void changeCard(Card newCard) {
            card = newCard;
        }

        public void changePosition(Vector3 newPosition) {
            position = newPosition;
            card.transform.position = position;
        }

        public bool compareCard (Card otherCard) {
            bool isCard = false;

            if (otherCard == card) {
                isCard = true;
            }

            return isCard;
        }
    }
}
