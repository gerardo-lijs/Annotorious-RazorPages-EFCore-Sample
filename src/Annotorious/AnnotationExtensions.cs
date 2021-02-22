using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Annotorious_RazorPages_EFCore_Sample.Data.Models;

namespace Annotorious_RazorPages_EFCore_Sample.Annotorious
{
    public static class AnnotationExtensions
    {
        public static IEnumerable<AnnotationJson> ParseToAnnotationJson(this IEnumerable<PanoramaAnnotation> analysisAnnotations) => analysisAnnotations.Select(x => x.ParseToAnnotationJson());
        public static AnnotationJson ParseToAnnotationJson(this PanoramaAnnotation analysisAnnotation) => Parser.ToJson(analysisAnnotation);
        public static PanoramaAnnotation ParseToPanoramaAnnotation(this AnnotationJson annotationJson) => Parser.FromJson(annotationJson);
    }
}
