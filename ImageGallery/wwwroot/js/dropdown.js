function hideError() {
    $('#errorDisplay').html("")
        .attr('style', 'visibility:hidden');
};

$(function () {
    $('select').on('change', function () {
        var url = this.getAttribute("imageUrl");
        var array = url.split('/');
        var id = array[array.length - 1];
        var mark = parseInt(this.value);
        $.ajax({
            url: '/api/votes/add',
            type: 'PUT',
            data: JSON.stringify({ imageId: id, mark: mark }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
            },
            error: function (xhr, status, text) {
                var response = JSON.parse(xhr.responseText);
                $('#errorDisplay').html("Something went wrong: " + response.message)
                    .removeAttr('style', 'visibility:hidden');

                setTimeout(hideError, 5000);
            }
        }); 
    })
});

