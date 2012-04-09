using System.Collections.Generic;
using Cloudcre.Service.Property.ViewModels;

namespace Cloudcre.Web.Models
{
    public class QueueResultViewModel
    {
        public IEnumerable<MultipleFamilyViewModel> Properties { get; set; }
    }
}