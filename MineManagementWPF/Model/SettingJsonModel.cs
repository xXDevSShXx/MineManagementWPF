using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineManagementWPF.Model
{
    public class SettingJsonModel
    {
        public List<Stop> Stops { get; set; }
    }

    public class Stop
    {
        public string Title { get; set; }

        public List<Item> Items { get; set; }
    }
    public class Item
    {
        public string Title { get; set; }
        public int Code { get; set; }
    }
}
