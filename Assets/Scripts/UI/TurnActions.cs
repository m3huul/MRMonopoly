using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnActions : MonoBehaviour
{
    public static TurnActions instance;

    public Ownable TradeOwnable;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] private Text rollText;
    
    public enum UserAction {ROLL, TRADE, MORTGAGE, BUILD, UNDECIDED}
    private UserAction chosenAction = UserAction.UNDECIDED;
    private UserAction chosenActionMortgage = UserAction.UNDECIDED;
    private UserAction chosenActionTrade = UserAction.UNDECIDED;
    public UserAction GetChosenAction()
    {
        return chosenAction;
    }

    public IEnumerator GetUserInput(bool enableRoll)
    {
        chosenAction = UserAction.UNDECIDED;
        
        transform.GetChild(0).gameObject.SetActive(true);
        rollText.text = enableRoll ? "ROLL" : "End Turn";

        while (chosenAction == UserAction.UNDECIDED)
            yield return null;
        
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public UserAction GetChosenActionnMortgage()
    {
        return chosenActionMortgage;
    }

    public IEnumerator GetUserInputMortgage(Player player)
    {
        chosenActionMortgage = UserAction.UNDECIDED;

        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

        while (chosenActionMortgage == UserAction.UNDECIDED)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    Debug.Log(hit.transform.name);
                    foreach(Ownable own in player.currentOwnables)
                    {
                        if(hit.collider.GetComponent<Ownable>() == own && own.isMortgaged == false)
                        {
                            own.isMortgaged = true;
                            own.reset();
                            player.AdjustBalanceBy(+own.mortgageValue);
                        }
                        else if(hit.collider.GetComponent<Ownable>()== own && own.isMortgaged == true)
                        {
                            own.isMortgaged = false;
                            own.ShowUnMortgagedProperty();
                            player.AdjustBalanceBy(-own.mortgageValue);
                        }
                    }
                }
            }

            yield return null;
        }
            

        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
    }

    public UserAction GetChosenActionTrade()
    {
        return chosenActionTrade;
    }

    public IEnumerator GetUserInputTrade(List<Player> otherPlayers, Player player)
    {
        chosenActionTrade = UserAction.UNDECIDED;

        transform.GetChild(0).gameObject.SetActive(true); //TURNING ON PANEL
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false); //TURNING roll button off
        
        while(chosenActionTrade == UserAction.UNDECIDED)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100))
                {
                    Debug.Log(hit.transform.name);
                    if (!TradeOwnable)
                    {
                        foreach (Ownable Iown in player.currentOwnables)
                        {
                            if (hit.collider.GetComponent<Ownable>() == Iown)
                            {
                                TradeOwnable=Iown;
                                foreach (Ownable ownable1 in player.currentOwnables)
                                {
                                    if (Iown != ownable1)
                                    {
                                        ownable1.reset();
                                    }
                                }
                                foreach (Player p in otherPlayers)
                                {
                                    foreach (Ownable ownable in p.currentOwnables)
                                    {
                                        ownable.ShowOwnable();
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        foreach (Player p in otherPlayers)
                        {
                            foreach (Ownable ownable in p.currentOwnables)
                            {
                                if (hit.collider.GetComponent<Ownable>() == ownable)
                                {
                                    StartCoroutine(ChoiceAlert.instance.CreateChoiceAlert(player.name + " wants to trade " + TradeOwnable.propertyName + " with " + ownable.propertyName, Color.green, "Accept", Color.red, "Decline"));
                                    while (!ChoiceAlert.instance.decisionMade)
                                    {
                                        yield return null;
                                    }
                                    if (ChoiceAlert.instance.resultingDecision)
                                    {
                                        player.currentOwnables.Add(ownable);
                                        ownable.OwnerColorIndicator.material = player.playerColor;
                                        p.currentOwnables.Add(TradeOwnable);
                                        TradeOwnable.OwnerColorIndicator.material = p.playerColor;

                                        player.currentOwnables.Remove(TradeOwnable);
                                        p.currentOwnables.Remove(ownable); //add if mortgage property is traded

                                        chosenAction = UserAction.TRADE;
                                        TradeOwnable = null;
                                        break;
                                    }
                                    else
                                    {
                                        chosenAction = UserAction.TRADE;
                                        TradeOwnable = null;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            yield return null; 
        }

    }

    public void Roll()
    {
        chosenAction = UserAction.ROLL;
    }

    public void Trade()
    {
        chosenAction = UserAction.TRADE;
        chosenActionTrade = UserAction.TRADE;
    }

    public void Mortgage()
    {
        chosenAction = UserAction.MORTGAGE;
        chosenActionMortgage = UserAction.MORTGAGE;
    }

    public void Build()
    {
        chosenAction = UserAction.BUILD;
    }
}
