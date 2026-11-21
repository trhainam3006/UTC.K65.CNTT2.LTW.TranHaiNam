using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2026_De2.ViewModels;
using _241230802_TranHaiNam_2026_De2.Models;

namespace _241230802_TranHaiNam_2026_De2.Controllers;

public class HomeController : Controller
{
    private readonly QLTaiSanContext _db;

    public HomeController(QLTaiSanContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(int? maLoai, bool chiLayDanhSach = false)
    {
        var query = _db.TaiSan.AsNoTracking()
            .Where(ts => ts.NguyenGia != null);

        ViewBag.TieuDe = "Trần Hải Nam";
        if (maLoai.HasValue)
        {
            var loai = await _db.LoaiTaiSan.FindAsync(maLoai.Value);
            if (loai == null) return NotFound();

            ViewBag.TieuDe = loai.TenLoai;
            query = query.Where(ts => ts.MaLoai == maLoai.Value);
        }
        else
        {
            query = query.Where(ts => ts.NguyenGia >= 20);
        }

        var danhSach = await query.OrderBy(ts => ts.MaTS).ToListAsync();
        if (chiLayDanhSach)
            return PartialView("~/Views/Shared/TranHaiNam_MainContent.cshtml", danhSach);

        return View(danhSach);
    }

    private async Task NapLoaiAsync()
    {
        var cacLoai = await _db.LoaiTaiSan.AsNoTracking()
            .OrderBy(loai => loai.MaLoai).ToListAsync();
        ViewBag.CacLoai = new SelectList(cacLoai, "MaLoai", "TenLoai");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var ts = await _db.TaiSan.FindAsync(id);
        if (ts == null) return NotFound();

        await NapLoaiAsync();
        return View(new TaiSanForm
        {
            MaTS = ts.MaTS,
            MaLoai = ts.MaLoai,
            TenTS = ts.TenTS,
            NguyenGia = (long?)ts.NguyenGia,
            Anh = ts.Anh
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, TaiSanForm form)
    {
        var ts = await _db.TaiSan.FindAsync(id);
        if (ts == null) return NotFound();

        form.MaTS = id;
        if (form.MaLoai.HasValue && !await _db.LoaiTaiSan.AnyAsync(loai => loai.MaLoai == form.MaLoai))
            ModelState.AddModelError(nameof(form.MaLoai), "Loại tài sản không tồn tại.");

        if (!ModelState.IsValid)
        {
            await NapLoaiAsync();
            return View(form);
        }

        ts.MaLoai = form.MaLoai!.Value;
        ts.TenTS = form.TenTS!.Trim();
        ts.NguyenGia = form.NguyenGia;
        ts.Anh = form.Anh?.Trim();
        await _db.SaveChangesAsync();
        TempData["ThongBao"] = "Đã cập nhật tài sản.";
        if (ts.NguyenGia < 20)
            return RedirectToAction(nameof(Index), new { maLoai = ts.MaLoai });
        return RedirectToAction(nameof(Index));
    }
}
