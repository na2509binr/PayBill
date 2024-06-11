using X.PagedList;

namespace PayBill.Models
{
    public class GetDataToViewModel
    {
        public string message { get; set; }
        public Dishes table { get; set; }
        public IPagedList<Tables> tablesList { get; set; }
        public IPagedList<Employee> employeeList { get; set; }
        public IPagedList<Dishes> dishesList { get; set; }
    }
}
