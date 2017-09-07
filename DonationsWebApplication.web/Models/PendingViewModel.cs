using DonationsWebApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonationsWebApplication.web.Models
{
    public class PendingViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
    }
}