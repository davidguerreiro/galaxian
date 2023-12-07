using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundAnims : MonoBehaviour
{
    [Header("Components")]
    public GameObject deathAnim;

    private Animator _anim;
    private AudioComponent _audio;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="enemy">Enemy</param>
    public void Init()
    {
        _audio = GetComponent<AudioComponent>();
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Hit sound/anim.
    /// </summary>
    public void HitAnim()
    {
        _anim.SetTrigger("Hit");
        _audio.PlaySound(0);
    }

    /// <summary>
    /// Play enemy common battle sound.
    /// </summary>
    /// <param name="soundKey"></param>
    public void PlayBattleSound(int soundKey)
    {
        _audio.PlaySound(soundKey);
    }

    /// <summary>
    /// Death anim.
    /// </summary>
    public void DeathAnim()
    {
        deathAnim.SetActive(true);
    }
}
