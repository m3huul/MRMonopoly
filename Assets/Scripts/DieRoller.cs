using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieRoller : MonoBehaviour
{
    public static DieRoller instance;

    private Vector3[] initialDiePositions;
    private Button GoBackButton;
    private int[] dieRollResults;

    void Awake()
    {
        instance = this;

        initialDiePositions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            initialDiePositions[i] = transform.GetChild(i).transform.position;
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public int[] GetDieRollResults()
    {
        return dieRollResults;
    }
    
    public void OnClickGoBack()
    {
        print("here");
        Gameplay.instance.Stop();
        StartCoroutine(SceneLoader.Instance.LoadYourAsyncScene("GameMenu"));
        Gameplay.instance.clearLists();
    }

    

    public IEnumerator RollDie()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            transform.GetChild(i).transform.position = initialDiePositions[i];
            transform.GetChild(i).transform.rotation = UnityEngine.Random.rotation;
        }

        // Let them start falling.  
        yield return new WaitForSeconds(2);
        
        while (true)
        {
            bool foundMovingDie = false;
            
            Rigidbody[] dieMovements = gameObject.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody dieMovement in dieMovements)
            {
                if (!dieMovement.IsSleeping())//(dieMovement.velocity.magnitude > .1f)
                {
                    foundMovingDie = true;
                    break;
                }
            }

            yield return null;

            if (!foundMovingDie)
                break;
        }

        // Determine die results.  
        Die[] dies = gameObject.GetComponentsInChildren<Die>();
        dieRollResults = new int[dies.Length];
        
        for (int i = 0; i < dies.Length; i++)
            dieRollResults[i] = dies[i].GetDieValue();
        
        Debug.Log("Got " + dieRollResults[0]+ " " + dieRollResults[1]);
    }
}
