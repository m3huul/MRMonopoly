using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameMode.PlatformState state;
    public GameObject PropertyDetails;
    public GameObject CommunityChest;
    public GameObject Railroads;

    public int propertyId;

    [Tooltip("Index of the player who owns this platform")]
    public int playerIndex;

    public bool Owned = false;


}
