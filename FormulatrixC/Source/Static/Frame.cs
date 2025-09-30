namespace FormulatrixC.Static;

public class Frame(byte[] raw) : IDisposable
{
  private bool _disposed;
  private readonly byte[] _rawBuffer = raw;

  public byte[] GetRawData()
  {
    return _disposed ? throw new ObjectDisposedException("underlying buffer has changed, should not be used anymore") : _rawBuffer;
  }
  public void Dispose()
  {
    _disposed = true;
    GC.SuppressFinalize(this);
  }
}