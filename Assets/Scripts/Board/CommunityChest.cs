using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CommunityChest : BoardLocation 
{
    public override void PassBy(Player player)
    {
        Debug.Log("Passed by community chest");
    }

    public override IEnumerator LandOn(Player player)
    {
        yield return new WaitForSeconds(1f);
        int i = UnityEngine.Random.Range(0, 8);
        switch (i)
        {
            case 0: //ADVANCE TO GO
                yield return player.JumpToSpace(PassGo.instance, 2f);
                print("ADVANCE TO GO");
                break;
            case 1: //BANK ERROR IN YOUR FAVOUR, COLLECT 200
                player.AdjustBalanceBy(200);
                print("BANK ERROR IN YOUR FAVOUR, COLLECT 200");
                break;
            case 2: //WON SECOND PRIZE IN BEAUTY COTEST, COLLECT 100
                player.AdjustBalanceBy(100);
                print("WON SECOND PRIZE IN BEAUTY COTEST, COLLECT 100");
                break;
            case 3: //DOCTOR FEE, PAY 50
                print("DOCTOR FEE, PAY 50");
                player.AdjustBalanceBy(-50);
                break;
            case 4: //GO TO JAIL WITHOUT COLLECTION 200
                print("GO TO JAIL WITHOUT COLLECTION 200");
                //int moveTo = Int32.Parse(this.gameObject.name) > Int32.Parse(InJail.instance.gameObject.name) ? Int32.Parse(InJail.instance.gameObject.name) - Int32.Parse(this.gameObject.name) : Int32.Parse(InJail.instance.gameObject.name) - Int32.Parse(this.gameObject.name);
                yield return player.MoveSpaces(Int32.Parse(InJail.instance.gameObject.name) - Int32.Parse(this.gameObject.name));
                //yield return player.JumpToSpace(InJail.instance, 2f);
                break;
            case 5: //LIFE INSURANCE MATURES, COLLECT 100
                print("LIFE INSURANCE MATURES, COLLECT 100");
                player.AdjustBalanceBy(100);
                break;
            case 6: //GRAND OPERA OPENING, COLLECT 50 FROM EVERY PLAYER
                print("GRAND OPERA OPENING, COLLECT 50 FROM EVERY PLAYER");
                foreach(Player p in Gameplay.instance.players)
                {
                    if (p != player)
                    {
                        p.AdjustBalanceBy(-50);
                    }
                }
                player.AdjustBalanceBy(50);
                break;
            case 7: //PAY SCHOOL TAX, OF 150
                print("PAY SCHOOL TAX, OF 150");
                player.AdjustBalanceBy(-150);
                break;
        }
        //yield return player.JumpToSpace(InJail.instance, 2f); <- this makes the player move to jail space

        Debug.Log("Landed on community chest");

        yield return null;
    }
}
