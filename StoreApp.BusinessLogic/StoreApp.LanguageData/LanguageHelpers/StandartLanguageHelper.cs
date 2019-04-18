using System.Collections.Generic;
using StoreApp.Abstract.Interfaces;
using StoreApp.LanguageData.Abstract;

namespace StoreApp.LanguageData.LanguageHelpers
{
    public class StandartLanguageHelper : ILanguageHelper
    {
        private ILanguageRepository<MessageDetail> repository;

        public StandartLanguageHelper(ILanguageRepository<MessageDetail> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<short> SupportedLanguages => throw new System.NotImplementedException();

        public virtual string GetText(int messageID, short languageCode, string defaultText)
        {
            var message = repository.Find(messageID, languageCode);

            return message == null ? defaultText : message.Message;
        }
    }
}
