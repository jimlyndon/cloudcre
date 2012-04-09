using System;
using Cloudcre.Model;

namespace Cloudcre.Service.Property.ViewModels
{
    [Serializable]
    public class OfficeViewModel : BuildingPropertyViewModel
    {
        public override PropertyType PropertyType
        {
            get { return PropertyType.Office; }
        }
    }
}