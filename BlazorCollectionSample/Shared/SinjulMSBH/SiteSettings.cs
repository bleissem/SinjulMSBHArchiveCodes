using System.ComponentModel.DataAnnotations;

namespace BlazorCollectionSample.Shared.SinjulMSBH
{
    public class SiteSettings
    {
        [Required]
        public string StiteTitle { get; set; }
        [Required]
        public string StiteIcon { get; set; }
    }
}
