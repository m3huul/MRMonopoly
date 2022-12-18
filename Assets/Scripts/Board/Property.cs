using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Property : Ownable
{
    [SerializeField] public List<Property> otherProperties;

    [Tooltip("These are cumulative, don't write total price")]
    [SerializeField] public int housePrice, hotelPrice;

    [Tooltip("These are NOT cumulative, write total price")]
    [SerializeField] public List<int> rentPrices;
    
    private int currentUpgradeLevel;

    protected override int ChargePlayer()
    {
        return rentPrices[currentUpgradeLevel];
    }

    public override bool BuildHouses(Ownable owner)
    {
        for(int i = 0; i < otherProperties.Count; i++)
        {
            if(otherProperties[i].owner != owner.owner || otherProperties[i].owner == null)
                return false;
        }
        return true;
    }

    public override bool ColorSet(Player player)
    {
        foreach(Property p in otherProperties)
        {
            if(p.owner != player)
            {
                return false;
            }
        }
        return true;
    }
}
