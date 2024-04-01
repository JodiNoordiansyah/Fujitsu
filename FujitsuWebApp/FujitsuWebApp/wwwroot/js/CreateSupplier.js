$('#Province').change(function () {
    var selectedCountry = $("#Province").val();
    var citiesSelect = $('#City');
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

$("#CreateForm").submit(function (event) {
    event.preventDefault();

    $.ajax({
        url: $(this).attr('action'),
        method: $(this).attr('method'),
        data: $(this).serialize(),
        success: function (response) {
            // Handle success, e.g., show a success message
            alert('Data Successfully Saved.');
            window.location.href = "/TbMSuppliers/Index";
        },
        error: function (xhr, status, error) {
            // Handle error
            alert(xhr.responseText); // Display the error message
        }
    });
});
