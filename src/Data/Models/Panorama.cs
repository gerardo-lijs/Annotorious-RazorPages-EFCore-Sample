using System;
using System.Collections.Generic;

#nullable disable

namespace Annotorious_RazorPages_EFCore_Sample.Data.Models
{
    public partial class Panorama
    {
        public Panorama()
        {
            PanoramaAnnotations = new HashSet<PanoramaAnnotation>();
        }

        public Guid PanoramaId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PanoramaAnnotation> PanoramaAnnotations { get; set; }
    }
}