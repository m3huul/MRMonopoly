using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
//using static UnityEditor.ShaderData;
using System;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public List<Inventory> myInventory = new List<Inventory>();


    public Animator PlayMovementAnim;
    [SerializeField] public List<Transform> Posi = new List<Transform>();
    //public bool moveAllowed;
    public float speed;
    public float duration;

    public int Waypoint;
    
    public int currentPosiIndex = 0;   //10 steps count
    //public uiElements ui;
    private void Start()
    {
    }

    public void AddItemToInventory(Inventory inventoryItem)
    {
        
    }

    public void StartMove(int i)
    {
        Calc(i);
    }

    public void Calc(int i)
    {
        if(currentPosiIndex + i > Posi.Count)
        {
            Waypoint = Posi.Count - currentPosiIndex + i;
        }
        else
        {
            Waypoint = currentPosiIndex + i;    
        }
        Platforms.instance.stateCheck(Waypoint);
        StartCoroutine(MoveSpaces(i));

    }

    public IEnumerator MoveSpaces(int spaces)
    {
        bool movingForward = spaces > 0;
        spaces = Math.Abs(spaces);

        for (int i = 0; i < spaces; i++)
        {
            currentPosiIndex++;
            currentPosiIndex %= Posi.Count;
            Transform targetSpace = movingForward ? Posi[currentPosiIndex] : Posi[currentPosiIndex-1];

            //currentSpace.PassBy(this);

            float timeForJump = .9f * (Mathf.Sqrt((i * 1.0f) / spaces + .8f) - .35f);

            yield return JumpToSpace(targetSpace, timeForJump);

            if(currentPosiIndex == 10 || currentPosiIndex == 20 || currentPosiIndex == 30 || currentPosiIndex == 0)
            {
                yield return RotateAdditionalDegrees(movingForward ? 90 : -90, 1f);
            }
        }
        Platforms.instance.ShowCard();
    }

    public IEnumerator JumpToSpace(/*BoardLocation space*/Transform space, float timeForJump)
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
        //currentSpace = space;
        //transform.position = space.position;
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

    //Run another func to return acive waypoints gameobject
    //    public IEnumerator Move(int finalDestination)
    //{

    //    PlayMovementAnim = GetComponentInChildren<Animator>();
    //    while (finalDestination>0)
    //    {
    //        steps++;
    //        steps %= Posi.Count;

    //        StartCoroutine(Lerping(steps));
    //        yield return new WaitForSeconds(2f);
    //        journey = 0;

    //        finalDestination--;

    //    }
    //    Monopoly.instance.coroutineAllowed = true;
    //    //OnReachingDestination();
    //    //Platforms.instance.ShowCard();
    //}

    //public IEnumerator Lerping(int i)
    //{
    //    Vector3 startingPosition = transform.position;
    //    while (journey <= duration)
    //    {
    //        journey += Time.deltaTime*speed;
    //        PlayMovementAnim.SetBool("Jump", true);
    //        if (journey > 0.8f)
    //        {
    //            PlayMovementAnim.SetBool("Jump", false);
    //        }
    //        transform.position = Vector3.Lerp(startingPosition, Posi[i].position, journey);

    //        yield return null;
    //    }

    //}

    public void OnReachingDestination()
    {
        RaycastHit Hit;
        if(Physics.Raycast(transform.position, -transform.up, out Hit))
        {
            //Platforms.instance.ShowCard(Hit.transform.GetComponent<Platform>().state, Hit.transform.GetComponent<Platform>().PropertyDetails);

        }
    }

}

            //for(int i = 0; i < 2; i++)
            //{
            //    //if (GameMode.instance.players[i].GetComponent<PlayerMovement>().inventory.Tiles.Contains(new KeyValuePair<int, string>(Hit.collider.GetComponent<Platform>().cardNo, Hit.collider.GetComponent<Platform>().state.ToString())))
            //    //{
            //    //    Debug.Log("a player alerady has this card");
            //    //    break;
            //    //}
            //}
            //Hit.collider.GetComponent<Platform>().ShowCard();