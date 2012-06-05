$(document).ready(function () {
    var cache = location.hash;
    //        .split(/#%0A/g).join(' ');
    //        .split(/\<.*\>/g).join();
    if (cache) {
        var container = $('#news-section .container');
        container.html('');
        container.append(cache);
    }
    $('#smiles').click(function () {
        $('#news-section .container').load('smiles.htm', function (response, status, xhr) {
            if (status == "error") {
                var msg = "Sorry but there was an error: ";
                alert(msg + xhr.status + " " + xhr.statusText + '. Please check your connection or wait a little bit.');
            }
            if (status == "success") {
                setTimeout(function () {
                    location.hash = $('#news').html();
                }, 100);
            }
        });
    });
    $('.close').live("click", function () {
        $(this).parent().parent().parent().remove();
        location.hash = $('#news').html();
    });
    $(window).unload(function () {
        location.hash = $('#news').html();
    });
});