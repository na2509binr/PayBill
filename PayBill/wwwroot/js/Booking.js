function incrementHandler(tableClass, index) {
    const numberElements = document.querySelectorAll(`.number`);
    const priceElements = document.querySelectorAll(`.price`);
    const totalElements = document.querySelectorAll(`.total`);

    return function () {
        let currentNumber = parseInt(numberElements[index].textContent);
        currentNumber++;
        numberElements[index].textContent = currentNumber;

        let currentPrice = parseInt(priceElements[index].textContent.replace(/,/g, ''));
        let currentTotal = currentPrice * currentNumber;
        totalElements[index].textContent = currentTotal.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
        calculateBill(tableClass);
    };
}

function decrementHandler(tableClass, index) {
    const numberElements = document.querySelectorAll(`.number`);
    const priceElements = document.querySelectorAll(`.price`);
    const totalElements = document.querySelectorAll(`.total`);

    return function () {
        let currentNumber = parseInt(numberElements[index].textContent);
        if (currentNumber > 0) {
            currentNumber--;
        }
        numberElements[index].textContent = currentNumber;

        let currentPrice = parseInt(priceElements[index].textContent.replace(/,/g, ''));
        let currentTotal = currentPrice * currentNumber;
        totalElements[index].textContent = currentTotal.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
        calculateBill(tableClass);
    };
}

function calculateBill(tableClass) {
    const totals = document.querySelectorAll(`#popup .${tableClass} tbody tr td .total`);
    let sum = 0;

    // Tính tổng giá trị của các món ăn
    totals.forEach(total => {
        const value = parseInt(total.textContent.replace(/,/g, ''));
        if (!isNaN(value)) {
            sum += value;  // Cộng dồn giá trị các món ăn
        }
    });

    document.querySelector(`#popup .${tableClass} tfoot tr td .bill`).textContent = sum.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
}

function addEventClick(tableClass) {
    const incrementButtons = document.querySelectorAll('.increment');
    const decrementButtons = document.querySelectorAll('.decrement');
    const vatCheckBox = document.querySelectorAll('.vat-check-box');
    incrementButtons.forEach((button, index) => {
        button.addEventListener('click', incrementHandler(tableClass, index));
    });

    decrementButtons.forEach((button, index) => {
        button.addEventListener('click', decrementHandler(tableClass, index));
    });
    vatCheckBox.forEach((checkbox, index) => {
        checkbox.addEventListener('change', function () {
            var totalBillDom = document.querySelector(`#popup .${tableClass} tfoot tr td .bill`);
            var valueTotalBill = parseInt(totalBillDom.textContent.replace(/,/g, ''));

            if (this.checked) {
                var currentValue = Math.round(valueTotalBill * 1.1);
            }
            else {
                var currentValue = Math.round(valueTotalBill / 1.1);
            }
            totalBillDom.textContent = currentValue.toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });;
            //totalBillDom.textContent = currentValue;
        });
    });
}

function removeEventClick(tableClass) {
    const incrementButtons = document.querySelectorAll('.increment');
    const decrementButtons = document.querySelectorAll('.decrement');

    incrementButtons.forEach((button) => {
        const newButton = button.cloneNode(true);
        button.replaceWith(newButton);
    });

    decrementButtons.forEach((button) => {
        const newButton = button.cloneNode(true);
        button.replaceWith(newButton);
    });
};

var i = 0;
function openPopup(tableClass, desk) {
    // Lấy tất cả các thẻ có class "clickable"
    const divs = document.querySelectorAll(`.${tableClass} .chose`);

    // Thêm sự kiện click cho từng thẻ
    divs.forEach(div => {
        div.addEventListener('click', function () {
            // Xóa class "slt" khỏi tất cả các thẻ
            divs.forEach(d => d.classList.remove('slt'));

            // Thêm class "slt" cho thẻ vừa được click
            this.classList.add('slt');
        });
    });


    removeEventClick(tableClass);
    addEventClick(tableClass);

    document.querySelector('.titleBackdrop').innerHTML = '';
    document.querySelector('.titleBackdrop').innerHTML = 'Danh sách món ' + desk;
    document.getElementById('backdrop').style.display = 'block';
    document.getElementById('popup').style.display = 'block';
    document.querySelector(`.${tableClass}`).style.display = 'inline-table';
}

