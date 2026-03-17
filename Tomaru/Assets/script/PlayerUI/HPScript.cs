using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class HPScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Image fillImage;

    public float maxHP = 5f;
    private float currentHP = 5f;

    public void SetHP(float hp)
    {
        currentHP = Mathf.Clamp(hp, 0, maxHP);
        fillImage.fillAmount = currentHP / maxHP;
    }
}
