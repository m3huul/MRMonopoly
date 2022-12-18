using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    //public static string _color;

    public static string _Name;
    public static int _housePrice;
    public static int _rent;

    public Inventory(string name, int housePrice, int rent)
    {
        Name = name;
        HousePrice = housePrice;
        Rent = rent;
    }

    public void printDetails()
    {
        Debug.Log(_Name + " " + _rent);
    }

    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }

    public int HousePrice
    {
        get { return _housePrice; }
        set { _housePrice = value; }
    }

    public int Rent
    {
        get {  return _rent; }
        set { _rent = value; }
    }
}
