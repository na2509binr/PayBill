-- Start of DDL Script for Procedure MYSCHEMA.SPMY_RECEIPT_EMPLOYEE_I
-- Generated 12-Jun-2024 04:20:55 from MYSCHEMA@(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = db11g)))

CREATE OR REPLACE 
PROCEDURE spmy_receipt_employee_i (
    p_idReceiptEmployee in varchar2,
    p_idReceipt in varchar2,
    p_idDish in number,
    p_idEmployee in varchar2,
    p_quantity in number,
    p_createdate in varchar2,
    p_result_out out number
)IS
BEGIN
    p_result_out := 1;
    INSERT INTO receipt_employee(ID_Receipt_Employee ,ID_Receipt, ID_Dish, ID_Employee, Quantity, Create_Date)
    VALUES  (p_idReceiptEmployee, p_idReceipt, p_idDish, p_idEmployee, p_quantity, p_createdate);

    EXCEPTION
    WHEN OTHERS THEN
        -- Xử lý lỗi, trả về -1 nếu có lỗi
        p_result_out := 0;
END spmy_receipt_employee_i;
/



-- End of DDL Script for Procedure MYSCHEMA.SPMY_RECEIPT_EMPLOYEE_I

