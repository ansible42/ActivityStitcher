using ActivityFileStiticher.GPX;
using ActivityFileStiticher;
using RecordStitcher;

namespace RecordStitcher.Test;

[TestClass]
public class UnitTest1
{




    [TestMethod]
    [DataRow("Test1-In1-Strava.gpx", "Test1-In2-Strava.gpx", "Test1-output.gpx")]
    public void StitchFilesBasic(string file1, string file2, string matchFile)
    {
        var fileExtension = Path.GetExtension(file1);
        var outputFile = $"./StitchFilesBasic-{DateTime.Now.ToString("HHmmss")}{fileExtension}";
        Console.WriteLine($"OutputData: {Path.GetFullPath(outputFile)}");
        ActivityStitcher.StitchGpx(outputFile, file2, file1);

    }

}