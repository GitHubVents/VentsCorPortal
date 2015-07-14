using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authorization;

namespace VentsCorpPortal
{
    class CheckActionCode
    {
        private static int ActionCode { get; set; } 
  
        // Доступ к просомтру каталога Вентиляторы и моторы
        static public bool ViewCatalogueFanAndMotors()
        {
            ActionCode = 1;
            var result = Admin.CheckUsers(ActionCode);
            return result;
        }

        // Редактировать каталог Вентиляторы и моторы
        static public bool UserCanEditCatalogueFanAndMotors()
        {
            ActionCode = 10;
            var result = Admin.CheckUsers(ActionCode);
            return result;
        }

        public static bool UserCanAddLinkToPdm()
        {
            ActionCode = 11;
            var result = Admin.CheckUsers(ActionCode);
            return result;
        }

    }
}
