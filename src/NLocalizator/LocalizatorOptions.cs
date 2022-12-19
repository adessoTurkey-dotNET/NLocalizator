public class LocalizatorOptions<T> where T: ILocalizationBook
{
    public int Id { get; set; }
    public string FolderPath { get; private set;}
    public string Language { get; private set;}
    public bool PropertyLevelSensitivity { get; private set; }


    public LocalizatorOptions<T> AddFolderPath(string path)
    {
        FolderPath = path;
        return this;
    }

    public LocalizatorOptions<T> AddLanguage(string language)
    {
        Language = language;
        return this;
    }

    public LocalizatorOptions<T> UsePropertyLevelSensitivity()
    {
        PropertyLevelSensitivity = true;
        return this;
    }
}