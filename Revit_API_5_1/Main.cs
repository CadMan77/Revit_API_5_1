//Создайте приложение с несколькими кнопками: 
//    1 кнопка по нажатию должна выводить окно с количеством труб, 
//    2 кнопка – окно с объёмом всех стен, 
//    3 кнопка – окно с количеством дверей.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_5_1
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            int pipeQty = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_PipeCurves)
                .WhereElementIsNotElementType()
                .GetElementCount();

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

            int doorQty = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .WhereElementIsNotElementType()
                .GetElementCount();

            TaskDialog.Show("Результат", $"Трубы = {pipeQty.ToString()}{Environment.NewLine}Стены = {wallVolumeTotal.ToString()}{Environment.NewLine}Двери = {doorQty.ToString()}");

            return Result.Succeeded;
        }
    }
}