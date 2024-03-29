using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private float _TimerLength;
    [SerializeField]
    private RectTransform _TimerObject;
    [SerializeField]
    private TextMeshProUGUI _Display;
    [SerializeField]
    private UnityEvent OnTimerEnd;

    public float BarLength;

    private float _CurrentTime;
    private bool _Running = false;

    private Vector2 Startposition;
    private Vector2 Endposition;

    private void Start()
    {
        Startposition = new Vector2(0, 0);
        Endposition = new Vector2(BarLength, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_Running)
        {
            _CurrentTime += Time.deltaTime;
            int temp = (int)(_TimerLength - _CurrentTime);
            _Display.text = (temp).ToString();
        }

        if ((_CurrentTime >= _TimerLength) && _Running)
        {
            _Running = false;
            if (OnTimerEnd != null)
                OnTimerEnd.Invoke();    //Event incase the timer runs out
        }

        _TimerObject.anchoredPosition = Vector2.Lerp(Startposition, Endposition, (_CurrentTime / _TimerLength));
        //Debug.Log(_CurrentTime / _TimerLength);
        //Debug.Log(_CurrentTime);
    }

    public void StartTimerwithTime(float _timer)
    {
        _TimerLength = _timer;
        _CurrentTime = 0;
        _Running = true;
    }

    public void StartTimer()
    {
        _CurrentTime = 0;
        _Running = true;
    }

    public float CurrentTime()
    {
        return _CurrentTime;
    }

    public void StopTimer()
    {
        _Running = false;
        _TimerObject.anchoredPosition = Startposition;
    }
}
