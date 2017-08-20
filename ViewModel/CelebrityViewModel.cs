using System;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Diagnostics;
using System.Text;
using Microsoft.ProjectOxford.Vision;
using ComputerVisionAPI.Interface;

namespace ComputerVisionAPI.ViewModel
{
    public class CelebrityViewModel : ObservableObject
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

        private string _imageUrl;
        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                RaisePropertyChangedEvent("ImageUrl");
            }
        }

        private string _celebrity;
        public string Celebrity
        {
            get { return _celebrity; }
            set
            {
                _celebrity = value;
                RaisePropertyChangedEvent("Celebrity");
            }
        }

        public ICommand LoadAndFindCelebrityCommand { get; private set; }
        
        public CelebrityViewModel(IVisionServiceClient visionClient)
        {
            _visionClient = visionClient;
            LoadAndFindCelebrityCommand = new DelegateCommand(LoadAndFindCelebrity, CanFindCelebrity);
        }
        
        private bool CanFindCelebrity(object obj)
        {
            return !string.IsNullOrEmpty(ImageUrl);
        }
        
        private async void LoadAndFindCelebrity(object obj)
        {
            Uri fileUri = new Uri(ImageUrl);
            BitmapImage image = new BitmapImage(fileUri);

            image.CacheOption = BitmapCacheOption.None;
            image.UriSource = fileUri;

            ImageSource = image;

            try
            {
                AnalysisInDomainResult celebrityResult = await _visionClient.AnalyzeImageInDomainAsync(ImageUrl, "celebrities");

                if (celebrityResult != null)
                    Celebrity = celebrityResult.Result.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to analyze image: {ex.Message}");
            }
        }
    }
}
