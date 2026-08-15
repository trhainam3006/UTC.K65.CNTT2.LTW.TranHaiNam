using System;
using System.Text;
using DevmasterTrainingManagement.Application.Services;
using DevmasterTrainingManagement.Domain.Entities;
using DevmasterTrainingManagement.Infrastructure.Logging;

namespace DevmasterTrainingManagement.ConsoleUI
{
    internal class Program
    {
        private static readonly IQuanLyDaoTaoService _dichVuDaoTao = new QuanLyDaoTaoService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            try
            {
                _dichVuDaoTao.DocDuLieuJson();
            }
            catch (Exception ex)
            {
                GhiLogLoi.GhiLoi("Lỗi tải dữ liệu ban đầu từ JSON", ex);
            }

            string luaChon;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("   HỆ THỐNG QUẢN LÝ ĐÀO TẠO DEVMASTER   ");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Phân hệ Quản lý Khóa học");
                Console.WriteLine("2. Phân hệ Quản lý Lớp học");
                Console.WriteLine("3. Phân hệ Quản lý Học viên");
                Console.WriteLine("4. Phân hệ Đăng ký & Học phí");
                Console.WriteLine("5. Phân hệ Chăm sóc học viên");
                Console.WriteLine("6. Báo cáo & Thống kê bằng LINQ");
                Console.WriteLine("0. Thoát chương trình");
                Console.WriteLine("==================================================");
                Console.Write("Mời bạn chọn chức năng: ");
                luaChon = Console.ReadLine() ?? "";