function closePopup() {
    document.getElementById('backdrop').style.display = 'none';
    document.getElementById('popup').style.display = 'none';
    document.querySelectorAll('#dataGridPopup').forEach(dataGrid => {
        dataGrid.style.display = 'none';
    });

} 
document.querySelectorAll('#pay').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault(); // Ngăn chặn hành động mặc định nếu nút nằm trong form
        const tableId = this.getAttribute('data-table');

        if (document.querySelector(`#popup .dataGridPopup${tableId} tfoot .bill`).textContent == '0') {
            const element = document.querySelector('.lblMesErr');
            element.classList.remove('animeErr');
            void element.offsetWidth;
            element.style.display = 'block';
            element.textContent = "Vui lòng chọn món trước khi thanh toán!";
            element.classList.add('animeErr');
            element.style.opacity = '0';
            return;
        } else if (!document.querySelector(`#popup .dataGridPopup${tableId} tfoot .slt`)) {
            const element = document.querySelector('.lblMesErr');
            element.classList.remove('animeErr');
            void element.offsetWidth;
            element.style.display = 'block';
            element.textContent = "Vui lòng chọn phương thức thanh toán!";
            element.classList.add('animeErr');
            element.style.opacity = '0';
            return;
        }
        const html = document.querySelector(`.dataGridPopup${tableId}`).innerHTML;
        var billInput = document.querySelector(`#popup .dataGridPopup${tableId} tfoot .bill`).textContent;
        var idTable = document.querySelector('.acti').dataset.table;
        var paymentMethod = document.querySelector(`.dataGridPopup${tableId} .slt`).id;
        const data = {
            html: html,
            billInput: billInput,
            idTable: idTable,
            paymentMethod: paymentMethod
        };
        fetch('/api/v1/GetReceiptDetails', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data) 
        })
            .then(response => response.json())
            .then(data => {
                console.log('Success:', data);
                if (data.message == "Thanh toán thành công!") {
                    console.log(1);
                    closePopup();
                    document.querySelectorAll(`.dataGridPopup${tableId} .number`).forEach(number => { 
                        number.textContent = "0";
                    });
                    document.querySelectorAll(`.dataGridPopup${tableId} .total`).forEach(number => {
                        number.textContent = "0";
                    });
                    document.querySelector(`.dataGridPopup${tableId} .bill`).textContent = "0";

                    const element = document.querySelector('.lblMesSuc');
                    element.classList.remove('animeSuc');
                    void element.offsetWidth;
                    element.style.display = 'block';
                    element.textContent = data.message;
                    element.classList.add('animeSuc');
                    element.style.opacity = '0';

                    document.querySelectorAll(`.dataGridPopup${tableId} .chose`).forEach(slt => {
                        slt.classList.remove('slt');
                    });
                } 
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    });
});


// Khi nhấn nút "Xem trước Hóa Đơn"
document.querySelectorAll('.view-invoice').forEach(button => {
    button.addEventListener('click', function (event) {
        event.preventDefault();
        const tableId = this.getAttribute('data-table'); // Lấy ID bảng

        // Lấy thông tin món ăn từ popup hiện tại
        const selectedDishes = [];

        const rows = document.querySelectorAll(`#popup .dataGridPopup${tableId} tbody tr`);
        rows.forEach(row => {
            const dishName = row.querySelector('td:nth-child(2)').textContent; // Tên món
            const dishPrice = parseFloat(row.querySelector('.price').textContent.replace('đ', '').trim()); // Giá món
            const quantity = parseInt(row.querySelector('.number').textContent); // Số lượng
            const total = parseFloat(row.querySelector('.total').textContent.replace('đ', '').trim()); // Thành tiền

            if (quantity > 0) { // Chỉ lấy những món có số lượng > 0
                selectedDishes.push({ dishName, quantity, dishPrice, total });
            }
        });
        const paymentMethod = document.querySelector(`.dataGridPopup${tableId} .slt`).id;
        // Hiển thị thông tin vào popup "Xem trước hóa đơn"
        const invoiceItemsContainer = document.getElementById('invoice-items');
        const totalAmountElement = document.getElementById('total-amount');

        // Dọn sạch nội dung cũ
        invoiceItemsContainer.innerHTML = '';

        let totalAmount = 0;
        selectedDishes.forEach((dish, index) => {
            totalAmount += dish.total;

            const row = document.createElement('tr');
            row.classList.add('item');
            row.innerHTML = `
                <td>${index + 1}</td>
                <td>${dish.dishName}</td>
                <td>${dish.quantity}</td>
                <td>${dish.dishPrice}đ</td>
                <td>${dish.total}đ</td>
            `;
            invoiceItemsContainer.appendChild(row);
        });

        // Cập nhật tổng thanh toán
        //totalAmountElement.textContent = totalAmount + 'đ';

        // Hiển thị popup "Xem trước hóa đơn"
        document.getElementById('invoice-popup').style.display = 'flex';
    });
});

