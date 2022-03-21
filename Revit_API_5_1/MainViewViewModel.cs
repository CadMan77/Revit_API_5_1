using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Revit_API_5_1
{
    public class MainViewViewModel
    {

        private static ExternalCommandData _commandData;

        private static UIApplication _uiapp;
        private static UIDocument _uidoc;
        private static Document _doc;

        public DelegateCommand ShowPipeQtyCommand { get; }
        public DelegateCommand ShowWallTotVlmCommand { get; }
        public DelegateCommand ShowDoorQtyCommand { get; }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            _uiapp = _commandData.Application;
            _uidoc = _uiapp.ActiveUIDocument;
            _doc = _uidoc.Document;

            ShowPipeQtyCommand = new DelegateCommand(OnShowPipeQtyCommand);
            ShowWallTotVlmCommand = new DelegateCommand(OnShowWallTotVlmCommand);
            ShowDoorQtyCommand = new DelegateCommand(OnShowDoorQtyCommand);
        }

        //public event EventHandler CloseRequest;

        //private void RaiseCloseRequest()
        //{
        //    CloseRequest?.Invoke(this, EventArgs.Empty);
        //}

        public event EventHandler HideRequest;

        private void RaiseHideRequest()
        {
            HideRequest?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ShowRequest;

        private void RaiseShowRequest()
        {
            ShowRequest?.Invoke(this, EventArgs.Empty);
        }

        private void OnShowPipeQtyCommand()
        {
            //RaiseCloseRequest(); // ???
            RaiseHideRequest(); // ???

            //int pipeQty = new FilteredElementCollector(_commandData.Application.ActiveUIDocument.Document)
            int pipeQty = new FilteredElementCollector(_doc)
                .OfCategory(BuiltInCategory.OST_PipeCurves)
                .WhereElementIsNotElementType()
                .GetElementCount();

            TaskDialog.Show("Трубы, шт:", $"{pipeQty}");

            RaiseShowRequest();
        }

        private void OnShowWallTotVlmCommand()
        {
            //RaiseCloseRequest(); // ???
            RaiseHideRequest(); // ???

            //List<Wall> walls = new FilteredElementCollector(_commandData.Application.ActiveUIDocument.Document)
            List<Wall> walls = new FilteredElementCollector(_doc)
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

            RaiseShowRequest();
        }

        private void OnShowDoorQtyCommand()
        {
            //RaiseCloseRequest(); // ???
            RaiseHideRequest(); // ???

            //int doorQty = new FilteredElementCollector(_commandData.Application.ActiveUIDocument.Document)
            int doorQty = new FilteredElementCollector(_doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .GetElementCount();

            TaskDialog.Show("Двери, шт:", $"{doorQty}");

            RaiseShowRequest();
        }
    }
}
