using System;
using System.Collections.Generic;

#nullable disable

namespace Annotorious_RazorPages_EFCore_Sample.Data.Models
{
    public partial class PanoramaAnnotation
    {
        public PanoramaAnnotation()
        {
            PanoramaAnnotationItems = new HashSet<PanoramaAnnotationItem>();
        }

        public Guid PanoramaId { get; set; }
        public Guid AnnotationId { get; set; }
        public double BoundingBoxLeft { get; set; }
        public double BoundingBoxTop { get; set; }
        public double BoundingBoxWidth { get; set; }
        public double BoundingBoxHeight { get; set; }

        public virtual Panorama Panorama { get; set; }
        public virtual ICollection<PanoramaAnnotationItem> PanoramaAnnotationItems { get; set; }
    }
}