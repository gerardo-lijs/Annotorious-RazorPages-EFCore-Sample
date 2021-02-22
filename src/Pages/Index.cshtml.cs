using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Annotorious_RazorPages_EFCore_Sample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Data.SampleContext _context;

        public IndexModel(Data.SampleContext context)
        {
            _context = context;
        }

        public List<Data.Models.Panorama> Panoramas { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Panoramas = await _context.Panoramas.ToListAsync();

            return Page();
        }
    }
}
