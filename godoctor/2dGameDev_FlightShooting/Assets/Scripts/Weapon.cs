using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float attackRate = 0.1f;  // ���ݼӵ�


    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while(true)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(attackRate);
        }
    }


}
