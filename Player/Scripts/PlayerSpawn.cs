using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [Header("Components")]
    public Transform wrapper;
    public Transform init;
    public Transform target;

    [HideInInspector]
    public Player player;

    [HideInInspector]
    public Coroutine spawnPlayer;

    /// <summary>
    /// Spawn player in game scene.
    /// </summary>
    public void Spawn()
    {
        if (spawnPlayer == null)
        {
            spawnPlayer = StartCoroutine(PlayerSpawnRoutine());
        }
    }

    /// <summary>
    /// Spawn player animation.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlayerSpawnRoutine()
    {
        gameObject.transform.parent = wrapper;
        gameObject.transform.localPosition = init.localPosition;

        player.playerAnimations.SetSpriteToNormal();

        while (Vector3.Distance(transform.localPosition, target.localPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, target.localPosition, player.speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        transform.localPosition = target.localPosition;
        gameObject.transform.parent = null;

        spawnPlayer = null;
    }

    /// <summary>
    /// Spawn player consumer.
    /// </summary>
    /// <param name="toWaitAfter"></param>
    /// <returns></returns>
    public IEnumerator Respawn(float toWaitAfter = 1.5f)
    {
        yield return new WaitForSeconds(toWaitAfter);

        // Spawn player.
        Spawn();
        player.RestoreHealth();

        while (player.playerSpawn.spawnPlayer != null)
        {
            yield return new WaitForFixedUpdate();
        }

        // make player playable.
        player.playerController.AllowMovement();
        player.playerCombat.AllowShooting();
        player.playerCombat.RemoveInvencible();
        player.playerCombat.RestoreWeapons();
    }

    /// <summary>
    /// Init class method.
    /// </summary>
    /// <param name="player">Player</param>
    public void Init(Player player)
    {
        this.player = player;
    }
}
