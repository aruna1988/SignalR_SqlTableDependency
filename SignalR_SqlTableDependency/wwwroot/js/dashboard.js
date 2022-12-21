
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/dashboardHub").build();

$(function () {
    connection.start().then(function () {
        alert('Connected to dashboardHub');

        InvokeProducts();
        //InvokeSales();
        //InvokeCustomers();

    }).catch(function (err) {
        return console.error(err.toString());
    });
});

// Product
function InvokeProducts() {
	connection.invoke("SendProducts").catch(function (err) {
		return console.error(err.toString());
	});
}

connection.on("ReceivedProducts", function (products) {
    BindProductsToGrid(products);
});
function BindProductsToGrid(products) {
	$('#tblProduct tbody').empty();

	var tr;
	$.each(products, function (index, product) {
		tr = $('<tr/>');
		/*tr.append(`<td>${(index + 1)}</td>`);*/
		tr.append(`<td>${product.name}</td>`);
		tr.append(`<td>${product.mobileNo}</td>`);
		tr.append(`<td>${product.address}</td>`);
		$('#tblProduct').append(tr);
	});
}
