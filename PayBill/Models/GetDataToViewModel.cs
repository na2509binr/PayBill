using X.PagedList;

namespace PayBill.Models
{
    public class GetDataToViewModel
    {
        public string message { get; set; }
        public Dish table { get; set; }
        public IPagedList<Tables> tablesList { get; set; }
        public IPagedList<Employee> employeeList { get; set; }
        public IPagedList<Dish> dishesList { get; set; }
        public IPagedList<Receipt> receiptList { get; set; }
        public IPagedList<Receipt_Details> receiptDetailList { get; set; }
        public IPagedList<Receipt_Employee> receiptEmployeeList { get; set; }
        public IPagedList<ReceiptEmployeeViewModel> receiptEmployeeModelList { get; set; }
        public IPagedList<Spending> SpendingModelList { get; set; }
    }
}
