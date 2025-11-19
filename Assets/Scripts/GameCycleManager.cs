using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameCycleManager : MonoBehaviour
{
    [SerializeField] int Day;
    [SerializeField] Animator DayAnim;
    [SerializeField] TMP_Text DayText;
    public UnityEvent<int> OnDayChange = new UnityEvent<int>();
    public void NextDay()
    {
        Day++;
        DayText.text = $"Day {Day}";
        DayAnim.SetTrigger("Play");
        OnDayChange.Invoke(Day);
    }
}
