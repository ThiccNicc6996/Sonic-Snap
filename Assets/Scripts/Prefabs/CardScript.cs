using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

using static PowerUtils.Modifier;

public class CardScript : MonoBehaviour
{
    //Script which has attributes of specific card
    SpecialScript specialScript;
    //Original values
    public int coreCost;
    public int corePower;

    //Potentially modified values
    private int cost;
    private int power;

    //List of values that change the card's power
    private List<PowerUtils.Modifier> mods = new List<PowerUtils.Modifier>();

    /*****************************************
    Variables for whether cards can be moved
    *****************************************/

    //Whether card is locked into place (ex: card was played on previous turn)
    private bool locked = false;
    //Whether the card can currently move to another zone DESPITE being locked
    public bool movable = false;

    private GameObject currentZone;

    //Text for power and cost
    private TextMeshPro powerText;
    private TextMeshPro costText;

    void Start() {
        specialScript = this.GetComponent<SpecialScript>();

        cost = coreCost;
        power = corePower;

        powerText = this.gameObject.transform.Find("PowerText").gameObject.GetComponent<TextMeshPro>();
        costText = this.gameObject.transform.Find("CostText").gameObject.GetComponent<TextMeshPro>();

        powerText.text = corePower.ToString();
        costText.text = coreCost.ToString();
    }

    public void playCard() {
        if (specialScript) {
            specialScript.playCard();
        }
        applyPowerMods();
    }

    public void updateCard() {
        if (specialScript) {
            specialScript.updateCard();
        }
        applyPowerMods();
    }

    /****************
    Getter functions
    ****************/

    public int getCost() {
        return cost;
    }

    public int getPower() {
        return power;
    }

    public GameObject getZone() {
        return currentZone;
    }

    public bool isLocked() {
        return locked;
    }

    public bool isMovable() {
        return movable;
    }

    public void lockCard() {
        locked = true;
    }

    /******************
    * Update functions*
    ******************/

    public void updateMovable() {
        movable = !movable;
    }

    public void updateZone(GameObject newZone) {
        currentZone = newZone; 
    }

    public void updatePowerText() {
        powerText.text = power.ToString();
    }

    /*********************
    * Modifier functions *
    *********************/

    private void applyPowerMods() {
        int newPower = corePower;

        foreach (PowerUtils.Modifier mod in mods) {
            if (mod.getType() == "Add") {
                newPower += mod.getValue();
            }
        }

        foreach (PowerUtils.Modifier mod in mods) {
            if (mod.getType() == "Subtract") {
                newPower -= mod.getValue();
            }
        }

        foreach (PowerUtils.Modifier mod in mods) {
            if (mod.getType() == "Multiply") {
                newPower = newPower * mod.getValue();
            }
        }

        foreach (PowerUtils.Modifier mod in mods) {
            if (mod.getType() == "Divide") {
                newPower = newPower * mod.getValue();
            }
            power = power / mod.getValue();
        }

        power = newPower;
        updatePowerText();
    }

    public bool hasPowerMod(PowerUtils.Modifier mod) {
        bool hasMod = false;
        if (mods.Contains(mod)) {
            hasMod = true;
        }

        return hasMod;
    }

    public void addPowerMod(PowerUtils.Modifier mod) {
        mods.Add(mod);
    }
}