// Đóng popup khi nhấn ra ngoài
document.getElementById('invoice-popup').addEventListener('click', function (event) {
    // Nếu click vào vùng ngoài của popup, đóng popup
    if (event.target === this) {
        closeInvoicePopup();
    }
});

// Hàm đóng popup Xem hóa đơn
function closeInvoicePopup() {
    const popup = document.getElementById('invoice-popup');
    popup.style.display = 'none'; // Ẩn popup
}


function formatNumber() {
    document.querySelectorAll('.price').forEach(price => {
        if (price) 
            price.innerText = parseFloat(price.innerText).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
        else 
            price.innerText = '0';
    });
    document.querySelectorAll('.total').forEach(total => {
        if (total)
            total.innerText = parseFloat(total.innerText).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
        else
            total.innerText = '0';
    });
    document.querySelectorAll('.bill').forEach(bill => {
        if (bill)
            bill.innerText = parseFloat(bill.innerText).toLocaleString('en-US', { minimumFractionDigits: 0, maximumFractionDigits: 2 });
        else
            bill.innerText = '0';
    });
}
formatNumber();



    


var buttons = document.querySelectorAll("#tbl");
buttons.forEach(function (button) {
    button.addEventListener("click", function () {
        var activeButtons = document.querySelectorAll(".acti");
        activeButtons.forEach(function (activeButton) {
            activeButton.classList.remove("acti");
        });
        button.classList.add("acti");
    });
});

function sortTable(columnIndex) {
    const table = document.getElementById("dataGrid");
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

function sortTablePopup(columnIndex) {
    const table = document.getElementById("dataGridPopup");
    const tbody = table.tBodies[0];
    const rows = Array.from(tbody.rows);
    const headerCell = table.rows[0].cells[columnIndex];
    const isAscending = headerCell.classList.toggle("asc");

    Array.from(headerCell.parentElement.cells).forEach(cell => {
        if (cell !== headerCell) {
            cell.classList.remove("asc", "desc");
        }
    });

    headerCell.classList.toggle("desc", !isAscending);

    rows.sort((rowA, rowB) => {
        var cellA = '';
        var cellB = '';
        if (rowA.cells[2]) {
            cellA = rowA.cells[columnIndex].textContent.replace(/,/g, '').replace(/đ/g, '').trim();
            cellB = rowB.cells[columnIndex].textContent.replace(/,/g, '').replace(/đ/g, '').trim();
        } else if (rowA.cells[4]) {
            cellA = rowA.cells[columnIndex].textContent.replace(/,/g, '').replace(/đ/g, '').trim();
            cellB = rowB.cells[columnIndex].textContent.replace(/,/g, '').replace(/đ/g, '').trim();
        } else {
            cellA = rowA.cells[columnIndex].textContent.trim();
            cellB = rowB.cells[columnIndex].textContent.trim();
        }

        if (!isNaN(cellA) && !isNaN(cellB)) {
            return isAscending ? cellA - cellB : cellB - cellA;
        }

        return isAscending ? cellA.localeCompare(cellB) : cellB.localeCompare(cellA);
    });

    rows.forEach(row => tbody.appendChild(row));
}