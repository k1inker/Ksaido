using UnityEngine;
using UnityEngine.UI;
public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public void setHealth(float health, float maxHealth)
    {
        slider.value = health;
        slider.maxValue = maxHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }   
}
