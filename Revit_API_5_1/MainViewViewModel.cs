using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_5_1
{
    public class MainViewViewModel
    {

        private ExternalCommandData _commandData;

        public DelegateCommand ShowPipeQtyCommand { get; }
        public DelegateCommand ShowWallTotVlmCommand { get; }
        public DelegateCommand ShowDoorQtyCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            ShowPipeQtyCommand = new DelegateCommand(OnShowPipeQtyCommand);
            ShowWallTotVlmCommand = new DelegateCommand(OnShowWallTotVlmCommand);
            ShowDoorQtyCommand = new DelegateCommand(OnShowDoorQtyCommand);
        }

        public event EventHandler CloseRequest;

        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnShowPipeQtyCommand()
        {
            RaiseCloseRequest(); // ???

            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            int pipeQty = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeCurves)
                .WhereElementIsNotElementType()
                .GetElementCount();

            TaskDialog.Show("Трубы, шт:",$"{pipeQty}");
        }

        private void OnShowWallTotVlmCommand()
        {
            RaiseCloseRequest(); // ???

            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Wall> walls = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Walls)
                .WhereElementIsNotElementType()
                .Cast<Wall>()
                .ToList();

            double wallVolumeTotal = 0.0;

            foreach (var wall in walls)
            {
                wallVolumeTotal += UnitUtils.ConvertFromInternalUnits(wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(), UnitTypeId.CubicMeters);
            }

            TaskDialog.Show("Стены, м^3:", $"{wallVolumeTotal}");
        }
        
        private void OnShowDoorQtyCommand()
        {
            RaiseCloseRequest(); // ???

            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            int doorQty = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .GetElementCount();

            TaskDialog.Show("Двери, шт:", $"{doorQty}");
        }
    }
}
