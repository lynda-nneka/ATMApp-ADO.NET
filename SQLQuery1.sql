USE NnekaDb

CREATE TABLE [dbo].[CustomerAccount](
    [UserID] [int] IDENTITY(1,1) NOT NULL,
    [FullName] [varchar](50) NOT NULL,
    [AccountNumber] [varchar](50) NOT NULL,
    [CardNumber] [varchar](50) NOT NULL,
    [Pin] [varchar](10) NOT NULL,
    [Balance] [money] NOT NULL,
    [CreationTimeStamp] [datetime] NOT NULL,
    [UpdateTimeStamp] [datetime] NULL
) ON [PRIMARY]
CREATE TABLE [dbo].[TransactionChannel](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [UserID] [int] NOT NULL,
    [SenderAccountNumber] [varchar](50) NOT NULL,
    [ReceiverAccountNumber] [varchar](50) NOT NULL
) ON [PRIMARY]
CREATE TABLE [dbo].[Transactions](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [TransactionChannelID] [int] NOT NULL,
    [TransactionType] [varchar](50) NOT NULL,
    [Amount] [money] NOT NULL,
    [TransactionStatus] [varchar](50) NOT NULL,
    [AccountBalance] [money] NOT NULL,
    [TransactionTimestamp] [datetime] NOT NULL
) ON [PRIMARY]