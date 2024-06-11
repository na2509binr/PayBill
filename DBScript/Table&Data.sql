CREATE TABLE Dish (
    ID_Dish number(10) ,
    Dish_Name varchar2(200),
    Is_For_Employee number(10),
    Dish_Price number(20)
);

CREATE TABLE Receipt (
    ID_Receipt varchar2(50) ,
    Create_Date varchar2(50),
    Receipt_Price number(20)
);

CREATE TABLE Receipt_Detail (
    ID_Receipt_Detail varchar2(50) ,
    ID_Receipt varchar2(50) ,
    ID_Dish number(10) ,
    Quantity number(10) ,
    Create_Date varchar2(50)
);

CREATE TABLE Employee (
    ID_Employee varchar2(10) ,
    Employee_Name varchar2(200)
);

CREATE TABLE Receipt_Employee (
    ID_Receipt_Employee varchar2(50),
    ID_Receipt varchar2(50),
    ID_Dish number(10) ,
    ID_Employee varchar2(10),
    Quantity number(10),
    Create_Date varchar2(50)
);

insert into employee(ID_Employee, Employee_Name) values ('Emp001', 'Đàm Bá Bằng');
insert into employee(ID_Employee, Employee_Name) values ('Emp011', 'Phạm Việt Huy');
insert into employee(ID_Employee, Employee_Name) values ('Emp061', 'Phạm Như Hạ Uyên');
insert into employee(ID_Employee, Employee_Name) values ('Emp025', 'Đỗ Hữu Hùng');
insert into employee(ID_Employee, Employee_Name) values ('Emp009', 'Phạm Tiến');
insert into employee(ID_Employee, Employee_Name) values ('Emp019', 'Bùi Hải Nam');

insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1001, 'Sushi' , 0, 50000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1002, 'Sashimi' , 0, 40000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1003, 'Rượu Sake' , 0, 60000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1004, 'Mỳ Udon' , 0, 60000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1005, 'Bánh xèo Okonomiyaki' , 0, 40000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1006, 'Bánh nhân bạch tuộc Takoyaki' , 0, 80000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1007, 'Lẩu bò Sukiyaki' , 0, 300000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1008, 'Lẩu bò Shabu' , 0, 400000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1009, 'Cơm cà ri' , 0, 60000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1010, 'Okonomiyaki monjayaki' , 0, 40000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1011, 'Ca hoi trung' , 1, 70000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1012, 'Bach tuoc Nhat' , 1, 60000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1013, 'Ca trang' , 1, 50000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1014, 'Ca trich' , 1, 50000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1015, 'Ca hoi' , 1, 40000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1016, 'Banh my' , 1, 30000);
insert into employee(id_dish , dish_name, is_for_employee , dish_price) values (1017, 'Dau luoc' , 1, 30000);
