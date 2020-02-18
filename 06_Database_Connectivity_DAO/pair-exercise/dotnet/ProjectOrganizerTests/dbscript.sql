---- Delete all of the data
DELETE FROM project_employee;
DELETE FROM project;
DELETE FROM employee;
DELETE FROM department;

--Create a new department
INSERT INTO department (name) VALUES ('bear training');
DECLARE @newDeptId int = (SELECT @@IDENTITY);

