using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scoreAmount = 10;
    [SerializeField] int health = 3;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        if (health < 1)
        {
            ProcessKill();
        }
    }

    void ProcessHit(GameObject other)
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform; // organize all explosions as children
        health--;
    }

    // ParticleSystem explosion - destroys itself automatically
    void ProcessKill()
    {
        scoreBoard.UpdateScore(scoreAmount);
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform; // organize all explosions as children
        Destroy(gameObject);
    }
}
