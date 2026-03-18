using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class HPScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Image fillImage;
    public PlayerController playerController;
    float cH,mH;
    void Update()
    {
        cH = playerController.currentHealth;
        mH = playerController.maxHealth;
        fillImage.fillAmount = cH / mH;
        Debug.Log(playerController.currentHealth);
        Debug.Log(playerController.maxHealth);
    }
}
//