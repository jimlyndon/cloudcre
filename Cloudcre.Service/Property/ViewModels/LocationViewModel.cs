namespace Cloudcre.Service.Property.ViewModels
{
    public class LocationViewModel
    {
        public LocationViewModel(string label, string category)
        {
            this.label = label;
            this.category = category;
        }
        public string label { get; set; }
        public string category { get; set; }
    }
}
