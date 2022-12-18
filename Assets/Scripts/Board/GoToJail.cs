using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GoToJail : BoardLocation 
{
    public override void PassBy(Player player)
    {
    }

    public override IEnumerator LandOn(Player player)
    {
        yield return MessageAlert.instance.DisplayAlert("Uh oh...", Color.red);

        yield return player.RotateAdditionalDegrees(90, 1f);

        yield return player.MoveSpaces(-20);
        //yield return player.JumpToSpace(InJail.instance, 2f);
    }
}
