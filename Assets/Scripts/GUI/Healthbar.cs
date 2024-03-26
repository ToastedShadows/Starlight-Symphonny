using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    private const float MAX_HEALTH = 100f;

    public float health = MAX_HEALTH;

    private Image healthBar; // Changed from Healthbar to healthBar

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / MAX_HEALTH;
    }
}