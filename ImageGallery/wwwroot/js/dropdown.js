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
        $('select').prop('disabled', 'disabled');

        $.ajax({
            url: '/api/votes/add',
            type: 'PUT',
            data: JSON.stringify({ imageId: id, mark: mark }),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data, status, xhr) {
                var response = JSON.parse(xhr.responseText);
                $('#votesCounter').html(response.votesLeft);

                if (response.votingCompleted) {
                    window.location("/gallery/summary");
                };
            },
            error: function (xhr, status, text) {
                var response = JSON.parse(xhr.responseText);
                $('#errorDisplay').html("Something went wrong: " + response.message)
                    .removeAttr('style', 'visibility:hidden');
                $(dropdown).val("0");
                setTimeout(hideError, 5000);
            }
        }).always(function () {
            $('select').prop('disabled', false);
            console.log('ma da');
        });
    });
});

