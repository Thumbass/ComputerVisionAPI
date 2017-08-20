using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision;

namespace ComputerVisionAPI.ViewModel
{
    public class DescriptionViewModel
    {
        private IVisionServiceClient _visionClient;

        public DescriptionViewModel(IVisionServiceClient visionClient)
        {
            _visionClient = visionClient;
        }
    }
}
