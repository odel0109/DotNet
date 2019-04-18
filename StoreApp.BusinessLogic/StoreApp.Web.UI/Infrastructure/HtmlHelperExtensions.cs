using System;
using System.Web.Mvc;

namespace StoreApp.Web.UI.Infrastructure
{
    public static class HtmlHelperExtensions
    {
        public static string GetImageSrc(this HtmlHelper htmlHelper, byte[] imageData, string imageMimeType)
        {
            var base64 = Convert.ToBase64String(imageData);
            return String.Format("data:image/{0};base64,{1}", imageMimeType, base64);
        }

        public static string GetDefaultImageSrc(this HtmlHelper htmlHelper)
        {
            return "/ObsoleteContent/2 - adidas/images/pic11.jpg";
        }
    }
}