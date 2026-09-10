
insert into ROLES
values ('Admin', 'Master of all', 1, GETDATE());
GO

update USERS set RoleId = 1 where Id =1
GO