$(function ($) {

    $('ul.slideshow')
        .each(function () {
            var wrap = $(this).parents('.slideshow-wrap');
            var inner = $(this).parents('.slideshow-inner');

            $(this).find('li:first').addClass('current');
            $(this).delay(2000).animate({ a:1 /**/ }, function () {
                slideshowRun($(this), null);
            });

            wrap.find('.slideshow-stop a').click(function (e) {
                e.preventDefault();
                var next;
                if (!wrap.hasClass('paused')) {
                    next = wrap.find('.slideshow li.current');
                }
                else {
                    next = null;
                }
                var slideshow = $(this).parents('.slideshow-wrap').find('ul.slideshow');
                slideshowRun(slideshow, next);
            });

            $('.slideshow-buttons li:first').addClass('current');
            wrap.find('.slideshow-buttons li').not('.slideshow-stop').find('a').click(function (e) {
                e.preventDefault();
                var slideshow = wrap.find('ul.slideshow');
                var link = $(e.currentTarget).attr('rel');
                var next = slideshow.find('li').eq(link);
                slideshowRun(slideshow, next);
            });
        });

    function slideshowRun(slideshow, next) {
        var wrap = slideshow.parents('.slideshow-wrap');
        var inner = slideshow.parents('.slideshow-inner');
        var current = slideshow.find('li.current');
        var nav = slideshow.parents('.slideshow-wrap').find('.slideshow-buttons li');
        var pause = nav.filter('.slideshow-stop');

        if (next == null) {
            next = current.next();
            if (next.length < 1) {
                next = slideshow.find('li:first');
            }
            wrap.removeClass('paused');
            pause.find('a').html('Stop');
        } else {
            wrap.addClass('paused');
            pause.find('a').html('Play');
        }

        var scrollEffect = inner.scrollTo(next, 1200);

        current.removeClass('current');
        next.addClass('current');
        nav.removeClass('current').eq(next.index()).addClass('current');
        slideshow.delay(8000).animate({ a:1 }, function () {
            if (wrap.hasClass('paused') == false) {
                slideshowRun(slideshow, null);
            }
        });
    }
});


