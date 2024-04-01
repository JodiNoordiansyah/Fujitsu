$('#tSupplier').on('click', 'input[type="checkbox"]', function () {
    $('input[type="checkbox"]').not(this).prop('checked', false);
});

$('#openFileDialogButton').on('click', function () {
    // Create an input element of type file
    $('#fileInput').trigger('click');
});

$('#fileInput').on('change', function (e) {
    var filename = e.target.files[0].name;
    $('#filenameTextbox').val(filename);
});

$('#uploadButton').on('click', function () {
    var fileInput = document.getElementById('fileInput');
    var file = fileInput.files[0];

    if (file) {
        var formData = new FormData();
        formData.append('file', file);

        $.ajax({
            url: '/TbMSuppliers/UploadFile', // Change this to your controller's action
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                alert('Data Successfully Uploaded.');
                location.reload();
            },
            error: function (xhr, status, error) {
                alert('Error uploading file:', error);
            }
        });
    } else {
        alert('No file selected.');
        // Handle case when no file is selected
    }
});

$('#ProvinceSrc').change(function () {
    var selectedCountry = $("#ProvinceSrc").val();
    var citiesSelect = $('#CitySrc');
    citiesSelect.empty();
    if (selectedCountry != null && selectedCountry != '') {
        $.ajax({
            type: "GET",
            url: "/TbMSuppliers/City/",
            data: { "Province": selectedCountry },
            success: function (Cities) {
                if (Cities != null && !jQuery.isEmptyObject(Cities)) {
                    citiesSelect.append($('<option/>', {
                        value: null,
                        text: ""
                    }));
                    $.each(Cities, function (index, city) {
                        citiesSelect.append("<option value='" + city.value + "'>" + city.text + "</option>");
                    });
                };
            },
            failure: function (response) {
                alert(response);
            }
        });
    }
});

$('#SearchButton').on('click', function () {
    var supplierCode = document.getElementById('SupplierCodeSrc');
    var province = document.getElementById('ProvinceSrc');
    var city = document.getElementById('CitySrc');

    var dataToSend = {
        param: {
            supplierCode: supplierCode.value,
            province: province.value,
            city:city.value
        }
    };

    $.ajax({
        url: '/TbMSuppliers/Search', // Change this to your controller's action
        type: 'POST',
        data: dataToSend,
        success: function (response) {
            $("#tSupplier tbody").empty();

            // Populate table with new data
            response.forEach(function (item) {
                $("#tSupplier tbody").append(
                    "<tr><td><input class='checkbox' type='checkbox' value='true' />" + "</td><td>"
                    + item.supplierCode + "</td><td>"
                    + item.supplierName + "</td><td>"
                    + item.address + "</td><td>"
                    + item.province + "</td><td>"
                    + item.city + "</td><td>"
                    + item.pic + "</td></tr>"
                );
            });
        },
        error: function (xhr, status, error) {
            alert('Error Search file:', error);
            // Handle error response if needed
        }
    });
});

$('#ClearButton').on('click', function () {
    var supplierCode = document.getElementById('SupplierCodeSrc');
    var province = document.getElementById('ProvinceSrc');
    var city = document.getElementById('CitySrc');

    supplierCode.value = ""
    province.value = ""
    city.value = ""

    var dataToSend = {
        param: {
            supplierCode: supplierCode.value,
            province: province.value,
            city: city.value
        }
    };

    $.ajax({
        url: '/TbMSuppliers/Search', // Change this to your controller's action
        type: 'POST',
        data: dataToSend,
        success: function (response) {
            $("#tSupplier tbody").empty();

            // Populate table with new data
            response.forEach(function (item) {
                $("#tSupplier tbody").append(
                    "<tr><td><input class='checkbox' type='checkbox' value='true' />" + "</td><td>"
                    + item.supplierCode + "</td><td>"
                    + item.supplierName + "</td><td>"
                    + item.address + "</td><td>"
                    + item.province + "</td><td>"
                    + item.city + "</td><td>"
                    + item.pic + "</td></tr>"
                );
            });
        },
        error: function (xhr, status, error) {
            alert('Error Search file:', error);
            // Handle error response if needed
        }
    });
});

$('#AddButton').on('click', function () {
    window.location.href = "/TbMSuppliers/Create";
});

$('#EditButton').on('click', function () {

    var checkboxes = $(".chkBox:checked").first();

    if (checkboxes.length > 0) {
        var row = checkboxes.closest('tr');
        var supplierCode = row.find("td:eq(1)").text();

        window.location.href = "/TbMSuppliers/Edit?id=" + supplierCode.trim();
    }
    else {
        alert("Please select data to edit")
    }
});

$('#DeleteButton').on('click', function () {
    var checkboxes = $(".chkBox:checked").first();

    if (checkboxes.length > 0) {

        if (confirm("Apakah anda yakin ingin mengahapus data ini ?") == true) {
            var row = checkboxes.closest('tr');
            var supplierCode = row.find("td:eq(1)").text();

            $.ajax({
            url: '/TbMSuppliers/Delete', // Change this to your controller's action
            type: 'DELETE',
            data: { "id": supplierCode.trim() },
            success: function (response) {
                alert('Data successfully Deleted.');
                location.reload();
                // Handle success response if needed
            },
            error: function (xhr, status, error) {
                alert('Error Delete:', error);
                // Handle error response if needed
            }
        });

            //window.location.href = "/TbMSuppliers/Delete?id=" + supplierCode.trim();
        } 
    } else {
        alert("Please select data to delete")
    }
});