var countdownValue = 10; // Giá trị đếm ngược ban đầu (số giây)

document.addEventListener('DOMContentLoaded', function () {
    startCountdown();
});

function startCountdown() {
    // Bắt đầu đếm ngược và chuyển hướng sau khi kết thúc
    var countdownInterval = setInterval(function () {
        countdownValue--;
        document.getElementById('countdownDisplay').innerHTML = countdownValue;
        if (countdownValue <= 0) {
            clearInterval(countdownInterval);
            window.location.href = '/'; // Thay 'TrangChinh.aspx' bằng đường dẫn thích hợp
        }
    }, 1000); // Đếm ngược mỗi giây (1000ms)
}
