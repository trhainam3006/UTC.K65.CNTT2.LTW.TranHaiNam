using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranHaiNam_241230802.Data;

namespace TranHaiNam_241230802.ViewComponents;

public class TranHaiNam_HeaderViewComponent : ViewComponent
{
    private readonly QLNhaSachContext _db;

    public TranHaiNam_HeaderViewComponent(QLNhaSachContext db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var theLoai = await _db.TheLoai.AsNoTracking()
            .OrderBy(t => t.MaTL).ToListAsync();

        return View("~/Views/Shared/TranHaiNam_Header.cshtml", theLoai);
    }
}
