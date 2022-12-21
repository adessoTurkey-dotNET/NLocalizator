using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace NLocalizator;

//TODO: concurrent okuma
public class Localizator<T> : ILocalizator<T> where T: ILocalizationBook
{
    public T LocalizationBook { get; private set;}
    public string FolderPath { get; }
    public string Language { get; private set; } //buradan verilecek kısaltma, dosya adını belirler.
    private readonly FileSystemWatcher _watcher;

    public Localizator(IOptions<LocalizatorOptions<T>> options) : this(options.Value.FolderPath, options.Value.Language)
    {
    }

    private Localizator(string folderPath, string language)
    {
        if (folderPath == null || language == null)
        {
            throw new ArgumentNullException();
        }

        FolderPath = folderPath;
        Language = language;
        var filePath = BuildFilePath();
        this.LocalizationBook = ReadLocalizationBook(filePath).Result;

        _watcher = new FileSystemWatcher(FolderPath);
        _watcher.NotifyFilter = NotifyFilters.LastWrite;

        _watcher.Changed += OnChanged;
        _watcher.Filter = language + ".json";
        _watcher.EnableRaisingEvents = true;
    }

    private async void OnChanged(object sender, FileSystemEventArgs e)
    {
        if (e.ChangeType != WatcherChangeTypes.Changed)
        {
            return;
        }
        
        var filePath = BuildFilePath();
        LocalizationBook = await ReadLocalizationBook(filePath);
    }


//TODO: lock
    public async void ChangeLanguage(string language)
    {
        Language = language;
        var filePath = BuildFilePath();
        _watcher.Filter = language + ".json";
        LocalizationBook = await ReadLocalizationBook(filePath);
    }

    private string BuildFilePath()
    {
        var sb = new StringBuilder();
        sb.Append(FolderPath)
            .Append("\\")
            .Append(Language)
            .Append(".json");

        var filePath = sb.ToString();

        if(!File.Exists(filePath))
        {
            throw new FileNotFoundException();
        }

        return filePath;
    }

    private async Task<T> ReadLocalizationBook(string filePath)
    {
        try
        {
            string json ="";

            using (var fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                int readLen;
                while ((readLen = await fs.ReadAsync(b,0,b.Length)) > 0)
                {
                    json += temp.GetString(b,0,readLen);
                }
            }

            
            var localizationBook = JsonSerializer.Deserialize<T>(json);

            if (localizationBook == null)
            {
                throw new Exception();
            }

            return localizationBook;
        }
        catch (System.Exception ex)
        {
            //return await ReadLocalizationBook(filePath);
            return LocalizationBook;
        }
        
    }

    public string GetByName(string propertyName)
    {
        var value = LocalizationBook?
            .GetType()?
            .GetProperty(propertyName)?
            .GetValue(LocalizationBook)?
            .ToString();

        if (value == null)
        {
            throw new Exception();
        }

        return value;
    }
}