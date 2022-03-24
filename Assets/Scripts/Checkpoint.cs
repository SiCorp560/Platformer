using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // The sprite renderer component which controls the checkpoint sprite
    public SpriteRenderer sprite;

    // Marks the final checkpoint at the end of the level (set in editor)
    public bool finalCheckpoint;

    // Used to prevent double-activating a checkpoint
    public bool active = false;

    // Particle prefab
    public GameObject particlePrefab;

    private void Start()
    {
        if (sprite == null)
        {
            Debug.LogError("Checkpoint doesn't have sprite renderer component.");
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !active)
        {
            active = true;
            GameObject particles = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);

            // Tell GameManager that player reached a checkpoint
            GameManager.S.TriggerCheckpoint(transform, finalCheckpoint);

            if (PlayerFlyController.player != null)
                PlayerFlyController.player.IncreaseStamina();
        }
    }
}
