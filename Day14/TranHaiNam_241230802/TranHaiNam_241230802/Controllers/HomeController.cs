using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TranHaiNam_241230802.Data;
using TranHaiNam_241230802.Models;
using TranHaiNam_241230802.ViewModels;

namespace TranHaiNam_241230802.Controllers;

public class HomeController : Controller
{
    private readonly QLNhaSachContext _db;

    public HomeController(QLNhaSachContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(int? maTL)
    {
        var model = await LayDanhSach(maTL);
        if (model == null) return NotFound();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LocSach(int? maTL)
    {
        var model = await LayDanhSach(maTL);
        if (model == null) return NotFound();
        return PartialView("TranHaiNam_MainContent", model);
    }

    private async Task<DanhSachSachViewModel?> LayDanhSach(int? maTL)
    {
        var query = _db.Sach.AsNoTracking().Where(s => s.Gia.HasValue);
        var tieuDe = "Trần Hải Nam";

        if (maTL.HasValue)
        {
            var theLoai = await _db.TheLoai.AsNoTracking()
                .FirstOrDefaultAsync(t => t.MaTL == maTL.Value);

            if (theLoai == null) return null;
            tieuDe = theLoai.TenTL;
            query = query.Where(s => s.MaTL == maTL.Value);
        }
        else
        {
            query = query.Where(s => s.Gia >= 100);
        }

        return new DanhSachSachViewModel
        {
            TieuDe = tieuDe,
            DanhSach = await query.OrderBy(s => s.MaSach).ToListAsync()
        };
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await NapTheLoai();
        return View(new ThemSachViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ThemSachViewModel model)
    {
        if (model.MaTL.HasValue &&
            !await _db.TheLoai.AnyAsync(t => t.MaTL == model.MaTL.Value))
        {
            ModelState.AddModelError(nameof(model.MaTL), "Thể loại không tồn tại.");
        }

        if (!ModelState.IsValid)
        {
            await NapTheLoai(model.MaTL);
            return View(model);
        }

        var sach = new Sach
        {
            MaTL = model.MaTL!.Value,
            TenSach = model.TenSach!.Trim(),
            Gia = model.Gia!.Value,
            Anh = model.Anh!.Trim()
        };

        _db.Sach.Add(sach);
        await _db.SaveChangesAsync();

        TempData["ThongBao"] = "Đã thêm sách thành công.";
        return RedirectToAction(nameof(Index), new { maTL = sach.MaTL });
    }

    private async Task NapTheLoai(int? maTL = null)
    {
        var theLoai = await _db.TheLoai.AsNoTracking()
            .OrderBy(t => t.MaTL).ToListAsync();

        ViewBag.TheLoai = new SelectList(theLoai, "MaTL", "TenTL", maTL);
    }
}
