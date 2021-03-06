USE [aaphones2]
GO
/****** Object:  StoredProcedure [billwilson12].[sp_GetMeetings]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [billwilson12].[sp_GetMeetings]
@MeetingType varchar(20) = NULL
AS
SELECT        dbo.meetings.pKey,dbo.meetings.DAY, dbo.meetings.TIME, dbo.meetings.TYPE, dbo.meetings.TOPIC, dbo.meetings.aaGROUP, 
                         dbo.meetings.LOCATION, dbo.meetings.CITY, dbo.locations.EmbedURL, dbo.locations.MapUIRL AS MapURL
FROM            dbo.dayorder INNER JOIN
                         dbo.meetings ON dbo.dayorder.Day = dbo.meetings.DAY INNER JOIN
                         dbo.locations ON dbo.meetings.LOCATION = dbo.locations.Location

WHERE 
    (ISNULL(@MeetingType,'') = '' AND (Type = 'OPEN' OR Type='CLOSED') 
    OR 
    (ISNULL(@MeetingType, '') != '' AND Type = @MeetingType)
    )
ORDER BY dbo.dayorder.sOrder,CONVERT(datetime, dbo.meetings.TIME, 8)
	
GO
/****** Object:  StoredProcedure [billwilson12].[sp_GetMeetingsPrint]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [billwilson12].[sp_GetMeetingsPrint]
@MeetingType varchar(20) = NULL
AS
SELECT        dbo.meetings.DAY, dbo.meetings.TIME, dbo.meetings.TYPE, dbo.meetings.TOPIC, dbo.meetings.aaGROUP, 
                         dbo.meetings.LOCATION, dbo.meetings.CITY
FROM            dbo.dayorder INNER JOIN
                         dbo.meetings ON dbo.dayorder.Day = dbo.meetings.DAY

WHERE 
    (ISNULL(@MeetingType,'') = '' AND (Type = 'OPEN' OR Type='CLOSED') 
    OR 
    (ISNULL(@MeetingType, '') != '' AND Type = @MeetingType)
    )
ORDER BY dbo.dayorder.sOrder,CONVERT(datetime, dbo.meetings.TIME, 8)
	
GO
/****** Object:  StoredProcedure [billwilson12].[sp_NextMeeting]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [billwilson12].[sp_NextMeeting]
AS
DECLARE @checkDate DATETIME
DECLARE @mtgKey int
DECLARE @mtgTime DATETIME
DECLARE @mtgFound BIT = 0

SET @checkDate = DATEADD(hh,2,GetDate())


WHILE @mtgFound = 0
BEGIN
	DECLARE mtgCursor CURSOR FOR  
	SELECT meetings.pkey, CAST(CONVERT(datetime, dbo.meetings.TIME,8) as TIME) AS convTime
	FROM dayorder INNER JOIN meetings ON dayorder.Day = meetings.DAY INNER JOIN locations ON meetings.LOCATION = locations.Location
	WHERE dayorder.sOrder = DATEPART(dw,@checkDate)
	AND CAST(CONVERT(datetime, dbo.meetings.TIME,8) as TIME) > CAST(@checkDate AS TIME)
	ORDER BY convTime
 
	OPEN mtgCursor   
	FETCH NEXT FROM mtgCursor INTO @mtgKey,@mtgTime
	IF @@FETCH_STATUS = 0
	BEGIN
		SET @mtgFound = 1
	END

	IF @@FETCH_STATUS <> 0
	BEGIN
		SET @checkDate = CAST(DATEADD(d,1,@checkDate) AS DATE)
		print @checkdate
	END
	CLOSE mtgCursor   
	DEALLOCATE mtgCursor
END
DECLARE @endTime TIME,
@startTime TIME
SET @endTime = CAST(CONVERT(datetime, @mtgTime,8) as TIME)
SET @checkDate = DATEADD(hh,2,GetDate())
SET @startTime = CAST(@checkDate AS TIME)

SELECT meetings.pKey,meetings.Day,meetings.Time,meetings.Type,meetings.topic,meetings.aaGroup,meetings.location,meetings.city,locations.EmbedURL,
(DateDiff(hh, @startTime, @endTime) % 24) - 1 As hours,
DateDiff(mi, @startTime, @endTime) % 60 As mins
FROM meetings 
INNER JOIN locations ON meetings.LOCATION = locations.Location
WHERE meetings.pKey=@mtgKey




            --If dayOfWeekNo Mod 2 = 0 Then
            --    sqlStr += ",TYPE"
            --Else
            --    sqlStr += ",aaGROUP"
            --End If
    
	   
	    --Dim dayOfWeekNo As Integer = Now.DayOfWeek
        --Dim dayOfWeek As String = ""

        --Dim currTime As DateTime = Now.AddHours(2)
        --Dim meetingTime As DateTime


        --Dim timeDiff As TimeSpan


        --Dim lblText As String = "There's a meeting in just "
        --Dim gotMeeting As Boolean = False
        --Dim dayAdder As Integer = 0


        --While Not gotMeeting
        --    Select Case dayOfWeekNo
        --        Case 0
        --            dayOfWeek = "SUNDAY"
        --        Case 1
        --            dayOfWeek = "MONDAY"
        --        Case 2
        --            dayOfWeek = "TUESDAY"
        --        Case 3
        --            dayOfWeek = "WEDNESDAY"
        --        Case 4
        --            dayOfWeek = "THURSDAY"
        --        Case 5
        --            dayOfWeek = "FRIDAY"
        --        Case 6
        --            dayOfWeek = "SATURDAY"
        --    End Select
        --    Dim sqlStr As String = ""
        --    sqlStr += "SELECT dayorder.sOrder, meetings.DAY, meetings.TIME, meetings.TYPE, meetings.TOPIC, meetings.aaGROUP, "
        --    sqlStr += " meetings.LOCATION, meetings.CITY, locations.EmbedURL, locations.MapUIRL, CONVERT(datetime, dbo.meetings.TIME, 8) AS convTime"
        --    sqlStr += " FROM dayorder INNER JOIN meetings ON dayorder.Day = meetings.DAY INNER JOIN locations ON meetings.LOCATION = locations.Location"
        --    sqlStr += " WHERE meetings.DAY='" + dayOfWeek + "'"
        --    sqlStr += " ORDER BY convTime"
        --    If dayOfWeekNo Mod 2 = 0 Then
        --        sqlStr += ",TYPE"
        --    Else
        --        sqlStr += ",aaGROUP"
        --    End If

        --    Dim dr As SqlDataReader = dg.GetDataReader(sqlStr)

        --    While dr.Read
        --        'If dr("TIME").ToString.Contains("PM") Then
        --        meetingTime = Convert.ToDateTime(dr("TIME")).AddDays(dayAdder)
        --        'Else
        --        'End If
        --        If meetingTime > currTime And Not (dr("aaGROUP").ToString.Contains("MENS")) Then
        --            timeDiff = Convert.ToDateTime(dr("TIME")).AddDays(dayAdder).Subtract(currTime)
        --            lblText += Str(timeDiff.Hours) + " hour(s), " + Str(timeDiff.Minutes) + " minute(s) and " + Str(timeDiff.Seconds) + " second(s)!<br/>"
        --            lblText += "<br/>If you have an desire to stop drinking, the " + dr("aaGroup") + " group in " + dr("CITY") + " has<br/>"
        --            If dr("TYPE") = "OPEN" Then
        --                lblText += "an <u>Open (we ask that all who participate confine their discussion to their problems with alcohol.)</u><br/> " + dr("TOPIC") + " meeting "
        --            Else
        --                lblText += "a <u>Closed (limited to persons who have a desire to stop drinking)</u><br/> " + dr("TOPIC") + " meeting "
        --            End If
        --            lblText += " at " + dr("LOCATION") + " at " + dr("TIME") + "</br>"
        --            lblText += "<br/>There's a map below, and if you need a to talk to someone about your problem, <br/>"
        --            lblText += "or a ride to a meeting, call us at (251)301-6773. We'll come get you<br/>"
        --            If dr("embedURL") Is DBNull.Value Then
        --                iFrameMap.Visible = False
        --                Label2.Visible = True
        --            Else
        --                iFrameMap.Visible = True
        --                Label2.Visible = False
        --                iFrameMap.Attributes("src") = dr("embedURL")
        --            End If
        --            Dim timeStr As String = dr("TIME")
        --            dg.KillReader(dr)
        --            If dg.GetRecordCount("SELECT * FROM meetings WHERE DAY='" + dayOfWeek + "'  AND TIME='" + timeStr + "'") > 1 Then
        --                lblText += "NOTE: There are other meetings scheduled at this time - check <a href='meetings.aspx'>Meeting List</a> for details<br/><br/>"
        --            End If

        --            Label1.Text = lblText
        --            gotMeeting = True
        --            Exit While
        --        End If
        --    End While
        --    dg.KillReader(dr)
        --    If dayOfWeekNo < 7 Then
        --        dayOfWeekNo += 1
        --    Else
        --        dayOfWeekNo = 1
        --    End If
        --    dayAdder += 1
        --    dr = Nothing
        --End While
GO
/****** Object:  Table [dbo].[dayorder]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dayorder](
	[sOrder] [int] NULL,
	[Day] [nvarchar](15) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[events]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[events](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[EventCat] [nvarchar](1024) NULL,
	[EventName] [nvarchar](1024) NULL,
	[EventLink] [nvarchar](1024) NULL,
	[EventLinkText] [nvarchar](1024) NULL,
	[EventCatName] [nvarchar](1024) NULL,
 CONSTRAINT [PK_events] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Groups]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Groups](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[emailKey] [int] NULL,
	[GroupName] [varchar](50) NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[links]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[links](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[LinkText] [nvarchar](255) NULL,
	[LinkURL] [nvarchar](255) NULL,
	[ListOrder] [int] NULL,
	[LinkComment] [varchar](255) NULL,
	[target] [nchar](10) NULL,
 CONSTRAINT [PK_links] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[locations]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[locations](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](255) NULL,
	[MapUIRL] [nvarchar](255) NULL,
	[EmbedURL] [nvarchar](400) NULL,
 CONSTRAINT [PK_locations] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[login]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[login](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](255) NULL,
	[password] [nchar](10) NULL,
	[level] [int] NULL,
	[Name] [varchar](255) NULL,
 CONSTRAINT [PK_login] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[meetings]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[meetings](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[DAY] [nvarchar](255) NULL,
	[TIME] [nvarchar](50) NULL,
	[TYPE] [nvarchar](255) NULL,
	[TOPIC] [nvarchar](255) NULL,
	[aaGROUP] [nvarchar](255) NULL,
	[LOCATION] [nvarchar](255) NULL,
	[CITY] [nvarchar](255) NULL,
 CONSTRAINT [PK_meetings] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[News](
	[pKey] [int] IDENTITY(1,1) NOT NULL,
	[Header] [nvarchar](1024) NULL,
	[NewsText] [varchar](max) NULL,
	[NewsLink] [nvarchar](1024) NULL,
	[LinkText] [nvarchar](1024) NULL,
	[EventKey] [int] NULL,
	[ListOrder] [int] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[pKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [billwilson12].[vw_meetingswloc]    Script Date: 10/12/2015 5:14:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [billwilson12].[vw_meetingswloc]
AS
SELECT        TOP (100) PERCENT dbo.dayorder.sOrder, dbo.meetings.DAY, dbo.meetings.TIME, dbo.meetings.TYPE, dbo.meetings.TOPIC, dbo.meetings.aaGROUP, 
                         dbo.meetings.LOCATION, dbo.meetings.CITY, dbo.locations.EmbedURL, dbo.locations.MapUIRL, CONVERT(datetime, dbo.meetings.TIME, 8) AS convTime
FROM            dbo.dayorder INNER JOIN
                         dbo.meetings ON dbo.dayorder.Day = dbo.meetings.DAY INNER JOIN
                         dbo.locations ON dbo.meetings.LOCATION = dbo.locations.Location
ORDER BY dbo.dayorder.sOrder, CONVERT(datetime, dbo.meetings.TIME, 8)

GO
ALTER TABLE [dbo].[links] ADD  CONSTRAINT [DF_links_Target]  DEFAULT (N'_blank') FOR [LinkComment]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "dayorder (dbo)"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 101
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "locations (dbo)"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 231
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "meetings (dbo)"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 135
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 12
         Width = 284
         Width = 1500
         Width = 1500
         Width = 2160
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2745
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'billwilson12', @level1type=N'VIEW',@level1name=N'vw_meetingswloc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'billwilson12', @level1type=N'VIEW',@level1name=N'vw_meetingswloc'
GO
