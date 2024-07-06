using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    private GameTimer gameTimer;

    public TimerManager()
    {
        gameTimer = new GameTimer(); 
    }

    public void Register(float timer, System.Action callback)
    {
        gameTimer.Register(timer, callback);
    }

    public void OnUpdate(float dt)
    {
        gameTimer.OnUpdate(dt);
    }

}
