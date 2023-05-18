using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneEffectScript : MonoBehaviour
{
    protected bool isRevealed = false;

    protected string zoneName = "";
    protected string effectText = "";
    protected PowerUtils.Modifier effect;

    protected ZoneField playerZone;
    protected ZoneField enemyZone;

    public virtual void Start() {
        playerZone = this.transform.parent.Find("PlayerZone").GetComponent<ZoneField>();
        enemyZone = this.transform.parent.Find("EnemyZone").GetComponent<ZoneField>();
    }

    public void updateZone() {
        playerZone.updateZone();
        enemyZone.updateZone();

        if (isRevealed) {
            onGoing();
        }
    }

    public void revealZoneEffect() {
        isRevealed = true;
        onReveal();
    }

    //Runs once when zone is revealed.
    public virtual void onReveal() {}

    //Runs after all cards are played on both sides
    public virtual void onGoing() {}

    //Runs after each card is played
    public virtual void perCard(Card cardScript) {}

    /*******************
    * Getter Functions *
    *******************/

    public ZoneField getPlayerZone() {
        return playerZone;
    }

    public ZoneField getEnemyZone() {
        return enemyZone;
    }
}
