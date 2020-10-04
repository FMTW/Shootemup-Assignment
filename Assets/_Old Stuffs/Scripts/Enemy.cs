using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Basic Setting")]
    [SerializeField] protected float health;

    [Header("VFX Setting")]
    [SerializeField] protected GameObject explosionVFX;
    [SerializeField] protected float glowIntensity;
    protected Renderer[] glowTargets;

    [Header("Loot")]
    [SerializeField] protected GameObject powerup;
    [SerializeField] protected float spawnRate;

    public virtual void TakeDamage(float damage) {}

    protected void Explode()
    {
        GameObject clone = Instantiate(explosionVFX, transform.position, transform.rotation);
        clone.transform.parent = GameObject.FindGameObjectWithTag("Explosions").transform;
        Destroy(clone, 2f);
        Destroy(gameObject);
    }


    protected void MultiplyStats(float multiplier)
    {
        health *= multiplier;
    }

    protected void DropItem()
    {
        if (Random.value < spawnRate)
        {
            GameObject clone = Instantiate(powerup, new Vector3(transform.position.x, 10f, transform.position.z), transform.rotation);
            clone.transform.parent = GameObject.FindGameObjectWithTag("Powerups").transform;
        }
    }

    protected IEnumerator Glow()
    {
        float time = 0.5f;
        while (time > 0)
        {
            time -= Time.deltaTime;
            foreach (var target in glowTargets)
            {
                target.material.SetVector("_EmissionColor", Color.yellow * glowIntensity * time);
            }
            yield return null;
        }
    }
}
