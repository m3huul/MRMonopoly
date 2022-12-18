using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private GameObject gameViewTarget;
    [SerializeField] private GameObject[] CameraPositions;
    [SerializeField] private float time;


    void Awake()
    {
        instance = this;
        //StartCoroutine(WaitForCameraRotation(time));
    }

    public IEnumerator LerpToCameraViewTargets(Vector3 position, Vector3 eulerAngles, float totalTime)
    {
        float startTime = Time.time;
        Vector3 initialPosition = transform.position;
        Vector3 initialRotation = transform.eulerAngles;

        float progressionCoefficient = 0f;
        while (progressionCoefficient < .98f)
        {
            progressionCoefficient = (Time.time - startTime) / totalTime;

            transform.position = Vector3.Lerp(initialPosition, position, progressionCoefficient);
            transform.eulerAngles = Vector3.Lerp(initialRotation, eulerAngles, progressionCoefficient);

            yield return null;
        }

        transform.position = position;
        transform.eulerAngles = eulerAngles;
    }

    public IEnumerator LerpToViewBoardTarget(float totalTime)
    {
        yield return LerpToCameraViewTargets(gameViewTarget.transform.position, gameViewTarget.transform.eulerAngles, totalTime);
    }

    public IEnumerator WaitForCameraRotation(float time)
    {
        float startTime = Time.time;
        Vector3 initialRotation = transform.parent.eulerAngles;
        Vector3 eularAngle = new Vector3(0f, 360f, 0f);

        float Coefficient = 0f;
        while (Coefficient < 0.98f)
        {
            Coefficient = (Time.time - startTime) / time;

            
            transform.parent.eulerAngles = Vector3.Lerp(initialRotation, eularAngle, Coefficient);
            yield return null;
        }

        yield return new WaitForSeconds(.2f);
        StartCoroutine(LerpToViewBoardTarget(2f));
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
