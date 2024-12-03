using TMPro;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text Date;
    [SerializeField] private TMP_Text Time;
    [SerializeField] private TMP_Text Dishes;

    public void SetupPanel(SaveManager.GameTimeData gameTimeData)
    {
        Date.text = gameTimeData.ToString();
        Time.text = PlayerPrefs.GetString(gameTimeData.DateTimeString + SaveManager.timeInGameKey);
        Dishes.text = PlayerPrefs.GetString(gameTimeData.DateTimeString + SaveManager.timeCyclesCountKey);
    }
}
