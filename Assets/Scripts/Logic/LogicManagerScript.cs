using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManagerScript : MonoBehaviour
{
    //Variables for general game state
    private int currentTurn = 1;
    private int numberOfTurns = 6;
    private int energy = 1;

    private bool gameFinished;

    //Player Variables
    PlayerHandScript playerHand;
    private int playerEnergy;

    //UI Objects
    GameObject canvas;
    TMP_Text turnButtonText;
    TMP_Text energyText;

    private void Start()
    {
        //Initialize Objects

        //Player Setup
        playerHand = GameObject.Find("PlayerHand").transform.gameObject.GetComponent<PlayerHandScript>();
        playerEnergy = energy;

        //UI Setup
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        turnButtonText = GameObject.Find("TurnButton").transform.GetComponent<RectTransform>().gameObject.transform.GetComponentInChildren<TMP_Text>();
        energyText = GameObject.Find("Energy").transform.GetComponent<RectTransform>().gameObject.transform.GetComponentInChildren<TMP_Text>();

        //Initialize UI
        setEnergyText();
        setButtonText();
    }

    public void nextTurn() {
        //Make the zones manage themselves
        updateZones();

        //Check if all turns are finished
        isGameFinished();

        //Setup the next turn
        setButtonText();

        //Update the energy available
        updateEnergy();

        playerHand.drawCard();
    }

    /***************
    Getter methods
    ***************/

    public int getEnergy() {
        return energy;
    }

    public int getPlayerEnergy() {
        return playerEnergy;
    }

    /****************
    Setting methods
    ****************/

    public void alterPlayerEnergy(int alterValue) {
        playerEnergy += alterValue;
        setEnergyText();
    }

    public void setEnergyText() {
        energyText.text = playerEnergy.ToString();
    }

    public void setButtonText() {
        string text = "End Turn " + currentTurn.ToString() + "/" + numberOfTurns.ToString();

        turnButtonText.text = text;
    }

    /**********************
    End of turn functions
    **********************/

    private void updateZones() {
        GameObject[] zones = GameObject.FindGameObjectsWithTag("Zone");

        foreach (GameObject zone in zones) {
            zone.GetComponentInChildren<ZoneEffectScript>().updateZone();
        }
    }

    private void updateEnergy() {
        if (!gameFinished) {
            energy++;
            playerEnergy = energy;

            setEnergyText();
        } else {
            Destroy(energyText.gameObject);
        }
    }

    private void isGameFinished() {
        currentTurn++;
        if (currentTurn > numberOfTurns) {
            resetScene();
        }
    }

    /*********************
    End of game functions
    *********************/

    public void resetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}