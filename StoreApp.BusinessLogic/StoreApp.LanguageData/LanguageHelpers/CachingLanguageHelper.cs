using StoreApp.Abstract.Interfaces;
using StoreApp.LanguageData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.LanguageData.LanguageHelpers
{
    public class CachingLanguageHelper : StandartLanguageHelper
    {
        /// <summary>
        /// Variable which will contain already given values
        /// </summary>
        protected Dictionary<Tuple<int, short>, string> cache;

        public CachingLanguageHelper(ILanguageRepository<MessageDetail> repository) : base(repository)
        {
            cache = new Dictionary<Tuple<int, short>, string>();
        }

        /// <summary>
        /// Returns text by key. If cache contains key -> returns value from cache
        /// </summary>
        /// <param name="messageID"></param>
        /// <param name="languageCode"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public override string GetText(int messageID, short languageCode, string defaultText)
        {
            var cacheKey = new Tuple<int, short>(messageID, languageCode);

            if (cache[cacheKey] != null)
                return cache[cacheKey];

            var result = base.GetText(messageID, languageCode, defaultText);
            cache[cacheKey] = result;

            return result;
        }

        public void ClearCache()
        {
            cache.Clear();
        }
    }
}
