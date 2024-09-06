
// Hàm để lấy ngày hiện tại và định dạng theo kiểu YYYY-MM-DD
function getCurrentDate() {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Tháng bắt đầu từ 0 nên cần cộng thêm 1
    const day = String(today.getDate()).padStart(2, '0');

    // return `${year}-${month}-${day}`;
    return `${day}/${month}/${year}`;
}
flatpickr("#fromdte", {
    dateFormat: "d/m/Y",
    enableTime: false
});
flatpickr("#todte", {
    dateFormat: "d/m/Y",
    enableTime: false
});
// Gán giá trị ngày hiện tại cho thuộc tính placeholder
document.querySelectorAll("#todte").forEach(dte => {
    dte.value = getCurrentDate();
});

function formatTotalPrice() {
    // Lấy bảng theo ID
    var table = document.getElementById("table1");

    // Lặp qua từng hàng trong tbody
    var rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");
    for (var i = 0; i < rows.length; i++) {
        // Lấy cột thứ 3 (Price)
        var cell = rows[i].getElementsByTagName("td")[2];

        // Lấy giá trị hiện tại của cột
        var price = parseFloat(cell.innerText);

        // Định dạng lại số, ví dụ: thêm dấu phân cách hàng nghìn và 2 chữ số thập phân
        var formattedPrice = price.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });

        // Gán lại giá trị đã định dạng vào cột
        cell.innerText = formattedPrice;
    }
}

function formatMethod() {
    // Lấy bảng theo ID
    var formattedMethod = '';
    var table = document.getElementById("table1");

    // Lặp qua từng hàng trong tbody
    var rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");
    for (var i = 0; i < rows.length; i++) {
        // Lấy cột thứ 3 (Price)
        var cell = rows[i].getElementsByTagName("td")[3];

        // Lấy giá trị hiện tại của cột
        var method = parseFloat(cell.innerText);

        switch (method) {
            case 1:
                formattedMethod = "Tiền mặt";
                break;
            case 2:
                formattedMethod = "Chuyển khoản";
                break;
            case 3:
                formattedMethod = "Credit Card";
                break;
            default:
                formattedMethod = "";
        }

        cell.innerText = formattedMethod;
    }
}

// Gọi hàm để thực thi khi cần
formatTotalPrice();
formatMethod();

// Lấy tất cả các thẻ li trong ul
const listTag = document.querySelectorAll('.tagBar li p');
const listTable = document.querySelectorAll('.carouselTables');

// Hàm xử lý khi nhấp vào thẻ li
function handleClick(event) {
    // Xóa class 'active' khỏi tất cả các thẻ li
    listTag.forEach(item => item.classList.remove('active'));

    // Thêm class 'active' vào thẻ li được nhấp
    event.target.classList.add('active');

    listTable.forEach(item => item.classList.remove('show'));
    const tableToShow = document.getElementById("t" + event.target.id);
    if (tableToShow) {
        tableToShow.classList.add('show');
    }
}

// Gán sự kiện click cho tất cả các thẻ li
listTag.forEach(item => item.addEventListener('click', handleClick));

function sortTable(columnIndex) {
    const table = document.getElementById("table1");
    const rows = Array.from(table.rows).slice(1);
    const isAscending = table.rows[0].cells[columnIndex].classList.toggle("asc");

    rows.sort((rowA, rowB) => {
        const cellA = rowA.cells[columnIndex].textContent.trim();
        const cellB = rowB.cells[columnIndex].textContent.trim();

        if (!isNaN(cellA) && !isNaN(cellB)) {
            return isAscending ? cellA - cellB : cellB - cellA;
        }

        return isAscending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
    });

    rows.forEach(row => table.tBodies[0].appendChild(row));
}

function convertToDate(dateStr) {
    // Chuyển định dạng từ dd/MM/yyyy sang yyyy-MM-dd
    //const formattedDate = dateStr.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$3-$2-$1");
    const [day, month, year] = dateStr.split("/");
    const dateObject = new Date(year, month - 1, day);
    // Sử dụng Date.parse() hoặc new Date() để chuyển đổi
    return dateObject;
}

function fillterDate(divId, tableClass) {
    console.log(1);
    var fromdte = document.getElementById(`${divId}`).querySelector('#fromdte').value;
    var todte = convertToDate(document.getElementById(`${divId}`).querySelector('#todte').value);
    if (fromdte == '')
        fromdte = convertToDate('01/01/0001');
    else
        fromdte = convertToDate(fromdte);

    const rows = document.querySelectorAll(`#${tableClass} tbody tr`);
    rows.forEach(row => {
        if (row.style.display == 'none') {
            row.removeAttribute('style');
        }
    });

    rows.forEach(row => {
        var date = convertToDate(row.getElementsByTagName('td')[1].textContent); // 
        if (date < fromdte || date > todte) {
            row.style.display = 'none';
        }
    });

}