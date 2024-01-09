using SFML.System;

namespace Nord.Engine.Core;

public class Time : Clock
{
    public float Dt { get; private set; }
    public double SinceStart { get; private set; }
    public float AverageFps { get; private set; }

    private float _frameCounter;
    private float _elapsedFrameTime;

    public void Update()
    {
        var time = Restart();
        
        Dt = time.AsSeconds();
        SinceStart += Dt;
        
        _frameCounter++;
        _elapsedFrameTime += Dt;

        if (_elapsedFrameTime >= 1)
        {
            AverageFps = _frameCounter / _elapsedFrameTime;
            _frameCounter = _elapsedFrameTime = 0;
        }
    }
}