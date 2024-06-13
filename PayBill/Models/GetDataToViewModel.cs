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
    }
}
