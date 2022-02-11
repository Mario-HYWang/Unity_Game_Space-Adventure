using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject hitVFXPrefab;
    [SerializeField] int scorePerHit;

    [SerializeField] int hP;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;
    MeshRenderer color;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindGameObjectWithTag("SpawnAtRuntime");

        color = GetComponent<MeshRenderer>();
        Rigidbody rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        EnemyHit();
        ScoreIncreased();
    }

    private void ScoreIncreased()
    {
        scoreBoard.ScoreIncrease(scorePerHit);
    }

    private void EnemyHit()
    {
        hP--;
        color.material.color = Color.red;
        GameObject hitVFX = Instantiate(hitVFXPrefab, this.transform.position, Quaternion.identity);
        hitVFX.transform.parent = parentGameObject.transform;

        if (hP == 0f)
        {
            GameObject vFX = Instantiate(explosionVFX, this.transform.position, Quaternion.identity);
            vFX.transform.parent = parentGameObject.transform;
            Destroy(this.gameObject);
        }        
    }
}
