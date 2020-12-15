
$('#button').click(function () {
    var blog = $('Blog').val();
    $.ajax({
        url: "writeblog",
        success: function (data) {
            $('sonuc').html(data);
        }
    });
});