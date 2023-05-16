using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static PowerUtils.Modifier;

public class StardustSpeedwayScript : ZoneEffectScript
{
    public override void Start() {
        isRevealed = true;
        zoneName = "Stardust Speedway";
        effectText = "Cards here have +5 power";

        effect = new PowerUtils.Modifier(zoneName, "Add", 5);

        base.Start();
    }

    public override void perCard(Card cardScript) {
        if (!cardScript.hasPowerMod(effect)) {
            cardScript.addPowerMod(effect);
        }
    }
}
