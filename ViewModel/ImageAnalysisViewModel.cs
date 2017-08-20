using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;
using ComputerVisionAPI.Interface;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.IO;
using Microsoft.ProjectOxford.Vision.Contract;
using System.Diagnostics;

namespace ComputerVisionAPI.ViewModel
{
    public class ImageAnalysisViewModel : ObservableObject
    {
        private IVisionServiceClient _visionClient;


        public ImageAnalysisViewModel(IVisionServiceClient visionClient)
        {
            _visionClient = visionClient;
            Initialize();
        }

        private void Initialize()
        {
            Features = Enum.GetValues(typeof(VisualFeature)).Cast<VisualFeature>().ToList();

            BrowseAndAnalyzeImageCommand = new DelegateCommand(BrowseAndAnalyze);

        }

        private async void BrowseAndAnalyze(object obj)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "JPEG Image(*.jpg) |*jpg";
            bool? result = openDialog.ShowDialog();
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
                    AnalysisResult analysisResult = await _visionClient.AnalyzeImageAsync(fileStream, SelectedFeatures);

                    if (analysisResult != null)
                        AnalysisResult = PrintAnalysisResult(analysisResult);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to analyze image: {ex.Message}");
            }
        }

        private List<VisualFeature> _features = new List<VisualFeature>();
        public List<VisualFeature> Features
        {
            get { return _features; }
            set
            {
                _features = value;
                RaisePropertyChangedEvent("Features");
            }
        }
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
        private List<VisualFeature> _selectedFeatures = new List<VisualFeature>();
        public List<VisualFeature> SelectedFeatures
        {
            get { return _selectedFeatures; }
            set
            {
                _selectedFeatures = value;
                RaisePropertyChangedEvent("SelectedFeatures");
            }
        }
        private string analysisResult;

        public string AnalysisResult
        {
            get { return analysisResult; }
            set {
                    analysisResult = value;
                RaisePropertyChangedEvent("AnalysisResult");
                }
        }
        public ICommand BrowseAndAnalyzeImageCommand { get; private set; }
        private string PrintAnalysisResult(AnalysisResult analysisResult)
        {
            StringBuilder result = new StringBuilder();

            GetDescription(analysisResult, result);

            GetCategories(analysisResult, result);

            GetColors(analysisResult, result);

            GetAdulty(analysisResult, result);

            GetFaces(analysisResult, result);

            GetMetaData(analysisResult, result);

            GetRequestId(analysisResult, result);

            GetTags(analysisResult, result);

            return result.ToString();
        }
        private static void GetDescription(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Description != null)
            {
                result.AppendFormat("Description: {0}\n", analysisResult.Description.Captions[0].Text);
                result.AppendFormat("Probability: {0}\n\n", analysisResult.Description.Captions[0].Confidence);
            }
        }
                
        private static void GetCategories(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Categories != null)
            {
                result.Append("Categories:\n");

                foreach (var category in analysisResult.Categories)
                {
                    if (!string.IsNullOrEmpty(category.Name)) result.AppendFormat("\t{0}\n", category.Name);
                    result.AppendFormat("\t\tProbability: {0}\n\n", category.Score);
                }
            }
        }
                
        private static void GetColors(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Color != null)
            {
                result.Append("Colors:\n");
                result.AppendFormat("\tForeground: {0}\n", analysisResult.Color.DominantColorForeground);
                result.AppendFormat("\tBackground: {0}\n\n", analysisResult.Color.DominantColorBackground);
            }
        }
        
        private static void GetAdulty(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Adult != null)
            {
                result.AppendFormat("Image content is adulty: {0}\n", analysisResult.Adult.IsAdultContent);
                result.AppendFormat("Image content is racy: {0}\n\n", analysisResult.Adult.IsRacyContent);
            }
        }
        
        private static void GetFaces(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Faces != null)
            {
                result.AppendFormat("Image contains {0} face(s)\n\n", analysisResult.Faces.Count());
            }
        }

        private static void GetMetaData(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Metadata != null)
            {
                result.AppendFormat("The image is {0}\n", analysisResult.Metadata.Format);
                result.AppendFormat("Image size: {0} * {1} px\n\n", analysisResult.Metadata.Width, analysisResult.Metadata.Height);
            }
        }

        private static void GetRequestId(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.RequestId != null)
            {
                result.AppendFormat("Request ID is: {0}\n\n", analysisResult.RequestId.ToString());
            }
        }
        
        private static void GetTags(AnalysisResult analysisResult, StringBuilder result)
        {
            if (analysisResult.Tags != null)
            {
                result.Append("Tags:\n");

                foreach (var tag in analysisResult.Tags)
                {
                    if (!string.IsNullOrEmpty(tag.Name)) result.AppendFormat("\t{0}\n", tag.Name);
                    if (!string.IsNullOrEmpty(tag.Hint)) result.AppendFormat("\t\t{0}\n", tag.Hint);
                    result.AppendFormat("\t\tProbability: {0}\n\n", tag.Confidence);
                }
            }
        }
    }
}
