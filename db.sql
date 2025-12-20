USE [TheMemoryGame];
GO

-- =============================================
-- 1. 插入 Users (模拟用户)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Users])
BEGIN
    PRINT '正在插入 Users 数据...'
    
    -- 开启允许手动插入 ID 的开关
    SET IDENTITY_INSERT [dbo].[Users] ON;

    INSERT INTO [dbo].[Users] ([Id], [Username], [Password], [IsPaid])
    VALUES 
    (1, N'user', N'123456', 0),    -- 普通用户
    (2, N'vip', N'password', 1),    -- 付费用户
    (3, N'user1', N'123456', 0);      -- 普通用户

    -- 记得关掉开关
    SET IDENTITY_INSERT [dbo].[Users] OFF;
END
ELSE
BEGIN
    PRINT 'Users 表已有数据，跳过插入。'
END
GO

-- =============================================
-- 2. 插入 Scores (模拟分数记录)
-- =============================================
IF NOT EXISTS (SELECT 1 FROM [dbo].[Scores])
BEGIN
    PRINT '正在插入 Scores 数据...'
    
    -- 不需要强制指定 Score 的 ID，让它自动生成即可，但必须指定 UserId
    INSERT INTO [dbo].[Scores] ([UserId], [CompletionTimeSeconds], [CompleteAt])
    VALUES 
    (1, 120, DATEADD(minute, -30, GETDATE())), -- User 1: 30分钟前玩了一次
    (1, 110, DATEADD(minute, -10, GETDATE())), -- User 1: 10分钟前又玩了一次，进步了
    (1, 95, GETDATE()),                        -- User 1: 刚刚玩了一次，更厉害了
    (2, 60, GETDATE()),                        -- User 2: 大神，60秒完成
    (3, 200, DATEADD(day, -1, GETDATE()));     -- User 3: 昨天玩的，很菜
END
ELSE
BEGIN
    PRINT 'Scores 表已有数据，跳过插入。'
END
GO

-- =============================================
-- 3. 插入 Ads (模拟广告)
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