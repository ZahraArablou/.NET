CREATE TABLE [dbo].[BorrowBook] (
    [StudId]    INT  NOT NULL,
    [BookId]    INT  NOT NULL,
    [IssueDate] DATE NOT NULL,
	[ReturnDate] Date null,
    PRIMARY KEY CLUSTERED ([StudId] ASC, [BookId] ASC),
    FOREIGN KEY ([StudId]) REFERENCES [dbo].[Student] ([SId]),
    FOREIGN KEY ([BookId]) REFERENCES [dbo].[Book] ([bId])
);
go

select count(*) FROM BorrowBook  WHERE StudId=@StudId AND ReturnDate is Null;

