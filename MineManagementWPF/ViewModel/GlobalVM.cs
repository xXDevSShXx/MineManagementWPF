using MineManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineManagementWPF.ViewModel
{
    public class GlobalVM
    {
        public static User CurrentUser { get; set; }
        public static bool isLoggedIn { get; set; } = false;
    }
}
