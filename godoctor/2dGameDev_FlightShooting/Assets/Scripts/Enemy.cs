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

    [SerializeField]
    private GameObject[] itemPrefabs;   // ���� �׿����� ȹ�氡���� ������ 


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
        SpawnItem();
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        // �Ŀ���������
        int spawnItem = Random.Range(0, 100);
        if (spawnItem < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 30)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }
}
