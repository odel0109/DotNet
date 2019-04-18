using System.Collections.Generic;

namespace StoreApp.Abstract.Interfaces
{
    public interface ILanguageHelper
    {
        /// <summary>
        /// Returns text using Message`s and language`s identifiers. If nothing was found, returns default text 
        /// </summary>
        /// <param name="MessageID"></param>
        /// <param name="LanguageCode"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        string GetText(int messageID, short languageCode, string defaultText);

        /// <summary>
        /// List of supported languages
        /// </summary>
        IEnumerable<short> SupportedLanguages { get; }
    }
}
