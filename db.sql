USE [TheMemoryGame];
GO

-- TheMemoryGame DB Initialization (Users/Scores/Ads)
SET NOCOUNT ON;

-- 1. Users Table (Regular:1-22 | VIP:23-28)
PRINT 'Initializing Users table...';
SET IDENTITY_INSERT [dbo].[Users] ON;
INSERT INTO [dbo].[Users] ([Id], [Username], [Password], [IsPaid])
VALUES
    (1,  N'user',   N'123456', 0),
    (2,  N'user1',  N'123456', 0),
    (3,  N'user2',  N'123456', 0),
    (4,  N'user3',  N'123456', 0),
    (5,  N'user4',  N'123456', 0),
    (6,  N'user5',  N'123456', 0),
    (7,  N'user6',  N'123456', 0),
    (8,  N'user7',  N'123456', 0),
    (9,  N'user8',  N'123456', 0),
    (10, N'user9',  N'123456', 0),
    (11, N'user10', N'123456', 0),
    (12, N'user11', N'123456', 0),
    (13, N'user12', N'123456', 0),
    (14, N'user13', N'123456', 0),
    (15, N'user14', N'123456', 0),
    (16, N'user15', N'123456', 0),
    (17, N'user16', N'123456', 0),
    (18, N'user17', N'123456', 0),
    (19, N'user18', N'123456', 0),
    (20, N'user19', N'123456', 0),
    (21, N'user20', N'123456', 0),
    (22, N'user21', N'123456', 0),
    (23, N'vip',    N'password', 1),
    (24, N'vip2',   N'password', 1),
    (25, N'vip3',   N'password', 1),
    (26, N'vip4',   N'password', 1),
    (27, N'vip5',   N'password', 1),
    (28, N'vip6',   N'password', 1);
SET IDENTITY_INSERT [dbo].[Users] OFF;

-- 2. Scores Table
PRINT 'Initializing Scores table...';
INSERT INTO [dbo].[Scores] ([UserId], [CompletionTimeSeconds], [CompleteAt])
VALUES
    (1,  120, DATEADD(minute, -30, GETDATE())),
    (1,  110, DATEADD(minute, -10, GETDATE())),
    (1,  95,  GETDATE()),
    (2,  200, DATEADD(day, -1, GETDATE())),
    (3,  135, DATEADD(hour, -2, GETDATE())),
    (4,  142, DATEADD(hour, -5, GETDATE())),
    (5,  118, DATEADD(day, -1, GETDATE())),
    (6,  98,  DATEADD(minute, -45, GETDATE())),
    (7,  156, DATEADD(day, -2, GETDATE())),
    (8,  89,  DATEADD(minute, -15, GETDATE())),
    (9,  178, DATEADD(hour, -8, GETDATE())),
    (10, 105, GETDATE()),
    (11, 127, DATEADD(minute, -30, GETDATE())),
    (12, 111, DATEADD(hour, -1, GETDATE())),
    (23, 60,  GETDATE()),
    (24, 72,  DATEADD(minute, -20, GETDATE())),
    (25, 65,  GETDATE());

-- 3. Ads Table
PRINT 'Initializing Ads table...';
SET IDENTITY_INSERT [dbo].[Ads] ON;
INSERT INTO [dbo].[Ads] ([Id], [AdImageUrl], [AdTitle], [IsActive])
VALUES
    (1, '/images/ads/ad1.png', 'ad1', 1),
    (2, '/images/ads/ad2.png', 'ad2', 1),
    (3, '/images/ads/ad3.png', 'ad3', 1),
    (4, '/images/ads/ad4.png', 'ad4', 0);
SET IDENTITY_INSERT [dbo].[Ads] OFF;

-- Final Stats
PRINT '==================================';
PRINT 'INITIALIZATION COMPLETED!';
PRINT 'Regular Users: ' + CAST((SELECT COUNT(*) FROM [dbo].[Users] WHERE IsPaid = 0) AS VARCHAR);
PRINT 'VIP Users: ' + CAST((SELECT COUNT(*) FROM [dbo].[Users] WHERE IsPaid = 1) AS VARCHAR);
PRINT 'Score Records: ' + CAST((SELECT COUNT(*) FROM [dbo].[Scores]) AS VARCHAR);
PRINT 'Active Ads: ' + CAST((SELECT COUNT(*) FROM [dbo].[Ads] WHERE IsActive = 1) AS VARCHAR);
PRINT '==================================';
SET NOCOUNT OFF;