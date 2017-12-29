using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem
{
    public class HelpViewModel
    {
        public string Text => _text;

        private string _text =
            "Программа предназначена для студентов, изучающий курс Математических моделей физических процессов" + Environment.NewLine + Environment.NewLine +
            "Просмотр теории осуществляется во вкладке \"Теория\"." + Environment.NewLine + Environment.NewLine +
            "Во вкладке \"Моделирование\" можно построить траекторию движения замедляющихся нейтронов. Для этого необходимо задать начальные данные и нажать кнопку \"Построить\"" + Environment.NewLine + Environment.NewLine +
            "Во вкладке \"Тестирование\" можно пройти тест по теме. Для этого нужно выбрать тест их списка и нажать \"Начать тестирование\".";
    }
}
