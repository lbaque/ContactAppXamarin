using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppXamarin.Helpers
{
    public interface IPickPhotoService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
