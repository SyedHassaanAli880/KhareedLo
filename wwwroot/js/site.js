// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    UpdateProductCount();
});

function UpdateProductCount()
{
    var existcook,products;

    var cookiesArray = document.cookie.split(";");

    for (var i = 0; i < cookiesArray.length; ++i) {
        var ValueArray = cookiesArray[i].split("=");
        if (ValueArray[0].trim() == 'cartprods') {
            existcook = ValueArray[1];
        }
    }

    if (existcook != undefined && existcook != "" && existcook != null) {
        products = existcook.split("-");
    }
    else {
        products = [];
    }

    $("#CartProductsCount").html("Cart(" + products.length + ")");
}

showInPopup = (url) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#cartModal .modal-body").html(res);
            $("#cartModal").modal('show');
        }
    });
}