namespace NLocalizator;

public interface ILocalizator<T> where T: ILocalizationBook
{
    T LocalizationBook { get; }
    void ChangeLanguage(string language);
    string GetByName(string propertyName);   
}