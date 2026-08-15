using System;
using System.Collections.Generic;
using System.Linq;
using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Services
{
    public class QuanLyDaoTaoService : IQuanLyDaoTaoService
    {
        // 1. Dữ liệu mẫu Khóa Học
        private List<KhoaHoc> _danhSachKhoaHoc = new List<KhoaHoc>
        {
            new KhoaHoc
            {
                MaKhoaHoc = "KH01",
                TenKhoaHoc = "Lập trình C# Clean Architecture",
                HocPhi = 5000000,
                SoTiet = 60
            },
            new KhoaHoc
            {
                MaKhoaHoc = "KH02",
                TenKhoaHoc = "Lập trình Web ASP.NET Core API",
                HocPhi = 7500000,
                SoTiet = 90
            },
            new KhoaHoc
            {
                MaKhoaHoc = "KH03",
                TenKhoaHoc = "Thiết kế CSDL SQL Server",
                HocPhi = 4000000,
                SoTiet = 45
            }
        };

        // 2. Dữ liệu mẫu Lớp Học
        private List<LopHoc> _danhSachLopHoc = new List<LopHoc>
        {
            new LopHoc
            {
                MaLop = "L01",
                TenLop = "C# Core K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = new DateTime(2026, 3, 1)
            },
            new LopHoc
            {
                MaLop = "L02",
                TenLop = "ASP.NET Core K02",
                MaKhoaHoc = "KH02",
                NgayKhaiGiang = new DateTime(2026, 4, 15)
            },
            new LopHoc
            {
                MaLop = "L03",
                TenLop = "SQL Basic K01",
                MaKhoaHoc = "KH03",
                NgayKhaiGiang = new DateTime(2026, 5, 10)
            }
        };

        // 3. Dữ liệu mẫu Học Viên
        private List<HocVien> _danhSachHocVien = new List<HocVien>
        {
            new HocVien
            {
                MaHocVien = "HV01",
                HoTen = "Đỗ Lâm",
                SoDienThoai = "0987654321",
                Email = "dolam@gmail.com"
            },
            new HocVien
            {
                MaHocVien = "HV02",
                HoTen = "Nguyễn Văn A",
                SoDienThoai = "0912345678",
                Email = "anguyen@gmail.com"
            },
            new HocVien
            {
                MaHocVien = "HV03",
                HoTen = "Trần Thị B",
                SoDienThoai = "0905111222",
                Email = "btran@gmail.com"
            },
            new HocVien
            {
                MaHocVien = "HV04",
                HoTen = "Lê Hoàng C",
                SoDienThoai = "0933444555",
                Email = "cle@gmail.com"
            }
        };

        // 4. Dữ liệu mẫu Đăng Ký Khóa Học
        private List<DangKyKhoaHoc> _danhSachDangKy = new List<DangKyKhoaHoc>
        {
            new DangKyKhoaHoc
            {
                MaDangKy = "DK01",
                MaHocVien = "HV01",
                MaKhoaHoc = "KH01",
                SoTienDaNop = 5000000,
                DaThanhToanDu = true
            },
            new DangKyKhoaHoc
            {
                MaDangKy = "DK02",
                MaHocVien = "HV02",
                MaKhoaHoc = "KH02",
                SoTienDaNop = 3000000, // Còn nợ tiền
                DaThanhToanDu = false
            },
            new DangKyKhoaHoc
            {
                MaDangKy = "DK03",
                MaHocVien = "HV03",
                MaKhoaHoc = "KH01",
                SoTienDaNop = 5000000,
                DaThanhToanDu = true
            },
            new DangKyKhoaHoc
            {
                MaDangKy = "DK04",
                MaHocVien = "HV04",
                MaKhoaHoc = "KH03",
                SoTienDaNop = 2000000, // Còn nợ tiền
                DaThanhToanDu = false
            }
        };

        // 5. Dữ liệu mẫu Chăm Sóc Học Viên
        private List<ChamSocHocVien> _danhSachChamSoc = new List<ChamSocHocVien>
        {
            new ChamSocHocVien
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV02",
                NgayChamSoc = new DateTime(2026, 2, 10),
                NoiDung = "Nhắc nhở đóng nốt học phí còn lại của khóa KH02",
                GhiChu = "Hẹn đầu tháng 3 nộp đủ"
            },
            new ChamSocHocVien
            {
                MaChamSoc = "CS02",
                MaHocVien = "HV04",
                NgayChamSoc = new DateTime(2026, 2, 12),
                NoiDung = "Hỏi thăm tình hình học tập khóa SQL Server",
                GhiChu = "Học viên phản hồi tốt"
            }
        };

        // ==========================================
        // CÁC HÀM TRUY XUẤT CƠ BẢN (GET ALL)
        // ==========================================
        public List<KhoaHoc> GetAllKhoaHoc() => _danhSachKhoaHoc;
        public List<LopHoc> GetAllLopHoc() => _danhSachLopHoc;
        public List<HocVien> GetAllHocVien() => _danhSachHocVien;
        public List<DangKyKhoaHoc> GetAllDangKy() => _danhSachDangKy;
        public List<ChamSocHocVien> GetAllChamSoc() => _danhSachChamSoc;

        // ==========================================
        // CÁC HÀM XỬ LÝ NGHIỆP VỤ & BÁO CÁO LINQ
        // ==========================================

        // 1. Tìm kiếm học viên theo tên
        public List<HocVien> TimKiemHocVienTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return _danhSachHocVien;

            return _danhSachHocVien
                .Where(h => h.HoTen.Contains(tuKhoa, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // 2. Tính tổng doanh thu hệ thống
        public decimal TinhTongDoanhThu()
        {
            return _danhSachDangKy.Sum(dk => dk.SoTienDaNop);
        }

        // 3. Tìm tên khóa học được đăng ký nhiều nhất
        public string TimKhoaHocNhieuHocVienNhat()
        {
            var result = _danhSachDangKy
                .GroupBy(dk => dk.MaKhoaHoc)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            if (result == null) return "Chưa có dữ liệu";

            var khoaHoc = _danhSachKhoaHoc.FirstOrDefault(k => k.MaKhoaHoc == result.Key);
            return khoaHoc != null ? $"{khoaHoc.TenKhoaHoc} ({result.Count()} lượt đăng ký)" : "Không xác định";
        }

        // 4. Tính tỷ lệ học viên đã đóng đủ học phí (%)
        public double TinhTyLeThanhToanDu()
        {
            if (!_danhSachDangKy.Any()) return 0;

            int soLuongDaThanhToanDu = _danhSachDangKy.Count(dk => dk.DaThanhToanDu);
            return (double)soLuongDaThanhToanDu / _danhSachDangKy.Count * 100;
        }

        // 5. Lấy danh sách học viên còn nợ tiền học phí
        public List<HocVien> LayDanhSachHocVienNoHocPhi()
        {
            var dsMaHocVienNo = _danhSachDangKy
                .Where(dk => !dk.DaThanhToanDu)
                .Select(dk => dk.MaHocVien)
                .Distinct();

            return _danhSachHocVien
                .Where(hv => dsMaHocVienNo.Contains(hv.MaHocVien))
                .ToList();
        }

        // Thêm lớp học mới
        public void ThemLopHoc(LopHoc lopHoc)
        {
            _danhSachLopHoc.Add(lopHoc);
        }

        // Lấy danh sách lớp sắp khai giảng (Ngày khai giảng > Ngày hiện tại)
        public List<LopHoc> LayLopSapKhaiGiang()
        {
            return _danhSachLopHoc
                .Where(l => l.NgayKhaiGiang >= DateTime.Now)
                .ToList();
        }

        // Lấy danh sách lớp đang học (Ngày khai giảng <= Ngày hiện tại)
        public List<LopHoc> LayLopDangHoc()
        {
            return _danhSachLopHoc
                .Where(l => l.NgayKhaiGiang < DateTime.Now)
                .ToList();
        }
    }
}