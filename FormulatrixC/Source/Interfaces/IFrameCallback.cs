namespace FormulatrixC.Interfaces;
public interface IFrameCallback
{
  public void FrameReceived(IntPtr pFrame, int pixelWidth, int pixelHeight);
}