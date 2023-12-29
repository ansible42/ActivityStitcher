namespace ActivityFileStiticher;

internal interface IActivityFileStiticher
{
    public string fileName { get; init; }
    public DateTime GetFileStartTime();

    public void AppendFileData(string fileName);

    public void SaveToFile(string fileName);

}
