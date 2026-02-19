using TMPro;
using UnityEngine;

public class SamllScoreText : MonoBehaviour
{
    private TextMeshProUGUI SmallscoreText;

    // When triggerd this code updates the text
    void Start()
    {
        SmallscoreText = GetComponent<TextMeshProUGUI>();
    }


    public void UpdateSmallscoreText(Scoresystem scoreSystem)
    {
        SmallscoreText.text = scoreSystem.SmallPickups.ToString();
    }
}
