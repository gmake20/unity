using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private AudioClip boomAudio;
    [SerializeField]
    private int damage = 100; // 폭탄 데미지
    private float boomDelay = 0.5f;
    private Animator animator;
    private AudioSource audioSource;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("MoveToCenter");
    }

    private IEnumerator MoveToCenter()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = Vector3.zero;
        float current = 0;
        float percent = 0;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / boomDelay;

            transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate(percent));

            yield return null;
        }

        animator.SetTrigger("onBoom");
        audioSource.clip = boomAudio;
        audioSource.Play();
    }


    // Animation Event가 끝나면 모든 적을 제거한다.
    public void OnBoom()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] meteorites = GameObject.FindGameObjectsWithTag("Meteorite");

        for(int i=0;i<enemys.Length;++i)
        {
            enemys[i].GetComponent<Enemy>().OnDie();
        }

        for(int i=0;i<meteorites.Length;++i)
        {
            meteorites[i].GetComponent<Meteorite>().OnDie();
        }

        // 현제 게임내에 존재하는 적, 보스의 발사제를 모두 파괴
        // FindGameObject는 속도가 느리지 않을까? 속도문제는 해결해야됨.
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        for(int i=0;i<projectiles.Length;i++)
        {
            projectiles[i].GetComponent<EnemyProjectile>().OnDie();
        }

        // 현재 게임내에서 Boss태그를 가진 오브젝트 정보를 가져온다.
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");
        if(boss != null)
        {
            boss.GetComponent<BossHP>().TakeDamage(damage);
        }

        // 모든적을 제거하면 자기자신도 삭제한다.
        Destroy(gameObject);
    }

}
