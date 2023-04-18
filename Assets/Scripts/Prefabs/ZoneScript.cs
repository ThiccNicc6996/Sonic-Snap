using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ZoneScript : MonoBehaviour
{
    public bool isPlayerZone;

    BoxCollider2D zoneCollider;
    List<Vector3> zoneSpots = new List<Vector3>();
    List<GameObject> cards = new List<GameObject>();

    private int zonePower = 0;

    private ZoneEffectScript zoneEffectScript;
    private LogicManagerScript logicScript;

    private TextMeshPro powerText;

    List<Collider2D> TriggerList = new List<Collider2D>();

    void Start()
    {
        zoneCollider = this.gameObject.GetComponent<BoxCollider2D>();
        establishCardSpots();

        zoneEffectScript = transform.parent.Find("ZoneEffect").GetComponent<ZoneEffectScript>();
        logicScript = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicManagerScript>();

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

    public List<GameObject> getCards() {
        return cards;
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
        } else {
            Debug.Log("FULL");
        }
    }

    private void removeCard(GameObject card) {
        cards.Remove(card);
        if (!card.transform.gameObject.GetComponent<CardScript>().isMovable()) {
            logicScript.alterPlayerEnergy(card.gameObject.GetComponent<CardScript>().getCost());
        }
    }

    private void checkAbsentCards() {
        foreach (GameObject card in cards.ToList()) {
            if (!TriggerListContainsCard(card)) {
                removeCard(card);
            }
        }
    }

    private bool enoughEnergy(GameObject card) {
        return logicScript.getPlayerEnergy() >= card.GetComponent<CardScript>().getCost();
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
        applyZoneEffect();
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

    private void applyZoneEffect() {
        zoneEffectScript.onGoing();
    }

    private void revealLatestCards(List<GameObject> latest) {
        foreach (GameObject card in latest) {
            card.GetComponent<CardScript>().playCard();
        }
    }

    private void updateOtherCards(List<GameObject> latest) {
        foreach (GameObject card in cards) {
            if (!latest.Contains(card)) {
                card.GetComponent<CardScript>().updateCard();
            }
        }
    }

    private void calculatePower() {
        int newPower = 0;
        foreach (GameObject card in cards) {
            Debug.Log("POWER IS");
            Debug.Log(card.gameObject.GetComponent<CardScript>().getPower());
            newPower += card.gameObject.GetComponent<CardScript>().getPower();
        }

        zonePower = newPower;
        powerText.text = zonePower.ToString();
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