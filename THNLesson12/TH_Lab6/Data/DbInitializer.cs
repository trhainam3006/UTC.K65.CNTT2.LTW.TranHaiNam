using Microsoft.EntityFrameworkCore;
using TH_Lab6.Models;

namespace TH_Lab6.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<SchoolContext>();
        await db.Database.EnsureCreatedAsync();

        // Seed each empty table only. Existing Lab4 records are not overwritten.
        if (!await db.Majors.AnyAsync())
        {
            db.Majors.AddRange(
                new Major { MajorName = "IT" },
                new Major { MajorName = "Economics" },
                new Major { MajorName = "Mathematics" });
            await db.SaveChangesAsync();
        }

        if (!await db.Courses.AnyAsync())
        {
            db.Courses.AddRange(
                new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 },
                new Course { CourseID = 4022, Title = "Microeconomics", Credits = 3 },
                new Course { CourseID = 4041, Title = "Macroeconomics", Credits = 3 });
            await db.SaveChangesAsync();
        }

        if (!await db.Learners.AnyAsync())
        {
            var majors = await db.Majors.ToDictionaryAsync(m => m.MajorName, m => m.MajorID);
            var learners = new List<Learner>();
            if (majors.TryGetValue("IT", out var it))
            {
                learners.Add(new Learner { FirstMidName = "Carson", LastName = "Alexander",
                    EnrollmentDate = new DateTime(2005, 9, 1), MajorID = it });
                learners.Add(new Learner { FirstMidName = "Tuấn Nam", LastName = "Trần",
                    EnrollmentDate = new DateTime(2023, 10, 5), MajorID = it });
                learners.Add(new Learner { FirstMidName = "Linh", LastName = "Nguyễn",
                    EnrollmentDate = new DateTime(2023, 9, 12), MajorID = it });
                learners.Add(new Learner { FirstMidName = "Hoàng", LastName = "Phạm",
                    EnrollmentDate = new DateTime(2023, 9, 16), MajorID = it });
                learners.Add(new Learner { FirstMidName = "Huy", LastName = "Đặng",
                    EnrollmentDate = new DateTime(2023, 10, 1), MajorID = it });
            }
            if (majors.TryGetValue("Economics", out var economics))
            {
                learners.Add(new Learner { FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = new DateTime(2002, 9, 1), MajorID = economics });
                learners.Add(new Learner { FirstMidName = "Hà", LastName = "Nguyễn",
                    EnrollmentDate = new DateTime(2023, 10, 4), MajorID = economics });
                learners.Add(new Learner { FirstMidName = "Minh", LastName = "Trần",
                    EnrollmentDate = new DateTime(2023, 10, 4), MajorID = economics });
                learners.Add(new Learner { FirstMidName = "Bích", LastName = "Vũ",
                    EnrollmentDate = new DateTime(2023, 10, 23), MajorID = economics });
                learners.Add(new Learner { FirstMidName = "An", LastName = "Lê",
                    EnrollmentDate = new DateTime(2023, 9, 17), MajorID = economics });
            }
            if (majors.TryGetValue("Mathematics", out var mathematics))
            {
                learners.Add(new Learner { FirstMidName = "Nam", LastName = "Lê",
                    EnrollmentDate = new DateTime(2023, 10, 4), MajorID = mathematics });
                learners.Add(new Learner { FirstMidName = "Ngọc", LastName = "Vũ",
                    EnrollmentDate = new DateTime(2023, 10, 5), MajorID = mathematics });
                learners.Add(new Learner { FirstMidName = "Duy", LastName = "Đỗ",
                    EnrollmentDate = new DateTime(2023, 9, 20), MajorID = mathematics });
            }
            if (learners.Count > 0)
            {
                db.Learners.AddRange(learners);
                await db.SaveChangesAsync();
            }
        }

        if (!await db.Enrollments.AnyAsync())
        {
            var carson = await db.Learners.FirstOrDefaultAsync(l => l.FirstMidName == "Carson" && l.LastName == "Alexander");
            var meredith = await db.Learners.FirstOrDefaultAsync(l => l.FirstMidName == "Meredith" && l.LastName == "Alonso");
            var courses = await db.Courses.Select(c => c.CourseID).ToListAsync();
            var enrollments = new List<Enrollment>();
            if (carson is not null && courses.Contains(1050))
                enrollments.Add(new Enrollment { LearnerID = carson.LearnerID, CourseID = 1050, Grade = 5.5f });
            if (carson is not null && courses.Contains(4022))
                enrollments.Add(new Enrollment { LearnerID = carson.LearnerID, CourseID = 4022, Grade = 7.5f });
            if (meredith is not null && courses.Contains(1050))
                enrollments.Add(new Enrollment { LearnerID = meredith.LearnerID, CourseID = 1050, Grade = 3.5f });
            if (meredith is not null && courses.Contains(4041))
                enrollments.Add(new Enrollment { LearnerID = meredith.LearnerID, CourseID = 4041, Grade = 7f });
            if (enrollments.Count > 0)
            {
                db.Enrollments.AddRange(enrollments);
                await db.SaveChangesAsync();
            }
        }
    }
}
