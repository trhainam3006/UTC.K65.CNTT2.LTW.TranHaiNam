using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2023.Models;

namespace _241230802_TranHaiNam_2023.ViewComponents;

public class TranHaiNam_HeaderViewComponent : ViewComponent
{
    private readonly QLHangHoaContext _db;

    public TranHaiNam_HeaderViewComponent(QLHangHoaContext db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var danhSach = await _db.LoaiHang.AsNoTracking()
            .OrderBy(loai => loai.MaLoai)
            .ToListAsync();

        return View("~/Views/Shared/TranHaiNam_Header.cshtml", danhSach);
    }
}
