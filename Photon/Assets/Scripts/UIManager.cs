using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text BlueScoreText;
    public Text RedScoreText;

    public Slider HealthBar;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetBlueScoreText(int value)
    {
        BlueScoreText.text = value.ToString();
    }

    public void SetRedScoreText(int value)
    {
        RedScoreText.text = value.ToString();
    }

    public void SetHealthBar(float value)
    {
        HealthBar.value = value / 100;
    }
}
