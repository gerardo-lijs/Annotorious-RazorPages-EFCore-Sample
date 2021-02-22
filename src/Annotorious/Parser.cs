using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Annotorious_RazorPages_EFCore_Sample.Data.Models;

namespace Annotorious_RazorPages_EFCore_Sample.Annotorious
{
    public enum AnnotationItemTypes
    {
        Comment = 1,
        Reply = 2,
        Tag = 3
    }
    public static class Parser
    {
        public static AnnotationJson ToJson(PanoramaAnnotation analysisAnnotation) => new AnnotationJson()
        {
            context = "http://www.w3.org/ns/anno.jsonld",
            id = $"#{analysisAnnotation.AnnotationId}",
            type = "Annotation",
            body = analysisAnnotation.PanoramaAnnotationItems.Select(x => new AnnotationJson.Body
            {
                type = "TextualBody",
                value = x.Value,
                purpose = Parser.AnnotationItemTypeToPurpose(x.ItemTypeId),
                created = x.CreatedDateTime.ToLocalTime(),
                creator = new AnnotationJson.Creator()
                {
                    id = x.CreatedUserId.ToString(),
                    name = x.CreatedUser.DisplayName
                }
            }).ToArray(),
            target = new AnnotationJson.Target()
            {
                selector = new AnnotationJson.Selector()
                {
                    conformsTo = "http://www.w3.org/TR/media-frags/",
                    type = "FragmentSelector",
                    value = $"xywh=pixel:{analysisAnnotation.BoundingBoxLeft},{analysisAnnotation.BoundingBoxTop},{analysisAnnotation.BoundingBoxWidth},{analysisAnnotation.BoundingBoxHeight}",
                }
            }
        };

        public static PanoramaAnnotation FromJson(AnnotationJson annotationJson)
        {
            var result = new PanoramaAnnotation
            {
                AnnotationId = new Guid(annotationJson.id.Replace("#", null))
            };

            // Bounding Box
            var boxRaw = annotationJson.target.selector.value.Replace("xywh=pixel:", null).Split(',');
            result.BoundingBoxLeft = Convert.ToDouble(boxRaw[0]);
            result.BoundingBoxTop = Convert.ToDouble(boxRaw[1]);
            result.BoundingBoxWidth = Convert.ToDouble(boxRaw[2]);
            result.BoundingBoxHeight = Convert.ToDouble(boxRaw[3]);

            // Items
            result.PanoramaAnnotationItems = annotationJson.body
                .Select(x => new PanoramaAnnotationItem
                {
                    AnnotationId = result.AnnotationId,
                    ItemTypeId = (int)AnnotationPurposeToItemType(x.purpose),
                    Value = x.value,
                    CreatedDateTime = x.created,
                    CreatedUserId = new Guid(x.creator.id)
                })
                .OrderBy(x => x.CreatedDateTime)
                .ToList();

            return result;
        }

        public static AnnotationItemTypes AnnotationPurposeToItemType(string itempurpose) => itempurpose switch
        {
            "commenting" => AnnotationItemTypes.Comment,
            "replying" => AnnotationItemTypes.Reply,
            "tagging" => AnnotationItemTypes.Tag,
            _ => throw new NotImplementedException(),
        };

        public static string AnnotationItemTypeToPurpose(AnnotationItemTypes itemType) => itemType switch
        {
            AnnotationItemTypes.Comment => "commenting",
            AnnotationItemTypes.Reply => "replying",
            AnnotationItemTypes.Tag => "tagging",
            _ => throw new NotImplementedException(),
        };

        public static string AnnotationItemTypeToPurpose(int itemTypeId) => AnnotationItemTypeToPurpose((AnnotationItemTypes)itemTypeId);
    }
}
