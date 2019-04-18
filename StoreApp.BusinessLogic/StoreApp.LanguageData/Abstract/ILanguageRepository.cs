using StoreApp.Abstract.Interfaces;
using StoreApp.LanguageData;
using System.Collections.Generic;

namespace StoreApp.LanguageData.Abstract
{
    public interface ILanguageRepository<TLangType> : IRepository<TLangType>
        where TLangType : MessageDetail
    {
        IEnumerable<short> SupportedLanguages
        {
            get;
        }
    }
}
