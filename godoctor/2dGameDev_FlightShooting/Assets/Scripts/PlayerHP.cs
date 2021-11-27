using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 0;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;


    // 프로퍼티
    public float MaxHP => maxHP;        // get만가능
    public float CurrentHP => currentHP; // get만가능


    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>(); 
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if(currentHP <= 0)
        {
            // Debug.Log("Player HP : 0 .. Die ");
            playerController.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
    
}
