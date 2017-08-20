using ComputerVisionAPI.Interface;
using Microsoft.ProjectOxford.Vision;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;

namespace ComputerVisionAPI.ViewModel
{
    public class ThumbnailViewModel : ObservableObject
    {
        private IVisionServiceClient _visionClient;

        private BitmapImage _iamgeSource;
        public BitmapImage ImageSource
        {
            get { return _iamgeSource; }
            set
            {
                _iamgeSource = value;
                RaisePropertyChangedEvent("ImageSource");
            }
        }

        private BitmapImage _thumbnail;
        public BitmapImage Thumbnail
        {
            get { return _thumbnail; }
            set
            {
                _thumbnail = value;
                RaisePropertyChangedEvent("Thumbnail");
            }
        }
        public ICommand BrowseAndGenerateThumbnailCommand { get; private set; }
               
        public ThumbnailViewModel(IVisionServiceClient visionClient)
        {
            _visionClient = visionClient;
            Initialize();
        }

        private void Initialize()
        {
            BrowseAndGenerateThumbnailCommand = new DelegateCommand(BrowseAndAnalyze);
        }
        
        private async void BrowseAndAnalyze(object obj)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "Image Files(*.jpg, *.gif, *.bmp, *.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
            bool? result = openDialog.ShowDialog();

            if (!(bool)result) return;

            string filePath = openDialog.FileName;

            Uri fileUri = new Uri(filePath);
            BitmapImage image = new BitmapImage(fileUri);

            image.CacheOption = BitmapCacheOption.None;
            image.UriSource = fileUri;

            ImageSource = image;

            try
            {
                using (Stream fileStream = File.OpenRead(filePath))
                {
                    byte[] thumbnailResult = await _visionClient.GetThumbnailAsync(fileStream, 250, 250);

                    if (thumbnailResult != null && thumbnailResult.Length != 0)
                        CreateThumbnail(thumbnailResult);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to analyze image: {ex.Message}");
            }
        }
        
        private void CreateThumbnail(byte[] thumbnailResult)
        {
            try
            {
                MemoryStream ms = new MemoryStream(thumbnailResult);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.None;
                image.StreamSource = ms;
                image.EndInit();

                Thumbnail = image;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to create thumbnail: {ex.Message}");
            }
        }
    }
}