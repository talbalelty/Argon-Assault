using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreAmount = 10;
    [SerializeField] int health = 3;

    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        scoreBoard.UpdateScore(scoreAmount);
        health--;
        ProcessHit(other);
        if (health < 1)
        {
            ProcessKill();
        }
    }

    void ProcessHit(GameObject other)
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent; // organize all explosions as children
    }

    // ParticleSystem explosion - destroys itself automatically
    void ProcessKill()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent; // organize all explosions as children
        Destroy(gameObject);
    }
}
