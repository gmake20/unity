using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScroller : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float scrollRange = 9.9f;
    [SerializeField]
    float moveSpeed = 3.0f;
    [SerializeField]
    Vector3 moveDirection = Vector3.down;



    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if(transform.position.y <= -scrollRange)
        {
            transform.position = target.position + Vector3.up * scrollRange;
        }
    }
}
