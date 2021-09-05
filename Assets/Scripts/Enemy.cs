using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreAmount = 10;

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
        // ParticleSystem explosion - destroys itself automatically
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity); 
        vfx.transform.parent = parent; // organize all explosions as children
        scoreBoard.UpdateScore(scoreAmount);
        Destroy(gameObject);
    }
}
