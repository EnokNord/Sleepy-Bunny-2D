using UnityEngine;
using TMPro;
public class BigScoreText : MonoBehaviour
{
    private TextMeshProUGUI BigscoreText;

    // When triggerd this code updates the text
    void Start()
    {
        BigscoreText = GetComponent<TextMeshProUGUI>();
    }

 
    public void UpdateBigscoreText(Scoresystem scoreSystem)
    {
        BigscoreText.text = scoreSystem.BigPickups.ToString();
    }
}
