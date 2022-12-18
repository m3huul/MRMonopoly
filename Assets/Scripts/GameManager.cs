using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void start()
    {
        StartCoroutine(SceneLoader.Instance.LoadYourAsyncScene("Monopoly Board"));
    }

}
