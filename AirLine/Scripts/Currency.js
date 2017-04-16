var currencyList;

$(function () {
    currencyList();
});

//Add new Currency 
function currencyAdd(currency) {
    // Call Web API to add a new product
    $.ajax({
        url: "/api/currency",
        type: 'POST',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(currency),
        success: function (currency) {
            currencyAddRow(currency);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

//Update Currency
function currencyUpdate(currency) {
    var url = "/api/currency/" + currency.currencyId;

    // Call Web API to update currency
    $.ajax({
        url: url,
        type: 'PUT',
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(currency),
        success: function (currency) {
            currencyUpdateInTable(currency);
        },
        error: function (request, message, error) {
            handleException(request, message, error);
        }
    });
}

// Get all currency to display
function currencyList() {
    // Call Web API to get a list of currencys
    $.ajax({
        url: '/api/currency/',
        type: 'GET',
        dataType: 'json',
        success: function (currencys) {
            currencyListSuccess(currencys);
        },
        error: function (request, message, error) {
            handleException(request, message, error);


        }
    });
}


// Delete currency from <table>
function currencyDelete(ctl) {
    var id = $(ctl).data("id");

    if (confirm('Do you want to delete this row?')) {
        // Call Web API to delete a currency
        $.ajax({
            url: "/api/currency/" + id,
            type: 'DELETE',
            success: function (currency) {
                $(ctl).parents("tr").remove();
                alert('Record has been deleted ')
            },
            error: function (request, message, error) {
                handleException(request, message, error);
            }
        });
    }
}
// Update currency in <table>
function currencyUpdateInTable(currency) {
    // Find currency in <table>
    var row = $("#currencyTable button[data-id='" + currency.currencyId + "']")
              .parents("tr")[0];
    // Add changed currency to table
    $(row).after(currencyAddRow(currency));
    // Remove original currency
    $(row).remove();

    // Clear form fields
    formClear();

    // Change Update Button Text
    $("#updateButton").text("Add");
}

function updateClick() {
    // Build Currency object from inputs
    Currency = new Object();
    Currency.ID = $("#currencyid").val();
    Currency.Name = $("#currencyname").val();
    Currency.Country = $("#country").val();
    Currency.Value = $("#rate").val();

    if ($("#updateButton").text().trim() == "Add") {
        currencyAdd(Currency);
    }
    else {
        currencyUpdate(Currency);
    }
}



// Display all currencys returned from Web API call
function currencyListSuccess(currencys) {
    currencyList = currencys;
    // Iterate over the collection of data
    $.each(currencys, function (index, currency) {
        // Add a row to the currency table
        currencyAddRow(currency);
    });
}

// Add currency row to <table>
function currencyAddRow(currency) {
    // First check if a <tbody> tag exists, add one if not
    if ($("#currencyTable tbody").length == 0) {
        $("#currencyTable").append("<tbody></tbody>");
    }

    // Append row to <table>
    $("#currencyTable tbody").append(currencyBuildTableRow(currency));
}

// Build a <tr> for a row of table data
function currencyBuildTableRow(currency) {
    var ret = "<tr>" +
          "<td>" +
            "<button type='button' " +
                "onclick='currencyGet(this);' " +
                "class='btn btn-default' " +
                "data-id='" + currency.ID + "'>" +
                "<span class='glyphicon glyphicon-edit' />" +
            "</button>" +
          "</td>" +
          "<td>" + currency.Name + "</td>" +
          "<td>" + currency.Country + "</td>" +
          "<td>" + currency.Value + "</td>" +
          "<td>" +
            "<button type='button' " +
                    "onclick='currencyDelete(this);' " +
                    "class='btn btn-default' " +
                    "data-id='" + currency.currencyId + "'>" +
                    "<span class='glyphicon glyphicon-remove' />" +
            "</button>" +
          "</td>" +
        "</tr>";

    return ret;
}

function currencyGet(ctl) {
    var id = $(ctl).data("id");
    // Store currency id in hidden field
    $("#currencyid").val(id);
    // Change Update Button Text
    $("#updateButton").text("Edit");
    $.each(currencyList, function (index, currency) {
        // Add a row to the currency table
        if (currency.ID == id) {
            $("#currencyname").val(currency.Name);
            $("#country").val(currency.Country);
            $("#rate").val(currency.Value);

            
        }
    });

}

// Clear form fields
function addClick() {
    formClear();

    // Change Update Button Text
    $("#updateButton").text("Add");
}

function formClear() {
    $("#currencyname").val("");
    $("#country").val("");
    $("#rate").val("");
}



// Handle exceptions from AJAX calls
function handleException(request, message, error) {
    var msg = "";

    msg += "Code: " + request.status + "\n";
    msg += "Text: " + request.statusText + "\n";
    if (request.responseJSON != null) {
        msg += "Message" + request.responseJSON.Message + "\n";
    }

    alert(msg);
}
