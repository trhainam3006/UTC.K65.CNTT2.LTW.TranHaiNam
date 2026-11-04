-- Chạy trên SQL Server (localhost) để kiểm tra 4 bảng được dùng bởi TH_Lab5.
USE School;
GO
SELECT * FROM dbo.Major;
SELECT * FROM dbo.Learner;
SELECT * FROM dbo.Course;
SELECT * FROM dbo.Enrollment;
GO
-- Thống kê số Learner theo Major:
SELECT m.MajorID, m.MajorName, COUNT(l.LearnerID) AS SoLearner
FROM dbo.Major AS m
LEFT JOIN dbo.Learner AS l ON l.MajorID = m.MajorID
GROUP BY m.MajorID, m.MajorName
ORDER BY m.MajorID;
