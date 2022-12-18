using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Utility : Ownable
{
	[SerializeField] private Utility otherUtility;

	protected override int ChargePlayer()
	{
		int toCharge = DieRoller.instance.GetDieRollResults().Sum();
		toCharge *= (otherUtility.owner == owner ? 10 : 4);

		return toCharge;
	}

	public override bool BuildHouses(Ownable owner)
	{
		return false;
	}

	public override bool ColorSet(Player player)
	{
		throw new System.NotImplementedException();
	}
}
