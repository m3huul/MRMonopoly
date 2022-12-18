using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    //public Monopoly mon;
    [SerializeField] public GameObject[] players;
    public int maxPlayers;
    public int numberOfPlayers;
    public int playerIndex;
    public int[] playerMoney;


    public enum PlatformState
    {
        Corners,
        Property,
        Chance,
        CommunityChest,
        Pay,
        RailRoad,
    }
    
    private void Awake()
    {
        //Monopoly.instance.nextTurn += NextTurn;
        instance = this;
    }


    public void MovePlayer(int Destination)
    {
        players[playerIndex].GetComponent<PlayerMovement>().StartMove(Destination);
    }


    //numberOfPlayers < playerIndex
    public void NextTurn()
    {
        if (playerIndex > maxPlayers-1)
        {
            playerIndex = 0;
        }
        if (numberOfPlayers-1 >= playerIndex)
        {
            print(playerIndex+1 +" : player");
        }
        else
        {
            print("bot");
            
        }
    }

    public void amountDeducted(int x)
    {
        playerMoney[playerIndex] -= x;
    }

    public void amountAdded(int x)
    {
        playerMoney[playerIndex] += x;
    }

    public IEnumerator DoCommunityChestAction(int i)
    {
        yield return new WaitForSeconds(5f);

        //mon.CommunityChestCard.SetActive(false);
        
        switch (i)
        {
            case 0:
                break;
            case 1:
                amountAdded(200);
                break;
            default:
                break;
        }
        yield break;
    }

    //public void WhatToDo(PlatformState state, int cardNo)
    //{
    //    switch (state)
    //    {
    //        case GameMode.PlatformState.PurpleProperty:
    //            mon.ShowPurplePropertyCard(cardNo);
    //            break;
    //        case GameMode.PlatformState.LightBlueProperty:
    //            Debug.Log("LightBlueProperty");
    //            Monopoly.instance.ShowLightBluePropertyCard(cardNo);
    //            //Monopoly.instance.Next(/*Monopoly.action.moveAhead*/);
    //            break;
    //        case GameMode.PlatformState.DarkBlueProperty:
    //            Debug.Log("DarkBlueProperty");
    //            mon.ShowDarkBlueCard(cardNo);
    //            break;
    //        case GameMode.PlatformState.OrangeProperty:
    //            Debug.Log("OrangeProperty");
    //            Monopoly.instance.Next();
    //            break;
    //        case GameMode.PlatformState.PinkProperty:
    //            Debug.Log("PinkProperty");
    //            Monopoly.instance.Next();
    //            break;
    //        case GameMode.PlatformState.RedProperty:
    //            Debug.Log("RedProperty");
    //            Monopoly.instance.Next();
    //            break;
    //        case GameMode.PlatformState.YellowProperty:
    //            Debug.Log("YellowProperty");
    //            Monopoly.instance.Next();
    //            break;
    //        case GameMode.PlatformState.GreenProperty:
    //            Debug.Log("GreenProperty");
    //            Monopoly.instance.Next();
    //            break;
    //        case GameMode.PlatformState.StartPoint:
    //            Monopoly.instance.Next();
    //            Debug.Log("StartPoint");
    //            break;
    //        case GameMode.PlatformState.CommunityChest:
    //            Monopoly.instance.Next();
    //            Debug.Log("CommunityChest");
    //            break;
    //        case GameMode.PlatformState.Pay:
    //            mon.ShowPayCard(cardNo);
    //            break;
    //        case GameMode.PlatformState.RailRoad:
    //            Monopoly.instance.Next();
    //            Debug.Log("RailRoad");
    //            break;
    //        case GameMode.PlatformState.Chance:
    //            Monopoly.instance.Next();
    //            Debug.Log("Chance");
    //            break;
    //        case GameMode.PlatformState.FreeParking:
    //            Monopoly.instance.Next();
    //            Debug.Log("FreeParking");
    //            break;
    //        case GameMode.PlatformState.GoToJail:
    //            Debug.Log("GoToJail");
    //            Monopoly.instance.Next();
    //            break;
    //    }
    //}
}
