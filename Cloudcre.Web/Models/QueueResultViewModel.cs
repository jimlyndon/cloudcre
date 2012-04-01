using System.Collections.Generic;
using Cloudcre.Service.ViewModels;

namespace Cloudcre.Web.Models
{
    public class QueueResultViewModel
    {
        public IEnumerable<MultipleFamilyViewModel> Properties { get; set; }
    }
}