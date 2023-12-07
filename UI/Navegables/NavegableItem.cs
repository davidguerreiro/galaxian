using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;

public class NavegableItem : MonoBehaviour
{
    public bool focus;

    [Header("Settings")]
    public float threshold;

    [Header("Navegables")]
    public NavegableItem up;
    public NavegableItem down;
    public NavegableItem right;
    public NavegableItem left;

    [Header("Events")]
    public UnityEvent onFocus;
    public UnityEvent lostFocus;
    public UnityEvent selected;

    private Rewired.Player _playerInput;

    // Update is called once per frame
    void Update()
    {
        if (focus && Navegable.navegableRoutine == null)
        {
            ListenForUserInput(); 
        }
    }

    /// <summary>
    /// Listen for user input.
    /// </summary>
    private void ListenForUserInput()
    {
        if (_playerInput.GetAxis("MoveVertical") > 0)
        {
            Navegable.navegableRoutine = StartCoroutine(NavigateUp());
        } else if (_playerInput.GetAxis("MoveVertical") < 0)
        {
            Navegable.navegableRoutine = StartCoroutine(NavigateDown());
        }

        else if (_playerInput.GetAxis("MoveHorizontal") > 0)
        {
            Navegable.navegableRoutine = StartCoroutine(NavigateRight());
        }

        else if (_playerInput.GetAxis("MoveHorizontal") < 0)
        {
            Navegable.navegableRoutine = StartCoroutine(NavigateLeft());
        }

        if (focus && _playerInput.GetButtonDown("AfirmativeAction"))
        {
            Selected();
        }
    }

    /// <summary>
    /// Logic performed when navegable gets the focus.
    /// </summary>
    public void GetFocus()
    {
        focus = true;
        onFocus?.Invoke();
    }

    /// <summary>
    /// Logic when lost focus.
    /// </summary>
    public void LostFocus()
    {
        focus = false;
        lostFocus?.Invoke();
    }

    /// <summary>
    /// Logic when navigating up.
    /// </summary>
    public IEnumerator NavigateUp()
    {
        if (up != null)
        {
            LostFocus();
            up.GetFocus();

            yield return new WaitForSecondsRealtime(threshold);
        }

        Navegable.SetNavigationRoutineToNull();
    }

    /// <summary>
    /// Logic when navigating down.
    /// </summary>
    public IEnumerator NavigateDown()
    {
        if (down != null)
        {
            LostFocus();
            down.GetFocus();

            yield return new WaitForSecondsRealtime(threshold);
        }

        Navegable.SetNavigationRoutineToNull();
    }

    /// <summary>
    /// Logic when navigating right.
    /// </summary>
    public IEnumerator NavigateRight()
    {
        if (right != null)
        {
            LostFocus();
            right.GetFocus();

            yield return new WaitForSecondsRealtime(threshold);
        }

        Navegable.SetNavigationRoutineToNull();
    }

    /// <summary>
    /// Logic when navigating left.
    /// </summary>
    public IEnumerator NavigateLeft()
    {
        if (left != null)
        {
            LostFocus();
            left.GetFocus();

            yield return new WaitForSecondsRealtime(threshold);
        }

        Navegable.SetNavigationRoutineToNull();
    }

    /// <summary>
    /// Logic when selected.
    /// </summary>
    public void Selected()
    {
        selected?.Invoke();
        focus = false;
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    public void Init()
    {
        _playerInput = ReInput.players.GetPlayer(0);
    }
}
