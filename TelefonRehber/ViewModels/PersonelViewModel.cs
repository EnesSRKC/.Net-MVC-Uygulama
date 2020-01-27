using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelefonRehber.Models.EntityFramework;

namespace TelefonRehber.ViewModels
{
    public class PersonelViewModel
    {
        public IEnumerable<TBL_DEPARTMAN> Departmanlar { get; set; }
        public TBL_PERSONEL Personel { get; set; }
    }
}