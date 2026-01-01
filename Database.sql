CREATE TABLE departments (id SERIAL PRIMARY KEY, name VARCHAR)

INSERT INTO departments (name) VALUES ('Computer Sci.'),('I.T'),('Electrical'),('Mechanical')

CREATE TABLE employee (
id SERIAL PRIMARY KEY,
DeptId INT NOT NULL REFERENCES departments(id),
name VARCHAR NOT NULL,
DateOfJoin date NOT NULL,
salary NUMERIC (12,2) NOT NULL,
YOE INT
)

INSERT INTO employee (DeptId, name, DateOfJoin, salary, YOE) VALUES
(1, 'Talha', '2020-01-15', 50000, 3),
(1, 'Sufiyan', '2019-03-20', 55000, 4),
(1, 'Noman', '2025-07-10', 48000, 2),
(2, 'Yash', '2018-05-25', 60000, 5),
(2, 'Soham', '2025-09-14', 52000, 3),
(2, 'Neel', '2021-11-02', 47000, 2),
(3, 'Samik', '2025-04-18', 65000, 6),
(3, 'Manav', '2019-12-01', 58000, 4),
(3, 'faruk', '2021-06-22', 50000, 2),
(4, 'Khalid', '2018-08-30', 61000, 5),
(4, 'yusuf', '2020-02-17', 53000, 3),
(4, 'Sakib', '2021-10-05', 49000, 2),
(1, 'Faiyaz', '2022-01-20', 47000, 1),
(2, 'Riyaz', '2022-03-15', 46000, 1),
(3, 'Waqar', '2022-05-10', 48000, 1)


-- 1st Query Get department-wise total employee count
SELECT d.name,COUNT(e.id)
FROM employee e
JOIN departments d
ON e.deptid=d.id
GROUP BY d.name

--2nd Query Get employees who joined in the last 1 year
SELECT name,salary,YOE,DateOfJoin FROM employee WHERE DateOfJoin >=(CURRENT_DATE - 365);

--3rd Query Get average salary department-wise
SELECT d.name,AVG(salary)
FROM employee e
JOIN departments d
ON e.deptid=d.id
GROUP BY d.name

--4th Query Find employees earning above their department’s average salary
SELECT e.name AS Employee,d.name AS Department
FROM employee e
JOIN departments d
ON e.deptid=d.id
WHERE e.salary > (SELECT AVG(salary) FROM employee WHERE deptid=d.id)
GROUP BY d.name,e.name

--5th Query Get department with the maximum number of employees
SELECT d.name,COUNT(e.id) AS employee_count
FROM employee e
JOIN departments d
ON e.deptid=d.id
GROUP BY d.name 
ORDER BY employee_count DESC
LIMIT 1
