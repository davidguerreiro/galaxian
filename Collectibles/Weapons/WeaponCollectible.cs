using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectible : MonoBehaviour
{
    [Header("Settings")]
    public string id;
    public float speed;
    public bool collectable;

    private AudioComponent _audio;
    private BoxCollider _boxCollider;
    private SpriteRenderer _renderer;
    private Animator _anim;

    private void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectable)
        {
            MoveDown();
        }
    }
    
    /// <summary>
    /// Move down collectable.
    /// </summary>
    public void MoveDown()
    {
        Vector2 current = transform.position;
        transform.position = new Vector2(current.x, current.y -= speed * Time.deltaTime);
    }

    /// <summary>
    /// Collect additional weapon collectible.
    /// </summary>
    /// <param name="playerCollided">GameObject</param>
    public void Collect(GameObject playerCollided)
    {
        collectable = false;

        _boxCollider.enabled = false;
        _anim.enabled = false;
        _renderer.sprite = null;

        _audio.PlaySound(0);

        PlayerCombat playerCombat = playerCollided.GetComponent<PlayerCombat>();
        playerCombat.AssignWeapon(id);

        Destroy(this.gameObject, 5f);
    }

    /// <summary>
    /// Spawn weapon collectable.
    /// </summary>
    public void Spawn()
    {
        _audio = GetComponent<AudioComponent>();
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        collectable = true;
    }

    /// <summary>
    /// On trigger enter collision.
    /// </summary>
    /// <param name="other">Collider</param>
    private void OnTriggerEnter(Collider other)
    {
        if (collectable)
        {
            if (other.CompareTag("Player"))
            {
                Collect(other.gameObject);
            }
        }

        if (other.CompareTag("RemoveItems"))
        {
            Destroy(gameObject);
        }
    }
}
