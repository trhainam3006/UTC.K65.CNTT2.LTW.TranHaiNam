using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2026_De1.Models;

namespace _241230802_TranHaiNam_2026_De1.ViewComponents;

public class TranHaiNam_HeaderViewComponent : ViewComponent
{
    private readonly QLTaiSanContext _db;

    public TranHaiNam_HeaderViewComponent(QLTaiSanContext db)
    {
        _db = db;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cacLoai = await _db.LoaiTaiSan.AsNoTracking()
            .OrderBy(loai => loai.MaLoai).ToListAsync();
        ViewBag.TongTaiSan = await _db.TaiSan.CountAsync();
        var tongGia = await _db.TaiSan.SumAsync(ts => ts.NguyenGia) ?? 0;
        ViewBag.TongGia = tongGia.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));

        return View("~/Views/Shared/TranHaiNam_Header.cshtml", cacLoai);
    }
}
