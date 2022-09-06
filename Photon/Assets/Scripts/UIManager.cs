using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text BlueScoreText;
    public Text RedScoreText;

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
        BlueScoreText.text = value.ToString();
    }
}