-- Start of DDL Script for Procedure MYSCHEMA.SPMY_DISH_GET
-- Generated 12-Jun-2024 04:19:03 from MYSCHEMA@(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = db11g)))

CREATE OR REPLACE 
PROCEDURE spmy_dish_get (
    p_typeCust in number,
    rs out SYS_REFCURSOR
)
IS
BEGIN
    open rs for
        select a.id_dish, a.dish_name, a.dish_price from Dish a
        where a.is_for_employee = p_typeCust or p_typeCust = -1;
END spmy_dish_get;
/



-- End of DDL Script for Procedure MYSCHEMA.SPMY_DISH_GET

