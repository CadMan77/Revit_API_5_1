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

            //TaskDialog.Show("Результат", $"Трубы = {pipeQty}{Environment.NewLine}Стены = {wallVolumeTotal}{Environment.NewLine}Двери = {doorQty}");

            var window = new MainView(commandData);
            window.ShowDialog();
            return Result.Succeeded;
        }
    }
}