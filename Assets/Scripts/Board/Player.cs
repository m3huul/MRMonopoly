using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour 
{
    private static int instantiatedPlayers = 0;
    
    /*[HideInInspector]*/ public List<Ownable> currentOwnables = new List<Ownable>();
    
    private string playerName;

    public Material playerColor;

    public AI ai;

    private BalanceTracker balanceTracker;

    private BoardLocation currentSpace;

    public bool isInJail;

    // Incremented by PassGo.  
    public int timesPastGo = 0;

    private bool isAI = false;

    public void SetPlayerName(string playerName)
    {
        this.playerName = playerName;
        gameObject.name = playerName;
    }

    public void SetIsAI(bool isAI)
    {
        this.isAI = isAI;
        if (isAI)
            ai = gameObject.AddComponent<AI>();

    }
    public bool IsAI()
    {
        return isAI;
    }

    public void SetBalanceTracker(BalanceTracker balanceTracker)
    {
        this.balanceTracker = balanceTracker;
        
        this.balanceTracker.UpdateName(playerName);

    }
    
    // Money
    private int accountBalance = 1500;

    public bool hasJailFreeCard;

    public void AdjustBalanceBy(int balance)
    {
        if (balance < 0 && accountBalance - balance < 0)
        {
            List<int> mortgageValues = new List<int>();
            foreach (Ownable ownable in currentOwnables)
            {
                mortgageValues.Add(ownable.mortgageValue);
            }

            mortgageValues = mortgageValues.OrderBy(i => i).ToList();
            foreach (int value in mortgageValues)
            {
                //todo show all the properties player can mortgage and then a flow so he can mortgage them.
            }
        }
        

        accountBalance += balance;
        balanceTracker.UpdateBalance(accountBalance);
    }

    public int GetBalance()
    {
        return accountBalance;
    }
    

    public void Initialize()
    {
        //transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = playerColors[instantiatedPlayers];
        currentSpace = PassGo.instance;
        instantiatedPlayers++;
    }

    public IEnumerator RotateAdditionalDegrees(float additionalDegrees, float timeForRotate)
    {
        float progressionCoefficient = 0;
        float startTime = Time.time;
        float startAngle = transform.eulerAngles.y;
                
        while (progressionCoefficient <= .98f)
        {
            progressionCoefficient = (Time.time - startTime) / timeForRotate;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + additionalDegrees * progressionCoefficient, transform.eulerAngles.z);

            yield return null;
        }
                
        // Finalize rotation.  
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, startAngle + additionalDegrees, transform.eulerAngles.z);
    }

    public IEnumerator JumpToSpace(BoardLocation space, float timeForJump)
    {
        float startTime = Time.time;

        Vector3 startPosition = transform.position; // not current space position because we might start abnormally.  
        Vector3 endPosition = space.gameObject.transform.position;

        Vector3 desiredDisplacement = endPosition - startPosition;
        desiredDisplacement.y = 0;

        float progressionCoefficient = 0;
        while (progressionCoefficient <= .98f)
        {
            progressionCoefficient = (Time.time - startTime) / timeForJump;
                
            Vector3 newPosition = startPosition + desiredDisplacement * progressionCoefficient;
            newPosition.y = -1 * Mathf.Pow(progressionCoefficient - 0.5f, 2) + 0.25f;

            transform.position = newPosition;

            yield return null;
        }
            
        // Onto the next space!
        currentSpace = space;
        transform.position = currentSpace.transform.position;
    }
    
    public IEnumerator MoveSpaces(int spaces)
    {   
        bool movingForward = spaces > 0;
        spaces = Math.Abs(spaces);

        for (int i = 0; i < spaces; i++)
        {
            BoardLocation targetSpace = movingForward ? currentSpace.next : currentSpace.preceding;
            
            currentSpace.PassBy(this);
            
            float timeForJump = .9f * (Mathf.Sqrt((i * 1.0f) / spaces + .8f) - .35f);

            transform.LookAt(currentSpace.next.transform);

            yield return JumpToSpace(targetSpace, timeForJump);
            
            // Rotate if we're on a corner space.  
            if (currentSpace is PassGo || currentSpace is GoToJail || currentSpace is InJail ||
                currentSpace is FreeParking)
            {
                yield return RotateAdditionalDegrees(movingForward ? 90 : -90, 1f);
                //transform.LookAt(currentSpace.next.transform.position);
            }
        }
        
        // Tell the space we ended on that we landed on it.  
        yield return currentSpace.LandOn(this);
    }


}
