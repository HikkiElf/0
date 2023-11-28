using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoostore.Utils
{
    internal class StateManager
    {
        public static string ProductVendorCode { get; set; }
        public static bool IsEditing { get; set; }
        public static string UserRole { get; set; }
        public static string UserName { get; set; }
        public static string UserSurname { get; set; }
        public static string UserPatronymic { get; set; }
    }
}
