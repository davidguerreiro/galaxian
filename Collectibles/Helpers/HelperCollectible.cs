using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperCollectible : MonoBehaviour
{
    [Header("Settings")]
    public string id;
    public float speed;
    public bool collectable;

    private AudioComponent _audio;
    private BoxCollider _boxCollider;
    private SpriteRenderer _renderer;
    private Animator _anim;

    private ExtraLifeCollectable _extraLifeCollectible;
    private ExtraEnergyCollectable _extraEnergyCollectible;
    private ExtraRocketsCollectible _extraRocketsCollectible;
    private ExtraScoreCollectible _extraScoreCollectible;
    private ShieldCollectible _shieldCollectible;

    // Start is called before the first frame update
    void Start()
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
    /// Collect additional weapon collectible.
    /// </summary>
    /// <param name="playerCollided">GameObject</param>
    public void Collect(GameObject playerCollided)
    {
        collectable = false;

        _boxCollider.enabled = false;
        _anim.enabled = false;
        _renderer.sprite = null;

        switch (id)
        {
            case "extraLife":
                _extraLifeCollectible = GetComponent<ExtraLifeCollectable>();
                _extraLifeCollectible.Collect(playerCollided, _audio);
                break;
            case "extraEnergy":
                _extraEnergyCollectible = GetComponent<ExtraEnergyCollectable>();
                _extraEnergyCollectible.Collect(playerCollided, _audio);
                break;
            case "extraRockets":
                _extraRocketsCollectible = GetComponent<ExtraRocketsCollectible>();
                _extraRocketsCollectible.Collect(playerCollided, _audio);
                break;
            case "extraScore":
                _extraScoreCollectible = GetComponent<ExtraScoreCollectible>();
                _extraScoreCollectible.Collect(playerCollided, _audio);
                break;
            case "shield":
                _shieldCollectible = GetComponent<ShieldCollectible>();
                _shieldCollectible.Collect(playerCollided);
                break;
            default:
                break;
        }

        Destroy(this, 5f);
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
