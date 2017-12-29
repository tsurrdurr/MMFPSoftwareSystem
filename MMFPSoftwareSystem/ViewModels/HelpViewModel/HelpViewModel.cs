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
            "Программа предназначена для студентов, изучающий курс Математических моделей физических процессов" + Environment.NewLine +
            "Просмотр теории осуществляется во вкладке \"Теория\"." + Environment.NewLine +
            "";
    }
}
