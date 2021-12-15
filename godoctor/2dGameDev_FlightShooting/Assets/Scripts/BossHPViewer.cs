using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossHP bossHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }


    // Update is called once per frame
    void Update()
    {
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;        
    }
}
