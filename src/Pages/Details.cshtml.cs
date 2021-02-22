using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Annotorious_RazorPages_EFCore_Sample.Annotorious;

namespace Annotorious_RazorPages_EFCore_Sample.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Data.SampleContext _context;

        public DetailsModel(Data.SampleContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Guid Id { get; set; }

        // Here you will normally return user id and name used in loging to the website. For this sample I hardcoded the values.
        public Guid CurrentUserId => new Guid("60c5169b-0120-46dc-8050-ed3d17cf9ade");
        //public Guid CurrentUserId => new Guid(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value);
        public string CurrentUserName => "Gerardo Lijs";
        //public string CurrentUserName => User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value;

        public IActionResult OnGet(Guid? id)
        {
            if (id is null) return NotFound();

            Id = id.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostLoadAnnotations()
        {
            var annotations = await _context.PanoramaAnnotations.Where(x => x.PanoramaId == Id).Include(x => x.PanoramaAnnotationItems).ThenInclude(x => x.CreatedUser).ToListAsync();
            return new JsonResult(annotations.ParseToAnnotationJson());
        }

        public async Task<IActionResult> OnPostCreateAnnotation([FromBody] AnnotationJson data)
        {
            var annotation = data.ParseToPanoramaAnnotation();

            // Save
            var analysisToUpdate = await _context.Panoramas.Where(x => x.PanoramaId == Id).FirstOrDefaultAsync();
            analysisToUpdate.PanoramaAnnotations.Add(annotation);
            await _context.SaveChangesAsync();

            return new EmptyResult();
        }

        public async Task<IActionResult> OnPostUpdateAnnotation([FromBody] AnnotationJson data)
        {
            var annotation = data.ParseToPanoramaAnnotation();

            // Get current
            var currentAnnotation = await _context.PanoramaAnnotations.Where(x => x.PanoramaId == Id).Include(x => x.PanoramaAnnotationItems).FirstOrDefaultAsync();

            // Merge
            currentAnnotation.BoundingBoxLeft = annotation.BoundingBoxLeft;
            currentAnnotation.BoundingBoxTop = annotation.BoundingBoxTop;
            currentAnnotation.BoundingBoxWidth = annotation.BoundingBoxWidth;
            currentAnnotation.BoundingBoxHeight = annotation.BoundingBoxHeight;

            // NB: Annotorius provides utc date in create but then it requires local time for display so we get a local time in update.
            // Fix UTC date problem with updates. Another approach could be to handle all CreateDateTime in database directly.
            foreach (var item in annotation.PanoramaAnnotationItems)
            {
                if (currentAnnotation.PanoramaAnnotationItems.Any(x => x.AnnotationId == item.AnnotationId))
                {
                    item.CreatedDateTime = item.CreatedDateTime.ToUniversalTime();
                }
            }

            currentAnnotation.PanoramaAnnotationItems = annotation.PanoramaAnnotationItems;

            // Save
            await _context.SaveChangesAsync();

            return new EmptyResult();
        }

        public async Task<IActionResult> OnPostDeleteAnnotation([FromBody] AnnotationJson data)
        {
            var annotation = data.ParseToPanoramaAnnotation();

            // Delete
            var annotationToDelete = await _context.PanoramaAnnotations.Where(x => x.PanoramaId == Id && x.AnnotationId == annotation.AnnotationId).FirstOrDefaultAsync();
            if (annotationToDelete is not null)
            {
                _context.PanoramaAnnotations.Remove(annotationToDelete);
                await _context.SaveChangesAsync();
            }
            return new EmptyResult();
        }
    }
}
