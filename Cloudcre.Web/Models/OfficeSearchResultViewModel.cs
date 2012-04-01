using System.Collections.Generic;
using Cloudcre.Service.ViewModels;

namespace Cloudcre.Web.Models
{
    public class OfficeSearchResultViewModel : PropertySearchResultViewModel
    {
        public IEnumerable<OfficeViewModel> Properties { get; set; }
    }
}