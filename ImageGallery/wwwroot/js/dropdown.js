function hideError() {
    $('#errorDisplay').html("")
        .attr('style', 'visibility:hidden');
}

$(function () {
    $('select').on('change', function () {
        var dropdown = this;
        var url = dropdown.getAttribute("imageUrl");
        var array = url.split('/');
        var id = array[array.length - 1];
        var mark = parseInt(dropdown.value);
        $.ajax({
            url: '/api/votes/add',
            type: 'PUT',
            data: JSON.stringify({ imageId: id, mark: mark }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                var response = JSON.parse(xhr.responseText);
                $('#votesCounter').html(response.votesLeft);
            },
            error: function (xhr, status, text) {
                var response = JSON.parse(xhr.responseText);
                $('#errorDisplay').html("Something went wrong: " + response.message)
                    .removeAttr('style', 'visibility:hidden');
                $(dropdown).val("0");
                setTimeout(hideError, 5000);
            }
        });
    });
});

