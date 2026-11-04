-- Kiểm tra dữ liệu Lab6 trong SQL Server localhost, database School.
USE School;
GO
SELECT * FROM dbo.Major ORDER BY MajorID;
SELECT * FROM dbo.Learner ORDER BY LearnerID;
SELECT * FROM dbo.Course ORDER BY CourseID;
SELECT * FROM dbo.Enrollment ORDER BY EnrollmentID;
GO
SELECT m.MajorID, m.MajorName, COUNT(l.LearnerID) AS SoLearner,
       CEILING(COUNT(l.LearnerID) / 3.0) AS SoTrangVoiPageSize3
FROM dbo.Major AS m
LEFT JOIN dbo.Learner AS l ON l.MajorID = m.MajorID
GROUP BY m.MajorID, m.MajorName
ORDER BY m.MajorID;
GO
