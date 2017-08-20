using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.ProjectOxford.Vision;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComputerVisionAPI.ViewModel;

namespace ComputerVisionAPI.View
{
    /// <summary>
    /// Interaction logic for ImageAnalysisView.xaml
    /// </summary>
    public partial class ImageAnalysisView : UserControl
    {
        public ImageAnalysisView()
        {
            InitializeComponent();
        }
    private void VisualFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = (MainViewModel)DataContext;
            vm.ImageAnalysisVm.SelectedFeatures.Clear();

            foreach (VisualFeature feature in VisualFeatures.SelectedItems)
            {
                vm.ImageAnalysisVm.SelectedFeatures.Add(feature);
            }
        }
    }
    
}
