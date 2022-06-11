internal sealed class GoogleDriveDownloadUrl
{
    private const string PREFIX = "https://drive.google.com/uc?export=download&id=";

    public string GenerateUrl(string id)
    {
        return string.Concat(PREFIX, id);
    }
}