                switch (luaChon)
                {
                    case "1": MenuKhoaHoc(); break;
                    case "2": MenuLopHoc(); break;
                    case "3": MenuHocVien(); break;
                    case "4": MenuDangKyHocPhi(); break;
                    case "5": MenuChamSoc(); break;
                    case "6": MenuBaoCaoLinq(); break;
                    case "0":
                        _dichVuDaoTao.LuuDuLieuJson();
                        Console.WriteLine("Đã tự động lưu dữ liệu. Tạm biệt!");
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ! Ấn phím bất kỳ để chọn lại.");
                        Console.ReadKey();
                        break;
                }
            } while (luaChon != "0");
        }

        // --- CÁC MENU CON ---
        static void MenuLopHoc()
        {
            bool quayLai = false;
            while (!quayLai)
            {
                Console.Clear();
                Console.WriteLine("================ QUẢN LÝ LỚP HỌC ================");
                Console.WriteLine("1. Xem danh sách lớp sắp khai giảng");
                Console.WriteLine("2. Xem danh sách lớp đang học");
                Console.WriteLine("3. Thêm mới lớp học");
                Console.WriteLine("0. Quay lại menu chính");
                Console.WriteLine("=================================================");
                Console.Write("Mời bạn chọn chức năng: ");

                string chon = Console.ReadLine();
                switch (chon)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- DANH SÁCH LỚP SẮP KHAI GIẢNG ---");
                        var dsSapKhaiGiang = _dichVuDaoTao.LayLopSapKhaiGiang();
                        foreach (var lop in dsSapKhaiGiang)
                        {
                            Console.WriteLine($"Mã: {lop.MaLop} | Tên: {lop.TenLop} | Mã KH: {lop.MaKhoaHoc} | Khai giảng: {lop.NgayKhaiGiang:dd/MM/yyyy}");
                        }
                        Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- DANH SÁCH LỚP ĐANG HỌC ---");
                        var dsDangHoc = _dichVuDaoTao.LayLopDangHoc();
                        foreach (var lop in dsDangHoc)
                        {
                            Console.WriteLine($"Mã: {lop.MaLop} | Tên: {lop.TenLop} | Mã KH: {lop.MaKhoaHoc} | Khai giảng: {lop.NgayKhaiGiang:dd/MM/yyyy}");
                        }
                        Console.WriteLine("\nNhấn phím bất kỳ để quay lại...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--- THÊM MỚI LỚP HỌC ---");
                        LopHoc lopMoi = new LopHoc();
                        Console.Write("Nhập mã lớp: ");
                        lopMoi.MaLop = Console.ReadLine();
                        Console.Write("Nhập tên lớp: ");
                        lopMoi.TenLop = Console.ReadLine();
                        Console.Write("Nhập mã khóa học: ");
                        lopMoi.MaKhoaHoc = Console.ReadLine();
                        lopMoi.NgayKhaiGiang = DateTime.Now.AddDays(15); 

                        _dichVuDaoTao.ThemLopHoc(lopMoi);
                        Console.WriteLine("\nThêm lớp học thành công!");
                        Console.ReadKey();
                        break;

                    case "0":
                        quayLai = true;
                        break;

                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void MenuLopHoc() 
        { 
            Console.WriteLine("Chức năng Quản lý Lớp học..."); 
            Console.ReadKey(); 
        }

        static void MenuHocVien()
        {
            Console.Clear();
            Console.WriteLine("=== QUẢN LÝ HỌC VIÊN ===");
            Console.WriteLine("1. Thêm học viên");
            Console.WriteLine("2. Tìm kiếm học viên (Tên, SĐT, Email)");
            Console.WriteLine("3. Xuất danh sách học viên ra CSV");
            Console.Write("Chọn: ");
            var chon = Console.ReadLine();
            if (chon == "1")
            {
                try
                {
                    var h = new HocVien();
                    Console.Write("Mã học viên: "); h.MaHocVien = Console.ReadLine()!;
                    Console.Write("Họ và tên: "); h.HoTen = Console.ReadLine()!;
                    Console.Write("Số điện thoại: "); h.SoDienThoai = Console.ReadLine()!;
                    Console.Write("Email: "); h.Email = Console.ReadLine()!;
                    _dichVuDaoTao.ThemHocVien(h);
                    Console.WriteLine("Thêm học viên thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi nghiệp vụ: {ex.Message}");
                    GhiLogLoi.GhiLoi("Lỗi nghiệp vụ", ex);
                }
            }
            else if (chon == "2")
            {
                Console.Write("Nhập từ khóa cần tìm: ");
                var tuKhoa = Console.ReadLine() ?? "";
                var ketQua = _dichVuDaoTao.TimKiemHocVien(tuKhoa);
                foreach (var h in ketQua) 
                    Console.WriteLine($"[{h.MaHocVien}] {h.HoTen} - SĐT: {h.SoDienThoai} - Email: {h.Email}");
            }
            else if (chon == "3")
            {
                _dichVuDaoTao.XuatHocVienRaCsv("hocvien_export.csv");
                Console.WriteLine("Đã xuất file hocvien_export.csv thành công!");
            }
            Console.ReadKey();
        }

        static void MenuDangKyHocPhi() { Console.WriteLine("Chức năng Đăng ký & Học phí..."); Console.ReadKey(); }
        static void MenuChamSoc() { Console.WriteLine("Chức năng Chăm sóc học viên..."); Console.ReadKey(); }

        static void MenuBaoCaoLinq()
        {
            Console.Clear();
            Console.WriteLine("================ BÁO CÁO LINQ THỐNG KÊ ================");
            Console.WriteLine($"1. Tổng doanh thu hệ thống: {_dichVuDaoTao.TinhTongDoanhThu():N0} VNĐ");
            Console.WriteLine($"2. Khóa học đăng ký nhiều nhất: {_dichVuDaoTao.TimKhoaHocNhieuHocVienNhat()}");
            Console.WriteLine($"3. Tỷ lệ học viên đã đóng đủ học phí: {_dichVuDaoTao.TinhTyLeThanhToanDu():F2}%");
            Console.WriteLine($"4. Số lượng học viên còn nợ tiền học phí: {_dichVuDaoTao.LayDanhSachHocVienNoHocPhi().Count}");
            Console.WriteLine("=========================================================");
            Console.ReadKey();
        }
    }
}