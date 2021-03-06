---- Delete all of the data
DELETE FROM project_employee;
DELETE FROM project;
DELETE FROM employee;
DELETE FROM department;

--Create a new department
INSERT INTO department (name) VALUES ('bear training');
DECLARE @newDeptId int = (SELECT @@IDENTITY);

--Create new employee
INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date) VALUES (@newDeptId, 'Sarah', 'Watkins', 'manager', '1999-02-03', 'F', '2019-04-05');
DECLARE @newEmployeeId int = (SELECT @@IDENTITY);

INSERT INTO employee (department_id, first_name, last_name, job_title, birth_date, gender, hire_date) VALUES (@newDeptId, 'Mark', 'Jenkins', 'manager', '1999-02-03', 'M', '2019-04-05');
DECLARE @newEmployeeId2 int = (SELECT @@IDENTITY);

--Create new project
INSERT INTO project (name, from_date, to_date) VALUES ('Macrame', '1994-06-08', '1996-01-02');
DECLARE @newProjId int = (SELECT @@IDENTITY);

INSERT INTO project_employee (project_id, employee_id) VALUES (@newProjId, @newEmployeeId2);

SELECT @newDeptId as newDeptId, @newProjId as newProjId, @newEmployeeID as newEmployeeId, @newEmployeeID2 as newEmployeeId2


