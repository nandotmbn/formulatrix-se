using FormulatrixC;
using FormulatrixC.Repositories;

var grabber = new FrameGrabber(isDummy: true);
var reporter = new ValueReporter();

int fps = 30;

var streamer = new FrameCalculateAndStream(grabber, reporter, fps);
streamer.StartStreaming();

Console.WriteLine("Streaming started... Press ENTER to exit.");
Console.ReadLine();