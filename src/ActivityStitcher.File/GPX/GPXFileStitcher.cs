using System.Xml.Serialization;

namespace ActivityFileStiticher.GPX;

public sealed class GPXFileStitcher : IActivityFileStiticher
{

    public string fileName { get; init; }
    private gpxType GPXData;
    public GPXFileStitcher(string file)
    {
        this.GPXData = this.ReadGPXFromFile(file);
        this.fileName = file;
    }

    public void AppendFileData(string fileName)
    {
        var tmpGPX = this.ReadGPXFromFile(fileName);
        var tmpTkptData = this.GPXData.trk[0].trkseg[0].trkpt.ToList();
        foreach (var trackpoint in tmpGPX.trk[0].trkseg[0].trkpt) 
        {
            tmpTkptData.Add(trackpoint);
        }
        this.GPXData.trk[0].trkseg[0].trkpt = tmpTkptData.ToArray();
    }

    public DateTime GetFileStartTime()
    {
        return this.GPXData.metadata.time;
    }

    public void SaveToFile(string fileName)
    {

        XmlSerializer mySerializer = new XmlSerializer(typeof(gpxType));
        // To write to a file, create a StreamWriter object.  
        StreamWriter myWriter = new StreamWriter(fileName);
        mySerializer.Serialize(myWriter, this.GPXData);
        myWriter.Close();
    }

    #region Private 

    private gpxType ReadGPXFromFile(string fileName) 
    {
        // Construct an instance of the XmlSerializer with the type
        // of object that is being deserialized.
        var mySerializer = new XmlSerializer(typeof(gpxType));
        // To read the file, create a FileStream.
        using var myFileStream = new FileStream(fileName, FileMode.Open);
        // Call the Deserialize method and cast to the object type.
        var GPXType = (gpxType)mySerializer.Deserialize(myFileStream)!;
        return GPXType; 
    }

    #endregion Private
}
