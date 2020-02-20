
$(function () {
    $('select').on('change', function () {
        var url = this.getAttribute("imageUrl");
        var array = url.split('/');
        var id = array[array.length - 1];
        console.log(id);
    })
});