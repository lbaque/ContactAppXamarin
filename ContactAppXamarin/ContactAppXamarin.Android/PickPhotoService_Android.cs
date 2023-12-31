using Android.Content;
using Android.Provider;
using ContactAppXamarin.Droid;
using ContactAppXamarin.Helpers;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using YourNamespace.Droid;

[assembly: Dependency(typeof(PickPhotoService_Android))]

namespace YourNamespace.Droid
{
    public class PickPhotoService_Android : IPickPhotoService
    {
        public Task<Stream> GetImageStreamAsync()
        {
            Intent intent = new Intent(Intent.ActionPick, MediaStore.Images.Media.ExternalContentUri);
            MainActivity.Instance.StartActivityForResult(Intent.CreateChooser(intent, "Select Photo"), MainActivity.PickImageId);

            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }
    }
}
