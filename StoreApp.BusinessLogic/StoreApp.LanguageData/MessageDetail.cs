using StoreApp.Abstract.Interfaces;

namespace StoreApp.LanguageData
{
    public class MessageDetail : IEntity
    {
        /// <summary>
        /// Identifier of message
        /// </summary>
        public int MessageID { get; set; }

        /// <summary>
        /// Language of message
        /// </summary>
        public short LanguageCode { get; set; }

        /// <summary>
        /// Localized message
        /// </summary>
        public string Message { get; set; }

        public object[] PrimaryKey => new object[] { MessageID, LanguageCode };
    }
}
