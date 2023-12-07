using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenUI : MonoBehaviour
{
    [Header("Components")]
    public FadeElement overlay;

    [Header("Settings")]
    public float secondsBefore;
    public float secondsIn;
    public float secondsAfter;
    public string sceneNameToLoad;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TriggerUILogic());   
    }

    /// <summary>
    /// UI logic animation trigger.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator TriggerUILogic()
    {
        yield return new WaitForSeconds(secondsBefore);

        overlay.FadeOut();
        yield return new WaitForSeconds(secondsIn);

        overlay.FadeIn();
        yield return new WaitForSeconds(secondsAfter);

        LoadNextScene();
    }

    /// <summary>
    /// Load next scene after animation is finished.
    /// </summary>
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
