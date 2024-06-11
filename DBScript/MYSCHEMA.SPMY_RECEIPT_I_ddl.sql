-- Start of DDL Script for Procedure MYSCHEMA.SPMY_RECEIPT_I
-- Generated 12-Jun-2024 04:20:09 from MYSCHEMA@(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = db11g)))

CREATE OR REPLACE 
PROCEDURE spmy_receipt_i (
    p_idReceipt in varchar2,
    p_createdate in varchar2,
    p_totalPrice in number,
    p_result_out out number
)as
    v_count number;
BEGIN
    SELECT COUNT(*) INTO v_count FROM receipt WHERE ID_Receipt = p_idReceipt;
    IF v_count > 0 THEN
        -- Nếu ID_Receipt đã tồn tại, trả về 0
        p_result_out := 0;
    ELSE
        -- Nếu ID_Receipt chưa tồn tại, chèn bản ghi mới
        Insert into receipt(ID_Receipt, Create_Date, Receipt_Price) values (p_idReceipt, p_createdate,p_totalPrice);

        -- Trả về 1
        p_result_out := 1;
    END IF;

    EXCEPTION
    WHEN OTHERS THEN
        -- Xử lý lỗi, trả về -1 nếu có lỗi
        p_result_out := -1;

END;
/



-- End of DDL Script for Procedure MYSCHEMA.SPMY_RECEIPT_I

