using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI : MonoBehaviour
{
    public List<string> colorSet= new List<string>();
    //public RestAPI restAPI;
    private void Start()
    {
        //restAPI = RestAPI.Instance;
    }

    public void duplicates()
    {
        
    }
    public string BuyOrSkip(Ownable ownable)
    {
        int[] fifty_fifty = { 1, 1, 1, 2, 2, 2 };
        int[] seventy_thirty = { 1, 1, 1, 1, 1, 1, 1, 2, 2, 2 };
        int[] eighty_twenty = { 1, 1, 1, 1, 1, 1, 1, 1, 2, 2 };
        int[] ninty_ten = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2 };


        var duplicates = colorSet.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key);
        foreach (var i in duplicates)
        {
            if (i == ownable.colorName)
            {
                print("we have duplicates of this");
                colorSet.Add(ownable.colorName);
                return "buy";
            }
        }

        //foreach (string s in colorSet)
        //{
        //    if (s == ownable.colorName)
        //    {
        //        var twoColorProperties = restAPI.colors.GroupBy(i => i).Where(g => g.Count() == 2).Select(g => g.Key);
        //        foreach(var i in twoColorProperties)
        //        {
        //            if(i == s)
        //            {
        //                print("Only 2 cards so AI is buying this one");
        //                colorSet.Add(ownable.colorName);
        //                return "buy";
        //            }
        //        }

        //        print("alerady has this colors card");
        //        return Probability(eighty_twenty, ownable);
        //    }
        //}

        return Probability(ninty_ten, ownable);
    }

    public string Probability(int[] i, Ownable own)
    {
        int random = Random.Range(0, i.Length);
        switch (i[random])
        {
            case 1:
                colorSet.Add(own.colorName);    
                return "buy";
            case 2:
                return "skip";
            default:
                return null;
        }
    }
}
