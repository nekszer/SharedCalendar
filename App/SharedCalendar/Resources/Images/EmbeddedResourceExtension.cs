using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SharedCalendar.Resources.Images
{
    [ContentProperty(nameof(Source))]
    public class EmbeddedResourceExtension : BaseResource, IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;
            Stream stream = GetStream(typeof(EmbeddedResourceExtension), Source);
            if (stream == null) return null;
            byte[] bytes = null;
            using (var memory = new MemoryStream())
            {
                stream.CopyTo(memory);
                bytes = memory.ToArray();
            }
            var imageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            return imageSource;
        }
    }
}