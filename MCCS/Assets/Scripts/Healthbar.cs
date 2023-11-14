using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    public Vector3 offset;


    public void UpdateHealthBar(float health, float maxHealth) {
        slider.value = health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
