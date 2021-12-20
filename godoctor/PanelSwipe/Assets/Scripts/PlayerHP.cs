using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Transform parentTransform;
    [SerializeField]
    private GameObject hudTextPrefab;

    private void OnEnable()
    {
        StartCoroutine("UpdateHPLoop");
    }

    private void OnDisable()
    {
        StopCoroutine("UpdateHPLoop");
    }

    private IEnumerator UpdateHPLoop()
    {
        while(true)
        {
            // 0.1 ~ 1초 사이의 시간동안 대기후 SpawnHUDText() 메소드 실행
            float time = Random.Range(0.1f, 1.0f);

            yield return new WaitForSeconds(time);

            // 0:체력감소 1:체력증가
            int type = Random.Range(0, 2);
            // 증가/감소되는 체력수치
            string text = Random.Range(10, 1000).ToString();
            Color color = type == 0 ? Color.red : Color.green;

            SpawnHUDText(text, color);
        }
    }

    private void SpawnHUDText(string text,Color color)
    {
        GameObject clone = Instantiate(hudTextPrefab);
        clone.transform.SetParent(parentTransform);
        Bounds bounds = GetComponent<Collider2D>().bounds;
        // Text출력 및 애니메이션 재생
        clone.GetComponent<UIHUDText>().OnPlay(text, color, bounds);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
