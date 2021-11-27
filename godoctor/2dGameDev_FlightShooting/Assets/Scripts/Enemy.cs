using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private int scorePoint = 100;
    private PlayerController playerController;
    [SerializeField]
    private GameObject explosionProfab;


    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            OnDie();
        }
    }


    public void OnDie()
    {
        playerController.Score += scorePoint;
        Instantiate(explosionProfab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
