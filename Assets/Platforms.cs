using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Platforms : MonoBehaviour
{
    public static Platforms instance;
    public List<GameObject> Platform;
    public GameObject propertyCard, communityChestCard;
    public GameMode.PlatformState platformState;
    public List<GameObject> propertyDetails;
    public GameObject communityChestDetails;

    int communityChestCardIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    public void stateCheck(int activeWaypoint)
    {
        platformState = Platform[activeWaypoint].GetComponent<Platform>().state;
        switch (platformState)
        {
            case GameMode.PlatformState.Property:
                plat p = propertyDetails[0].GetComponent<plat>();
                Platform[activeWaypoint].GetComponent<Platform>().propertyId = p.propertyId; 
                propertyCard.GetComponent<CardDisplay>().card(p.propertyId, p.Name, p.price, p.mortgage, p.rent, "", p.house1, p.house2, p.house3, p.house4, p.hotel, p.price_per_house, p.hotel);
                break;
            case GameMode.PlatformState.CommunityChest:
                //CommunityChest c = communityChestDetails.GetComponent<CommunityChest>();
                //KeyValuePair<int, Sprite> Cchest = c.RandomSprite("Community");
                //communityChestCard.transform.GetChild(0).GetComponent<Image>().sprite = Cchest.Value;
                //communityChestCardIndex = Cchest.Key;
                break;
        }
    }

    public void ShowCard() //Need to change this to get called when player lands on da platform
    {
        switch (platformState)
        {
            case GameMode.PlatformState.Property:
                //propertyCard.GetComponent<CardDisplay>().TitleText.text = details.GetComponent<plat>().Name;
                propertyCard.SetActive(true);
                break;
            case GameMode.PlatformState.CommunityChest:
                communityChestCard.SetActive(true);
                StartCoroutine(GameMode.instance.DoCommunityChestAction(communityChestCardIndex));
                //details.GetComponent<CommunityChest>().RandomSprite("Community");
                break;
            case GameMode.PlatformState.Chance:
                //details.GetComponent<CommunityChest>().RandomSprite("Chance");
                break;

        }
    }
}
