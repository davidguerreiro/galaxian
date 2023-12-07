using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("Status")]
    public bool canMove;
    public bool isMoving;
    public bool isMovingRight;
    public bool isMovingLeft;
    public bool isMovingUp;
    public bool isMovingDown;
    public bool inDash;

    [Header("Dash")]
    public float dashPotency;
    public float dashDuration;
    public float dashCooling;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public Vector2 movement = new Vector2();

    [HideInInspector]
    public Coroutine dashRoutine;

    private Rewired.Player _playerInput;
    private CharacterController _controller;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            UpdateMovementState();
            CheckForDash();
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (! inDash) { 
                MovePlayer();
            }
        }
    }

    /// <summary>
    /// Move player based on input.
    /// </summary>
    public void MovePlayer()
    {
        movement.x = _playerInput.GetAxis("MoveHorizontal") * speed;
        movement.y = _playerInput.GetAxis("MoveVertical") * speed;

        movement = Vector2.ClampMagnitude(movement, speed);

        _controller.Move(movement * Time.deltaTime);
    }


    /// <summary>
    /// Update player state.
    /// </summary>
    private void UpdateMovementState()
    {
        isMoving = (movement.magnitude > 0f) ? true : false;
        isMovingRight = (movement.x > 0f) ? true : false;
        isMovingLeft = (movement.x < 0f) ? true : false;
        isMovingUp = (movement.y > 0f) ? true : false;
        isMovingDown = (movement.y < 0f) ? true : false;
    }

    /// <summary>
    /// Check for dash mechacnic.
    /// </summary>
    private void CheckForDash()
    {
        if (_playerInput.GetButtonDown("Dash"))
        {
            Dash();
        }
    }

    /// <summary>
    /// Perform dash mechanic.
    /// </summary>
    public void Dash()
    {
        if (dashRoutine == null)
        {  
            dashRoutine = StartCoroutine(DashRoutine());
        }
    }

    /// <summary>
    /// Dash routine.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>IEnumerator</returns>
    public IEnumerator DashRoutine()
    {
        inDash = true;

        canMove = false;
        int counter = 0;
        float dashLogic = dashPotency * speed;

        movement = Vector2.zero;

        Vector2 dashMovement = new Vector2();

        // calculate dash direction logic.
        if (isMovingRight)
        {
            dashMovement.x = dashLogic;
        } else if (isMovingLeft)
        {
            dashMovement.x = dashLogic * -1;
        } else
        {
            dashMovement.x = 0f;
        }

        if (isMovingUp)
        {
            dashMovement.y = dashLogic;
        } else if (isMovingDown)
        {
            dashMovement.y = dashLogic * -1;
        } else
        {
            dashMovement.y = 0f;
        }
        

        dashMovement = Vector2.ClampMagnitude(dashMovement, speed) * dashPotency;

        while (counter <= dashDuration)
        {
            _controller.Move(dashMovement * Time.deltaTime);
            counter++;

            yield return new WaitForFixedUpdate();
        }

        canMove = true;
        inDash = false;
        yield return new WaitForSeconds(dashCooling);

        dashRoutine = null;
    }

    /// <summary>
    /// Allow player to control the ship.
    /// </summary>
    public void AllowMovement()
    {
        canMove = true;
        _controller.enabled = true;
    }

    /// <summary>
    /// Stop player ship controlling.
    /// </summary>
    public void StopMovement()
    {
        canMove = false;
        _controller.enabled = false;
    }
    
    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="speed">int</param>
    public void Init(float speed)
    {
        this.speed = speed;
        _controller = GetComponent<CharacterController>();
        _playerInput = ReInput.players.GetPlayer(0);
        dashRoutine = null;
    }
}
