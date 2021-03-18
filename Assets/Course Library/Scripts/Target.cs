using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // Start is called before the first frame update
    public int spawnRange = 20;
    public int minSpeed = 10;
    public int maxSpeed = 20;
    public int rotRange = 10;
    public int pointValue = 0;
    public ParticleSystem[] explosionParticles;
    private Rigidbody oBody;
    private Vector3 spawnPos;

    private GameManager gameManager;
    void Start()
    {
        oBody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        int xPos = Random.Range(-spawnRange, spawnRange);
        transform.position = new Vector3(xPos, -1);

        oBody.AddForce(randomForce(), ForceMode.Impulse);
        oBody.AddTorque(randomRotation(-rotRange, rotRange), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 randomRotation(int lBound, int uBound)
    {
        return new Vector3(Random.Range(lBound, uBound), Random.Range(lBound, uBound), Random.Range(lBound, uBound));
    }
    
    Vector3 randomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private void OnMouseDown()
    {
        if (!gameManager.gameOver)
        {
            gameManager.UpdateScore(pointValue);
            ParticleSystem explosionParticle = explosionParticles[Random.Range(0, explosionParticles.Length)];
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
        Destroy(gameObject);
    }
}
