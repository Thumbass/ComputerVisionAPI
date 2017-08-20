using ComputerVisionAPI.Interface;
using System.Linq;
using System.Collections.Generic;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace ComputerVisionAPI.ViewModel
{
    public class OcrViewModel : ObservableObject
    {
        private IVisionServiceClient _visionClient;

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                RaisePropertyChangedEvent("ImageSource");
            }
        }

        private string _ocrResult;
        public string OcrResult
        {
            get { return _ocrResult; }
            set
            {
                _ocrResult = value;
                RaisePropertyChangedEvent("OcrResult");
            }
        }

        public ICommand BrowseAndAnalyzeImageCommand { get; private set; }
                
        public OcrViewModel(IVisionServiceClient visionClient)
        {
            _visionClient = visionClient;
            Initialize();
        }
        
        private void Initialize()
        {
            BrowseAndAnalyzeImageCommand = new DelegateCommand(BrowseAndAnalyze);
        }
        
        private async void BrowseAndAnalyze(object obj)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog();

            openDialog.Filter = "JPEG Image(*.jpg)|*.jpg";
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
                    OcrResults analysisResult = await _visionClient.RecognizeTextAsync(fileStream);

                    if (analysisResult != null)
                        OcrResult = PrintOcrResult(analysisResult);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to analyze image: {ex.Message}");
            }
        }
        
        private string PrintOcrResult(OcrResults ocrResult)
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("Language is {0}\n", ocrResult.Language);
            result.Append("The words are:\n\n");

            foreach (var region in ocrResult.Regions)
            {
                foreach (var line in region.Lines)
                {
                    foreach (var text in line.Words)
                    {
                        result.AppendFormat("{0} ", text.Text);
                    }

                    result.Append("\n");
                }
                result.Append("\n\n");
            }

            return result.ToString();
        }
    }
}