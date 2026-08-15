using System;
using System.Collections.Generic;
using System.Linq;
using DevmasterTrainingManagement.Domain.Entities;

namespace DevmasterTrainingManagement.Application.Services
{
    public class TrainingService
    {
        // Khởi tạo Mock Data dữ liệu mẫu
        private List<HocVien> _listHocVien = new List<HocVien>
        {
            new HocVien { MaHocVien = "HV01", HoTen = "Nguyễn Văn A", Email = "a@gmail.com", SoDienThoai = "0912345678" },
            new HocVien { MaHocVien = "HV02", HoTen = "Trần Thị B", Email = "b@gmail.com", SoDienThoai = "0987654321" }
        };

        // Hàm lấy danh sách học viên
        public List<HocVien> GetAllHocVien()
        {
            return _listHocVien;
        }

        // Hàm tìm kiếm LINQ
        public List<HocVien> SearchHocVienByName(string keyword)
        {
            return _listHocVien
                .Where(h => h.HoTen.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}