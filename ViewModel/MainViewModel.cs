using ComputerVisionAPI.Interface;
using Microsoft.ProjectOxford.Vision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerVisionAPI.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private IVisionServiceClient _visionClient;
        private CelebrityViewModel _celebrityVm;

        public CelebrityViewModel CelebrityVm
        {
            get { return _celebrityVm; }
            set
            {
                _celebrityVm = value;
                RaisePropertyChangedEvent("CelebrityVM");
            }
        }
        private DescriptionViewModel _descriptionVm;
        public DescriptionViewModel DescriptionVm
        {
            get { return _descriptionVm; }
            set
            {
                _descriptionVm = value;
                RaisePropertyChangedEvent("DescriptionVm");
            }
        }

        private ImageAnalysisViewModel _imageAnalysisVm;
        public ImageAnalysisViewModel ImageAnalysisVm
        {
            get { return _imageAnalysisVm; }
            set
            {
                _imageAnalysisVm = value;
                RaisePropertyChangedEvent("ImageAnalysisVm");
            }
        }

        private OcrViewModel _ocrVm;
        public OcrViewModel OcrVm
        {
            get { return _ocrVm; }
            set
            {
                _ocrVm = value;
                RaisePropertyChangedEvent("OcrVm");
            }
        }

        private ThumbnailViewModel _thumbnailVm;
        public ThumbnailViewModel ThumbnailVm
        {
            get { return _thumbnailVm; }
            set
            {
                _thumbnailVm = value;
                RaisePropertyChangedEvent("ThumbnailVm");
            }
        }
        public MainViewModel()
        {
            _visionClient = new VisionServiceClient("57763c58fb7a47fb9ee105c216e66211");
            CelebrityVm = new CelebrityViewModel(_visionClient);
            DescriptionVm = new DescriptionViewModel(_visionClient);
            ImageAnalysisVm = new ImageAnalysisViewModel(_visionClient);
            OcrVm = new OcrViewModel(_visionClient);
            ThumbnailVm = new ThumbnailViewModel(_visionClient);
        }
    }
}
