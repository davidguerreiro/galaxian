using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite normal;
    public Sprite movingRight;
    public Sprite movingLeft;

    [Header("Animation Components")]
    public GameObject destroyAnim;
    public GameObject propulsion;
    public GameObject shield;

    private PlayerController _playerController;
    private PlayerCombat _playerCombat;
    private SpriteRenderer _renderer;
    private Animator _anim;
    private AudioComponent _audio;
    private Coroutine _inDashAnim;
    private Coroutine _hitAnim;
    private bool _showEndLevelAnim;

    // Update is called once per frame
    void Update()
    {
        /*
        if (_playerController.canMove && _playerController.isMoving)
        {
            UpdatePlayerMovementSprite();
        } else
        {
            UpdatePlayerMovementSprite(true);
        }
        */

        if (_playerController.isMoving)
        {
            UpdateDashAnimSound();
        }

        if (_showEndLevelAnim)
        {
            ShipLeavesLevel();
        }
    }

    /// <summary>
    /// Update player sprite based on movement.
    /// </summary>
    /// <param name="forceNormal">bool</param>
    private void UpdatePlayerMovementSprite(bool forceNormal = false)
    {
        if (forceNormal)
        {
            _renderer.sprite = normal;
        } else
        {
            if (_playerController.isMovingRight)
            {
                _renderer.sprite = movingRight;
            }
            else if (_playerController.isMovingLeft)
            {
                _renderer.sprite = movingLeft;
            }
            else
            {
                _renderer.sprite = normal;
            }
        }
        
    }

    /// <summary>
    /// Update dash anim sound.
    /// </summary>
    private void UpdateDashAnimSound()
    {
        if (_playerController.inDash && _inDashAnim == null)
        {
            _inDashAnim = StartCoroutine(UpdateDashAnimSoundRoutine());
        }
    }

    /// <summary>
    /// Update dash sound/anim routine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator UpdateDashAnimSoundRoutine()
    {
        _audio.PlaySound(0);

        if (_playerController.isMovingRight)
        {
            _renderer.sprite = movingRight;
        }
        else if (_playerController.isMovingLeft)
        {
            _renderer.sprite = movingLeft;
        }
        else
        {
            _renderer.sprite = normal;
        }

        while (_playerController.inDash)
        {
            yield return new WaitForFixedUpdate();
        }

        _renderer.sprite = normal;

        while (_playerController.dashRoutine != null)
        {
            yield return new WaitForFixedUpdate();
        }

        _inDashAnim = null;
    }

    /// <summary>
    /// Hit sound/animation.
    /// </summary>
    public void HitAnim()
    {
        if (_hitAnim == null)
        {
            _hitAnim = StartCoroutine(HitAnimRoutine());
        }
    }

    /// <summary>
    /// Hit anim coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator HitAnimRoutine()
    {
        _anim.SetBool("Hit", true);
        _audio.PlaySound(1);

        yield return new WaitForSeconds(_playerCombat.invencibleDuration);

        _anim.SetBool("Hit", false);

        _hitAnim = null;
    }

    /// <summary>
    /// Destroy animation wrapper.
    /// </summary>
    public void DestroyAnim()
    {
        StartCoroutine(DestroyAnimRoutine());
    }

    /// <summary>
    /// Destroy anim Coroutine.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator DestroyAnimRoutine()
    {
        destroyAnim.SetActive(true);
        yield return new WaitForSeconds(.5f);

        destroyAnim.SetActive(false);

        yield return new WaitForSeconds(3.5f);
    }

    /// <summary>
    /// Set sprite to normal.
    /// </summary>
    public void SetSpriteToNormal()
    {
        _renderer.sprite = normal;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 1f);
        _anim.enabled = true;
        propulsion.SetActive(true);
    }

    /// <summary>
    /// Remove normal sprite.
    /// Usually called when player loses a life.
    /// </summary>
    public void RemoveNormalSprite()
    {
        _anim.enabled = false;
        _renderer.sprite = null;
        propulsion.SetActive(false);
    }

    /// <summary>
    /// Enable shield.
    /// </summary>
    public void EnableShield()
    {
        shield.SetActive(true);
        _audio.PlaySound(3);
    }

    /// <summary>
    /// Disable shield.
    /// </summary>
    public void DisableShield()
    {
        shield.SetActive(false);
    }

    /// <summary>
    /// Display end level anim for player coroutine.
    /// </summary>
    public void DisplayEndLevelAnim()
    {
        _playerController.StopMovement();
        _playerCombat.DisableShield();
        _playerCombat.RestrictShooting();

        _showEndLevelAnim = true;
    }

    /// <summary>
    /// Ship leaves level animation.
    /// </summary>
    private void ShipLeavesLevel()
    {
        Vector2 current = transform.position;
        transform.position = new Vector2(current.x, current.y += _playerController.speed * Time.deltaTime);
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="playerController">PlayerController</param>
    /// <param name="playerCombat">PlayerCombat</param>
    public void Init(PlayerController playerController, PlayerCombat playerCombat)
    {
        _playerController = playerController;
        _playerCombat = playerCombat;
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioComponent>();
        _inDashAnim = null;
    }
}
