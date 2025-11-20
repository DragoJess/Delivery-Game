using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameCycleManager : MonoBehaviour
{
    public static GameCycleManager Instance;
    [SerializeField] public int Day;
    [SerializeField] Animator DayAnim;
    [SerializeField] TMP_Text DayText;
    public UnityEvent<int> OnDayChange = new UnityEvent<int>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        NextDay();
    }
    public void NextDay()
    {
        Day++;
        DayText.text = $"Day {Day}";
        DayAnim.SetTrigger("Play");
        OnDayChange.Invoke(Day);
    }
}
