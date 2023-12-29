using ActivityFileStiticher.GPX;
namespace ActivityFileStiticher;


public static class ActivityStitcher
{
    public static void StitchGpx(string outputFile, params string[] filesToStitch)
    {
        var fileExtension = Path.GetExtension(filesToStitch[0]).ToLower(); //All must be the same 
        List<IActivityFileStiticher> fileStitcherList = new();
        foreach (var fileStitch in filesToStitch)
        {
            if (Path.GetExtension(fileStitch).ToLower() != fileExtension) 
            {
                throw new FileLoadException("All files must be of the same typ");
            }
            fileStitcherList.Add(ActivityStitcherFactory(fileStitch));
        }
        fileStitcherList.Sort((l, g) => Comparer<DateTime>.Default.Compare((DateTime)l.GetFileStartTime(), (DateTime)g.GetFileStartTime()));

        File.Copy(fileStitcherList[0].fileName, outputFile);
        var outputStitcher = ActivityStitcherFactory(outputFile);
        foreach (var stitcher in fileStitcherList.Skip(1)) 
        { 
            outputStitcher.AppendFileData(stitcher.fileName);
        }

        outputStitcher.SaveToFile(outputFile);
    }

    private static IActivityFileStiticher ActivityStitcherFactory(string file)
    {
        var fileExtension = Path.GetExtension(file).ToLower();
        return fileExtension switch
        {
            ".gpx" => new GPXFileStitcher(file),
            _ => throw new FileLoadException()
        } ;
    }
}


