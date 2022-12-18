using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    void Update()
    {
        
    }

    public IEnumerator LoadYourAsyncScene(string SceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(LoadYourAsyncScene("GameMenu"));
    }
}
