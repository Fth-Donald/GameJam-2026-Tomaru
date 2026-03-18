//using UnityEngine;

//public class Skillicon : MonoBehaviour
//{
//    [Header("Skill Prefabs")]
//    public GameObject[] SkillPrefabs;
//    [Header("Player Prefab")]
//    public PlayerController playerController;

//}


using UnityEngine;
using UnityEngine.UI;

public class Skillicon : MonoBehaviour
{
    [Header("Skill Prefabs")]
    public GameObject[] SkillPrefabs;

    [Header("Skill Icons")]
    public Sprite[] skillIcons;

    [Header("Player Prefab")]
    public PlayerController playerController;

    private Image iconImage;

    private void Awake()
    {
        iconImage = GetComponent<Image>();
    }
    private void Update()
    {
        SetSkillIcon(playerController.skill1);
    }
    public void SetSkillIcon(int index)
    {
        int SkillIndex = playerController.skill1;
        iconImage.sprite = skillIcons[SkillIndex];
    }
}