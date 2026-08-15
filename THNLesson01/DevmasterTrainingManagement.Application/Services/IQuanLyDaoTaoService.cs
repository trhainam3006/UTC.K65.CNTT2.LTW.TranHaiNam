using System;
using System.Collections.Generic;
using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Services
{
    public interface IQuanLyDaoTaoService
    {
        // ==========================================
        // 1. QUẢN LÝ KHÓA HỌC
        // ==========================================
        void ThemKhoaHoc(KhoaHoc khoaHoc);
        List<KhoaHoc> LayDanhSachKhoaHoc();
        KhoaHoc? TimKhoaHocTheoMa(string maKhoaHoc);

        // ==========================================
        // 2. QUẢN LÝ LỚP HỌC
        // ==========================================
        void ThemLopHoc(LopHoc lopHoc);
        List<LopHoc> LayLopSapKhaiGiang();
        List<LopHoc> LayLopDangHoc();

        // ==========================================
        // 3. QUẢN LÝ HỌC VIÊN
        // ==========================================
        void ThemHocVien(HocVien hocVien);
        List<HocVien> TimKiemHocVien(string tuKhoa);
        void NhapHocVienTuCsv(string duongDanFile);
        void XuatHocVienRaCsv(string duongDanFile);

        // ==========================================
        // 4. ĐĂNG KÝ & THANH TOÁN
        // ==========================================
        bool DangKyLopHoc(string maHocVien, string maLop, out string thongBao);
        bool GhiNhanThanhToan(string maDangKy, decimal soTien, out string thongBao);

        // ==========================================
        // 5. CHĂM SÓC HỌC VIÊN
        // ==========================================
        void ThemLichSuChamSoc(LichSuChamSoc log);
        List<LichSuChamSoc> LayLichSuChamSocTheoHocVien(string maHocVien);

        // ==========================================
        // 6. BÁO CÁO LINQ (MỤC 7)
        // ==========================================
        Dictionary<string, int> ThongKeHocVienTheoKhoaHoc();
        Dictionary<string, int> ThongKeHocVienTheoLopHoc();
        List<DangKyKhoaHoc> LayDanhSachHocVienNoHocPhi();
        decimal TinhTongDoanhThu();
        decimal TinhDoanhThuTheoThang(int thang, int nam);
        string TimKhoaHocNhieuHocVienNhat();
        List<LichSuChamSoc> LayLichHenHomNay();
        List<HocVien> LayHocVienLauNgayChuaChamSoc(int soNgayGiadinh);
        double TinhTyLeThanhToanDu();

        // ==========================================
        // 7. LƯU TRỮ DỮ LIỆU
        // ==========================================
        void LuuDuLieuJson();
        void DocDuLieuJson();
    }
}