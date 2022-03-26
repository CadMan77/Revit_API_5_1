//Создайте приложение с графическим окном. В окне разместите три кнопки:
// - 1 кнопка по нажатию должна выводить новое окно с количество труб;
// - 2 кнопка по нажатию должна выводить новое окно с объёмом всех стен;
// - 3 кнопка по нажатию должна выводить новое окно с количество дверей.
//Доработайте приложение таким образом, чтобы при закрытии каждого из трех "дочерних" окон автоматически вызывалось "головное" окно приложения.

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