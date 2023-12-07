using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelUI : MonoBehaviour
{
    [Header("Components")]
    public FadeElement background;
    public GameObject titleOne;
    public GameObject titleTwo;
    public GameObject levelNumber;
    public Animator barLeft;
    public Animator barBottom;
    public TextComponent levelType;

    [Header("Settings")]
    public float toWaitIn;
    public float toWaitDisplayed;
    public float toWaitOut;

    [HideInInspector]
    public LevelData levelData;

    [HideInInspector]
    public Coroutine displayingUI;

    private Animator _titleOneAnim;
    private Animator _titleTwoAnim;
    private Animator _levelNumberAnim;
    private TextComponent _titleOneText;
    private TextComponent _titleTwoText;
    private TextComponent _levelNumberText;

    /// <summary>
    /// Init start UI dependencies.
    /// </summary>
    /// <param name="data">LevelData</param>
    public void InitDependencies(LevelData data)
    {
        levelData = data;

        _titleOneAnim = titleOne.GetComponent<Animator>();
        _titleTwoAnim = titleTwo.GetComponent<Animator>();
        _levelNumberAnim = levelNumber.GetComponent<Animator>();

        _titleOneText = titleOne.GetComponent<TextComponent>();
        _titleTwoText = titleTwo.GetComponent<TextComponent>();
        _levelNumberText = levelNumber.GetComponent<TextComponent>();
    }

    /// <summary>
    /// Write level data in the UI.
    /// </summary>
    private void WriteLevelData()
    {
        string[] levelName = levelData.displayName.Split(' ');
        string stageType = levelData.levelType == LevelData.LevelType.action ? "Action Stage" : "Bonus Stage";

        _titleOneText.UpdateContent(levelName[0]);
        _titleTwoText.UpdateContent(levelName[1]);

        _levelNumberText.UpdateContent("Level " + levelData.levelNumber.ToString());
        levelType.UpdateContent(stageType);
    }

    /// <summary>
    /// Display start UI.
    /// </summary>
    public void DisplayUI()
    {
        if (displayingUI == null)
        {
            displayingUI = StartCoroutine(DisplayUIRoutine());
        }
    }

    /// <summary>
    /// Display UI Coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DisplayUIRoutine()
    {
        WriteLevelData();

        yield return new WaitForSeconds(toWaitIn);

        _levelNumberAnim.SetBool("In", true);
        _titleOneAnim.SetBool("In", true);
        _titleTwoAnim.SetBool("In", true);
        barLeft.SetBool("In", true);
        barBottom.SetBool("In", true);

        yield return new WaitForSeconds(toWaitDisplayed);

        _levelNumberAnim.SetBool("Out", true);
        _titleOneAnim.SetBool("Out", true);
        _titleTwoAnim.SetBool("Out", true);
        barLeft.SetBool("Out", true);
        barBottom.SetBool("Out", true);

        yield return new WaitForSeconds(.5f);
        background.FadeOut();

        yield return new WaitForSeconds(toWaitOut);

        displayingUI = null;
    }

}
