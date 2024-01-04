(function ($) {
    var btn = $('#btnTop');

    $(window).scroll(function () {
        if ($(window).scrollTop() > 300) {
            btn.addClass('show');
        } else {
            btn.removeClass('show');
        }
    });

    btn.on('click', function (e) {
        e.preventDefault();
        $('html, body').animate({ scrollTop: 0 });
    });

    //Navbar slide
    var prevScrollpos = window.scrollY;
    window.onscroll = function () {
        var currentScrollPos = window.scrollY;
        if (prevScrollpos > currentScrollPos) {
            document.getElementById("header-navbar").style.top = "0";
        } else {
            document.getElementById("header-navbar").style.top = "-84px";
        }
        prevScrollpos = currentScrollPos;
    }

    //Canvas Menu
    $(".canvas__open").on('click', function () {
        $(".offcanvas-menu-wrapper").addClass("active");
        $(".offcanvas-menu-overlay").addClass("active");
        $(".header").removeClass("fixed-top");
    });

    $(".offcanvas-menu-overlay, .offcanvas__close").on('click', function () {
        $(".offcanvas-menu-wrapper").removeClass("active");
        $(".offcanvas-menu-overlay").removeClass("active");
        $(".header").addClass("fixed-top");
    });

    $(document).ready(function () {
        $('#updateCart').on('click', function () {
            var updates = [];

            $('.quantity-input').each(function () {
                var productId = $(this).data('product-id');
                var quantity = $(this).val();
                
                updates.push({ productId: productId, quantity: quantity });
            });
            
            $.ajax({
                url: '/cart/update',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(updates),
                success: function (result) {
                    updateProductList(result.cartItems);
                    $('#totalAmount').html(result.totalAmount.toLocaleString('vi-VN') + ' &#8363;');
                    console.log(result);
                },
                error: function (error) {
                    console.error(error);
                }
            });
        });
    });

    $(document).ready(function () {
        $('.removeItem').on('click', function () {
            var productId = $(this).data('product-id');

            $.ajax({
                url: '/cart/remove',
                method: 'POST',
                data: { maSp: productId },
                success: function (result) {
                    removeProduct(productId);
                    $('#totalAmount').html(result.totalAmount.toLocaleString('vi-VN') + ' &#8363;');
                    console.log(result);
                },
                error: function (error) {
                    console.error(error);
                }
            });
        })
    });
    
    function updateProductList(cartItems) {
        cartItems.forEach(function (item) {
            var productItem = $('.product-item[data-product-id="' + item.maSp + '"]');
            productItem.find('.quantity-input').val(item.soLuong);
            productItem.find('.cart__total').html(item.thanhTien.toLocaleString('vi-VN') + ' &#8363');
        });
    }

    function removeProduct(item) {
        var productItem = $('.product-item[data-product-id="' + item + '"]');
        productItem.remove();
    }

    // Đợi cho trang được tải hoàn thành
    document.addEventListener('DOMContentLoaded', function () {
        // Lấy thẻ p chứa thông báo giỏ hàng
        var cartMessage = document.querySelector('#cartMessage');

        // Lấy thẻ a thanh toán
        var checkoutLink = document.querySelector('#checkoutLink');

        // Kiểm tra xem có thông báo giỏ hàng hay không
        if (cartMessage && cartMessage.textContent.trim() !== "") {
            // Nếu có thông báo giỏ hàng, thêm thuộc tính không có giá trị để ngăn chặn hành động mặc định của thẻ a
            checkoutLink.setAttribute('href', 'javascript:void(0)');
            
            checkoutLink.addEventListener('click', function (event) {
                event.preventDefault();
                alert("Vui lòng thêm sản phẩm vào giỏ hàng!");
            });
        }
    });
})(jQuery);
