-- Start of DDL Script for Procedure MYSCHEMA.SPMY_EMPLOYEE_GET
-- Generated 12-Jun-2024 04:19:32 from MYSCHEMA@(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = db11g)))

CREATE OR REPLACE 
PROCEDURE spmy_employee_get (
    rs out SYS_REFCURSOR
)
IS
BEGIN
    open rs for
        select a.id_employee, a.employee_name from employee a;
END spmy_employee_get;
/



-- End of DDL Script for Procedure MYSCHEMA.SPMY_EMPLOYEE_GET

