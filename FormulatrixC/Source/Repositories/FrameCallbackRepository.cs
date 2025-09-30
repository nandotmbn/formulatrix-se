using System.Runtime.InteropServices;
using FormulatrixC.Interfaces;
using FormulatrixC.Static;

namespace FormulatrixC.Repositories;

public class FrameGrabber
{
  private byte[]? _buffer;
  public delegate void FrameUpdateHandler(Frame rawFrame);
  public event FrameUpdateHandler? OnFrameUpdated;

  public FrameGrabber(bool isDummy = false)
  {
    if (isDummy == true) DummyGrabber();
  }

  public void DummyGrabber()
  {
    var rand = new Random();
    var timer = new Timer(_ =>
        {
          var data = Enumerable.Range(0, 1000).Select(_ => (byte)rand.Next(0, 255)).ToArray();
          var frame = new Frame(data); // dummy frame
          OnFrameUpdated?.Invoke(frame);
        }, null, 0, 100); // set fps
  }

  public void FrameReceived(IntPtr frame, int width, int height)
  {
    _buffer ??= new byte[width * height];
    Marshal.Copy(frame, _buffer, 0, width * height);
    Frame bufferedFrame = new(_buffer);
    OnFrameUpdated?.Invoke(bufferedFrame);
    bufferedFrame.Dispose();
  }
}

