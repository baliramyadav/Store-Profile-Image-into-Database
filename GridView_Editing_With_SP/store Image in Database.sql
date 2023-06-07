

create table Student
(
Sid int primary key,
Name varchar(50),
Class int,
Fees Money,
PhotoName varchar(50),
PhotoBinary VarBinary(MAX),
Status bit Not null default 1
)

select * from Student

alter proc sp_selectRecord
(
@Sid int
)
as
begin
set nocount on;
select Sid,Name,Class,Fees,PhotoName,PhotoBinary from Student where Status=1 and Sid=@Sid
end
go

-----
create proc sp_Save_Student
(
@Sid int ,
@Name varchar(50),
@Class int,
@Fees Money,
@PhotoName varchar(50),
@PhotoBinary VarBinary(MAX)

)
as
begin
Insert into Student(Sid,Name,Class,Fees,PhotoName,PhotoBinary) values(@Sid,@Name,@Class,@Fees,@PhotoName,@PhotoBinary)
end
go


select * from Student
----
create proc sp_Update_Student
(
@Sid int ,
@Name varchar(50),
@Class int,
@Fees Money,
@PhotoName varchar(50),
@PhotoBinary VarBinary(MAX)

)
as
begin
Update Student set Name=@Name,Class=@Class,Fees=@Fees,PhotoName=@PhotoName,PhotoBinary=@PhotoBinary where Sid=@Sid
end
go

----
create proc sp_Delete_Student
(
@Sid int
)
as
begin
Update Student Set Status=0 Where Sid=@Sid
end
go

select * from Student