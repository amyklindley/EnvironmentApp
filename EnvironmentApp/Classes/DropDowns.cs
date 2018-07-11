using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnvironmentApp.Models;

namespace EnvironmentApp.Classes
{
    public class DropDowns
    {
        private static readonly InvMgmtEntities db = new InvMgmtEntities();

        public static IEnumerable<SelectListItem> ApplicationList()
        {
            return db.Applications.Select(a => new SelectListItem
            {
                Text = a.appCode,
                Value = a.appCode
            });
        }

        public static IEnumerable<SelectListItem> StandardsList()
        {
            return db.Standards.Select(s => new SelectListItem
            {
                Text = s.sName,
                Value = s.sName
            });
        }

    }
}