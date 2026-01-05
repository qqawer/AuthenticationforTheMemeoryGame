USE [TheMemoryGame];
GO

-- =============================================
-- 1. 插入 Users (模拟用户：保留原有 + 新增20普通用户 + 5VIP用户)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE Id > 3) -- 仅新增未存在的用户
BEGIN
    PRINT '正在插入扩展的 Users 数据...'
    
    -- 开启允许手动插入 ID 的开关
    SET IDENTITY_INSERT [dbo].[Users] ON;

    -- 原有用户（保留）
    INSERT INTO [dbo].[Users] ([Id], [Username], [Password], [IsPaid])
    SELECT 1, N'user', N'123456', 0 WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE Id = 1)
    UNION ALL
    SELECT 2, N'vip', N'password', 1 WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE Id = 2)
    UNION ALL
    SELECT 3, N'user1', N'123456', 0 WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Users] WHERE Id = 3);

    -- 新增20个普通用户 (user2 ~ user21)
    INSERT INTO [dbo].[Users] ([Id], [Username], [Password], [IsPaid])
    VALUES 
    (4, N'user2', N'123456', 0),
    (5, N'user3', N'123456', 0),
    (6, N'user4', N'123456', 0),
    (7, N'user5', N'123456', 0),
    (8, N'user6', N'123456', 0),
    (9, N'user7', N'123456', 0),
    (10, N'user8', N'123456', 0),
    (11, N'user9', N'123456', 0),
    (12, N'user10', N'123456', 0),
    (13, N'user11', N'123456', 0),
    (14, N'user12', N'123456', 0),
    (15, N'user13', N'123456', 0),
    (16, N'user14', N'123456', 0),
    (17, N'user15', N'123456', 0),
    (18, N'user16', N'123456', 0),
    (19, N'user17', N'123456', 0),
    (20, N'user18', N'123456', 0),
    (21, N'user19', N'123456', 0),
    (22, N'user20', N'123456', 0),
    (23, N'user21', N'123456', 0);

    -- 新增5个VIP用户 (vip2 ~ vip6)
    INSERT INTO [dbo].[Users] ([Id], [Username], [Password], [IsPaid])
    VALUES 
    (24, N'vip2', N'password', 1),
    (25, N'vip3', N'password', 1),
    (26, N'vip4', N'password', 1),
    (27, N'vip5', N'password', 1),
    (28, N'vip6', N'password', 1);

    -- 记得关掉开关
    SET IDENTITY_INSERT [dbo].[Users] OFF;
END
ELSE
BEGIN
    PRINT 'Users 表已有扩展数据，跳过用户插入。'
END
GO

-- =============================================
-- 2. 插入 Scores (模拟分数记录：原有 + 10普通用户 + 2VIP用户的分数)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId > 3) -- 仅新增未存在的分数
BEGIN
    PRINT '正在插入扩展的 Scores 数据...'
    
    -- 原有分数（保留）
    INSERT INTO [dbo].[Scores] ([UserId], [CompletionTimeSeconds], [CompleteAt])
    SELECT 1, 120, DATEADD(minute, -30, GETDATE()) WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId = 1 AND CompletionTimeSeconds = 120)
    UNION ALL
    SELECT 1, 110, DATEADD(minute, -10, GETDATE()) WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId = 1 AND CompletionTimeSeconds = 110)
    UNION ALL
    SELECT 1, 95, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId = 1 AND CompletionTimeSeconds = 95)
    UNION ALL
    SELECT 2, 60, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId = 2 AND CompletionTimeSeconds = 60)
    UNION ALL
    SELECT 3, 200, DATEADD(day, -1, GETDATE()) WHERE NOT EXISTS (SELECT 1 FROM [dbo].[Scores] WHERE UserId = 3 AND CompletionTimeSeconds = 200);

    -- 为10个新增普通用户 (user2~user11, ID4~13) 添加分数
    INSERT INTO [dbo].[Scores] ([UserId], [CompletionTimeSeconds], [CompleteAt])
    VALUES 
    (4, 135, DATEADD(hour, -2, GETDATE())),    -- user2: 2小时前，135秒
    (5, 142, DATEADD(hour, -5, GETDATE())),    -- user3: 5小时前，142秒
    (6, 118, DATEADD(day, -1, GETDATE())),     -- user4: 昨天，118秒
    (7, 98, DATEADD(minute, -45, GETDATE())),  -- user5: 45分钟前，98秒
    (8, 156, DATEADD(day, -2, GETDATE())),     -- user6: 2天前，156秒
    (9, 89, DATEADD(minute, -15, GETDATE())),  -- user7: 15分钟前，89秒（高手）
    (10, 178, DATEADD(hour, -8, GETDATE())),   -- user8: 8小时前，178秒
    (11, 105, GETDATE()),                      -- user9: 刚刚，105秒
    (12, 127, DATEADD(minute, -30, GETDATE())),-- user10: 30分钟前，127秒
    (13, 111, DATEADD(hour, -1, GETDATE()));   -- user11: 1小时前，111秒

    -- 为2个新增VIP用户 (vip2、vip3, ID24、25) 添加分数（VIP用户分数普遍更低）
    INSERT INTO [dbo].[Scores] ([UserId], [CompletionTimeSeconds], [CompleteAt])
    VALUES 
    (24, 72, DATEADD(minute, -20, GETDATE())), -- vip2: 20分钟前，72秒
    (25, 65, GETDATE());                       -- vip3: 刚刚，65秒（大神）

END
ELSE
BEGIN
    PRINT 'Scores 表已有扩展数据，跳过分数插入。'
END
GO

-- =============================================
-- 3. 插入 Ads (模拟广告：保留原有数据)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Ads])
BEGIN
    PRINT '正在插入 Ads 数据...'
    
    SET IDENTITY_INSERT [dbo].[Ads] ON;

    INSERT INTO [dbo].[Ads] ([Id], [AdImageUrl], [AdTitle], [IsActive])
    VALUES 
    (1, '/images/ads/ad1.png', 'dog1', 1),
    (2, '/images/ads/ad2.png', 'dog2', 1),
    (3, '/images/ads/ad3.png', 'dog3', 1), -- 这是一个在线占位图，没本地图也能显示
    (4, '/images/ads/ad4.png', 'dog4', 0);            -- IsActive=0，测试不显示过期的广告

    SET IDENTITY_INSERT [dbo].[Ads] OFF;
END
ELSE
BEGIN
    PRINT 'Ads 表已有数据，跳过插入。'
END
GO

PRINT '=== 模拟数据初始化完成 ==='
PRINT '=== 数据统计 ==='
PRINT '普通用户总数：' + CAST((SELECT COUNT(*) FROM [dbo].[Users] WHERE IsPaid = 0) AS VARCHAR);
PRINT 'VIP用户总数：' + CAST((SELECT COUNT(*) FROM [dbo].[Users] WHERE IsPaid = 1) AS VARCHAR);
PRINT '有分数记录的用户数：' + CAST((SELECT COUNT(DISTINCT UserId) FROM [dbo].[Scores]) AS VARCHAR);