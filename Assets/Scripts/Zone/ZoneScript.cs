using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ZoneScript : MonoBehaviour
{
    //Main variables
    public bool isPlayerZone;
    public bool isRevealed = false;
    private int zonePower = 0;
    List<GameObject> cards = new List<GameObject>();

    //Variables for outside scripts/objects
    private ZoneEffectScript zoneEffectScript;
    private LogicManagerScript logicScript;
    private PlayerHandScript handScript;
    private TextMeshPro powerText;

    //Collision detection variables
    BoxCollider2D zoneCollider;
    List<Collider2D> TriggerList = new List<Collider2D>();
    List<Vector3> zoneSpots = new List<Vector3>();

    public virtual void Start()
    {
        zoneCollider = this.gameObject.GetComponent<BoxCollider2D>();
        establishCardSpots();

        zoneEffectScript = this.transform.parent.Find("ZoneEffect").GetComponent<ZoneEffectScript>();

        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();

        handScript = GameObject.FindGameObjectWithTag("PlayerHand").GetComponent<PlayerHandScript>();

        powerText = this.gameObject.transform.Find("PowerText").gameObject.GetComponent<TextMeshPro>();
        powerText.text = zonePower.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            List<GameObject> newCards = gatherNewCards();

            foreach (GameObject card in newCards) {
                if (enoughEnergy(card)) {
                    addCard(card);
                }
            }
            checkAbsentCards();
        }

    }

    public virtual void onReveal() {}
    
    public virtual void onGoing() {}

    public List<GameObject> getCards() {
        return cards;
    }


    private bool enoughEnergy(GameObject card) {
        return logicScript.getPlayerEnergy() >= card.GetComponent<CardScript>().getCost();
    }

    /*******************
    * Getter Functions *
    *******************/

    public ZoneEffectScript getZoneEffect() {
        return zoneEffectScript;
    }

    /************************************
    Functions for initializing the zone
    *************************************/

    private void establishCardSpots() {
        Vector3 zoneSize = zoneCollider.bounds.size;
        float startPosX = transform.position.x - zoneSize.x/2;
        float startPosY = transform.position.y - zoneSize.y/2;

        zoneSpots.Add(new Vector3(
            startPosX + zoneSize.x/3,
            startPosY + zoneSize.y/3,
            0
        ));
        zoneSpots.Add(new Vector3(
            startPosX + (zoneSize.x/3)*2,
            startPosY + zoneSize.y/3,
            0
        ));
        zoneSpots.Add(new Vector3(
            startPosX + zoneSize.x/3,
            startPosY + (zoneSize.y/3)*2,
            0
        ));
        zoneSpots.Add(new Vector3(
            startPosX + (zoneSize.x/3)*2,
            startPosY + (zoneSize.y/3)*2,
            0
        ));
    }


    /**********************
    End of turn functions
    **********************/

    public void updateZone() {
        List<GameObject> latestCards = gatherLatestCards();
        lockZone();
        revealLatestCards(latestCards);
        updateOtherCards(latestCards);
        calculatePower();
    }

    private List<GameObject> gatherLatestCards() {
        List<GameObject> latest = new List<GameObject>();

        foreach (GameObject card in cards) {
            if (!card.GetComponent<CardScript>().isLocked()) {
                latest.Add(card);
            }
        }

        return latest;
    }

    //Lock all of the cards in the zone
    private void lockZone() {
        foreach(GameObject card in cards) {
            card.GetComponent<CardScript>().lockCard();
            card.GetComponent<CardScript>().updateZone(this.gameObject);
        }
    }

    private void revealLatestCards(List<GameObject> latest) {
        foreach (GameObject card in latest) {
            CardScript cardScript = card.GetComponent<CardScript>();
            cardScript.playCard();
            cardScript.onReveal();
            cardScript.onGoing();

            zoneEffectScript.perCard(cardScript);
            cardScript.perCard();
        }
    }

    private void updateOtherCards(List<GameObject> latest) {
        foreach (GameObject card in cards) {
            if (!latest.Contains(card)) {
                CardScript cardScript = card.GetComponent<CardScript>();
                cardScript.onGoing();
                cardScript.updateCard();
            }
        }
    }

    private void calculatePower() {
        int newPower = 0;
        foreach (GameObject card in cards) {
            newPower += card.gameObject.GetComponent<CardScript>().getPower();
        }

        zonePower = newPower;
        powerText.text = zonePower.ToString();
    }

    /************************************************
    * Functions for getting information about cards *
    ************************************************/

    public List<GameObject> getOtherCards(GameObject card) {
        List<GameObject> otherCards = new List<GameObject>();

        foreach (GameObject other in cards) {
            if (other != card) {
                otherCards.Add(other);
            }
        }

        return otherCards;
    }

    /****************************************
    Functions for adding and removing cards
    ****************************************/

    private void addCard(GameObject newCard) {
        if (cards.Count < zoneSpots.Count) {
            int slotNum = cards.Count;

            cards.Add(newCard);
            newCard.gameObject.transform.position = zoneSpots[slotNum];

            logicScript.alterPlayerEnergy(newCard.gameObject.GetComponent<CardScript>().getCost() * -1);

            handScript.removeCard(newCard);
        } else {
            Debug.Log("FULL");
        }
    }

    private void removeCard(GameObject card) {
        cards.Remove(card);
        logicScript.alterPlayerEnergy(card.gameObject.GetComponent<CardScript>().getCost());
    }

    private void checkAbsentCards() {
        foreach (GameObject card in cards.ToList()) {
            if (!TriggerListContainsCard(card)) {
                removeCard(card);
            }
        }
    }

    /*********************************
    Functions for detecting collision
    *********************************/

    private List<GameObject> gatherNewCards() {
        List<GameObject> newCards = new List<GameObject>();

        foreach(Collider2D collider in TriggerList) {
            GameObject newCard = collider.gameObject;
            if (!cards.Contains(newCard)) {
                newCards.Add(newCard);
            }
        }

        return newCards;
    }

    private bool TriggerListContainsCard(GameObject card) {
        bool containsCard = false;
        foreach (Collider2D collider in TriggerList) {
            if (collider.gameObject == card) {
                containsCard = true;
                break;
            }
        }

        return containsCard;
    }

     //Called when something enters the trigger
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if the object is not already in the list
        if(!TriggerList.Contains(collider))
        {
            //add the object to the list
            TriggerList.Add(collider);
        }
    }
    
    //Called when something exits the trigger
    private void OnTriggerExit2D(Collider2D collider)
    {
        //if the object is in the list
        if(TriggerList.Contains(collider))
        {
            //remove it from the list
            TriggerList.Remove(collider);
        }
    }
}
