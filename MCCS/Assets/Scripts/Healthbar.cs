using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Image sliderBackground;
    [SerializeField] private Image sliderFill;
    public Vector3 offset;

	public void Start()
	{
        Image[] sliderImages = GetComponentsInChildren<Image>();
        sliderBackground = sliderImages[0];
        sliderFill = sliderImages[1];
	}

	public void UpdateHealthBar(float health, float maxHealth) {
        slider.value = health / maxHealth;
    }

    public void ChangeColor() {
        sliderBackground.color = new Color(190, 87, 0, 255);
        sliderFill.color = new Color(255,153,0,255);

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
