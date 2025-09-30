using System.Collections.Concurrent;
using System.Timers;
using FormulatrixC.Interfaces;
using FormulatrixC.Repositories;
using FormulatrixC.Static;

namespace FormulatrixC;

public class FrameCalculateAndStream
{
  private readonly IValueReporter _reporter;
  // Change Queue to ConcurrentQueue to comply with multi-threading systems
  // https://stackoverflow.com/questions/50086076/concurrentqueuet-or-queuet-when-one-thread-only-ever-enqueues-and-another-th

  private readonly ConcurrentQueue<Frame> _receivedFrames = new();
  private readonly System.Timers.Timer _timer;
  public FrameCalculateAndStream(FrameGrabber fg, IValueReporter vr, int fps)
  {
    fg.OnFrameUpdated += HandleFrameUpdated;
    _timer = new System.Timers.Timer(1000 / fps);
    _timer.Elapsed += OnTimerElapsed;
    _reporter = vr;
  }
  private void HandleFrameUpdated(Frame frame)
  {
    // Remove old frame, in case the old frame arrives before processed.
    while (_receivedFrames.Count > 2)
    {
      Console.WriteLine($"Dropping old frame: {_receivedFrames.Count} frames");
      _receivedFrames.TryDequeue(out _);
    }
    _receivedFrames.Enqueue(frame);
  }
  private void OnTimerElapsed(object sender, ElapsedEventArgs e)
  {
    if (_receivedFrames.TryDequeue(out var frame))
    {
      var raw = frame.GetRawData();
      int result = (int)raw.Average(b => b);

      _reporter.Report(result);
    }
  }
  public void StartStreaming()
  {
    _timer.Enabled = true;
  }
}