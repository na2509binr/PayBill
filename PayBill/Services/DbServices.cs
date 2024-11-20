using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using PayBill.Data;
using PayBill.DTOs;
using System.Data;

namespace PayBill.Services;

public class DbServices : IDbServices
{
    private readonly string _connectionString;

    public DbServices(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<ReceiptSummaryDto> GetReceiptSummary()
    {
        var receiptSummary = new List<ReceiptSummaryDto>();
        // Gọi StoreceiptSummaryred Procedure GetReceiptSummary
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            using (var cmd = new MySqlCommand("GetReceiptSummary", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;


                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string day = reader.GetString("day");
                        long totalPrice = reader.GetInt64("total_price");
                        long totalCashAmount = reader.GetInt64("total_cash_amount");
                        long totalBankTransfer = reader.GetInt64("total_bank_transfer");
                        long totalCreditCard = reader.GetInt64("total_credit_card");

                        // add vaof lisst
                        receiptSummary.Add(new ReceiptSummaryDto()
                        {
                            Day = day,
                            TotalPrice = totalPrice,
                            TotalCashAmount = totalCashAmount,
                            TotalBankTransfer = totalBankTransfer,
                            TotalCreditCard = totalCreditCard
                        });
                    }
                }
            }
        }

        return receiptSummary;
    }

    public List<SpendingSummaryDto> GetSpendingSummary()
    {
        var spendingSummary = new List<SpendingSummaryDto>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            using (var cmd = new MySqlCommand("GetSpendingSummary", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string spendingDay = reader.GetString("spending_day");
                        long totalSpending = reader.GetInt64("total_spending");
                        long totalWine = reader.GetInt64("total_wine");
                        long totalGlocery = reader.GetInt64("total_glocery");
                        long totalSacrificialOfferings = reader.GetInt64("total_sacrificial_offerings");
                        long totalBillExtract = reader.GetInt64("total_bill_extract");
                        long totalLoanEmployee = reader.GetInt64("total_loan_employee");
                        long totalCsvc = reader.GetInt64("total_csvc");
                        long totalHdd = reader.GetInt64("total_hdd");

                        // add vaof lisst
                        spendingSummary.Add(new SpendingSummaryDto()
                        {
                            SpendingDay = spendingDay,
                            TotalSpending = totalSpending,
                            TotalWine = totalWine,
                            TotalGlocery = totalGlocery,
                            TotalSacrificialOfferings = totalSacrificialOfferings,
                            TotalBillExtract = totalBillExtract,
                            TotalLoanEmployee = totalLoanEmployee,
                            TotalCsvc = totalCsvc,
                            TotalHdd = totalHdd
                        });
                    }
                }
            }
        }

        return spendingSummary;
    }
}
