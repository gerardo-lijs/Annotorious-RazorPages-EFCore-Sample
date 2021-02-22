using System;
using System.Collections.Generic;

#nullable disable

namespace Annotorious_RazorPages_EFCore_Sample.Data.Models
{
    public partial class PanoramaAnnotationItem
    {
        public Guid PanoramaId { get; set; }
        public Guid AnnotationId { get; set; }
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public Guid CreatedUserId { get; set; }

        public virtual PanoramaAnnotation PanoramaAnnotation { get; set; }
        public virtual User CreatedUser { get; set; }
    }
}