using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HandUtilities {
    public class CardSlot
    {
        public Vector3 position;
        public GameObject card;

        public void changeCard(GameObject newCard) {
            card = newCard;
        }

        public void changePosition(Vector3 newPosition) {
            position = newPosition;
        }

        public bool isEmpty() {
            bool isEmpty;

            if (position == null && card == null) {
                isEmpty = true;
            } else {
                isEmpty = false;
            }

            return isEmpty;
        }
    }
}
