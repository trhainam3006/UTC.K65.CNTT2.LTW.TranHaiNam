using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _241230802_TranHaiNam_2023.Models;

namespace _241230802_TranHaiNam_2023.Controllers;

public class HomeController : Controller
{
    private readonly QLHangHoaContext _db;

    public HomeController(QLHangHoaContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(int? maLoai)
    {
        var hang = await LayHang(maLoai);
        return View(hang);
    }

    public async Task<IActionResult> LocTheoLoai(int? maLoai)
    {
        var hang = await LayHang(maLoai);
        return PartialView("TranHaiNam_MainContent", hang);
    }

    private async Task<List<HangHoa>> LayHang(int? maLoai)
    {
        var query = _db.HangHoa.AsNoTracking().Where(h => h.Gia >= 100);
        ViewBag.TieuDe = "Trần Hải Nam";

        if (maLoai.HasValue)
        {
            query = query.Where(h => h.MaLoai == maLoai.Value);
            ViewBag.TieuDe = "New Products";
        }

        return await query.OrderBy(h => h.MaHang).ToListAsync();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await NapLoaiHang();
        return View(new ThemHangHoa());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ThemHangHoa form)
    {
        if (form.MaLoai.HasValue && !await _db.LoaiHang.AnyAsync(l => l.MaLoai == form.MaLoai.Value))
        {
            ModelState.AddModelError(nameof(form.MaLoai), "Loại hàng không tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await NapLoaiHang();
            return View(form);
        }

        var hang = new HangHoa
        {
            MaLoai = form.MaLoai!.Value,
            TenHang = form.TenHang!.Trim(),
            Gia = form.Gia,
            Anh = form.Anh!.Trim()
        };
        _db.HangHoa.Add(hang);
        await _db.SaveChangesAsync();

        TempData["ThongBao"] = "Đã thêm hàng hóa mới.";
        return RedirectToAction(nameof(Index));
    }

    private async Task NapLoaiHang()
    {
        var loai = await _db.LoaiHang.AsNoTracking()
            .OrderBy(l => l.MaLoai)
            .ToListAsync();
        ViewBag.CacLoai = new SelectList(loai, "MaLoai", "TenLoai");
    }
}
