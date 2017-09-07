using DonationsWebApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonationsWebApplication.web.Models
{
    public class HstoryViewModel
    {
        public IEnumerable<Application> Applications { get; set; }       
    }

}