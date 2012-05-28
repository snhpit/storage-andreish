(function($){
    var counter = 0;
    var canSlide = true;

    $.fn.slide = function(data, numberOfSlides, time) {
        addTempElement();
        slide(numberOfSlides);
    };

    $('ul.slideshow-buttons li').live('click', function(){
        canSlide = false;
        counter = $(this).index();
        $("ul.slideshow-buttons").find('.current').removeClass('current');
        $(this).addClass('current');
        var slideShow = $("ul.slideshow");
        slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000);
    });

    $('.slideshow-wrap').mouseover(function(){
        $('.nav-play').css('opacity', '0.4');
    }).mouseout(function(){
            $('.nav-play').css('opacity', '0');
        });
    /**
     * adding first element to the end of collection of slides for cyclic animation
     */
    function addTempElement() {
        var slideshow = $("ul.slideshow");
        slideshow.append(slideshow.children().first().clone());
    }

    function hoverCurrentButton(index) {
        var liCollection = $('ul.slideshow-buttons li');
        liCollection.eq(index-1).removeClass('current');
        liCollection.eq(index).addClass('current');
    }

    function slide(numberOfSlides) {
        hoverCurrentButton(counter);
        setTimeout(function (){
            if (!canSlide) { return; }
            counter++;
            var slideShow =  $("ul.slideshow");
            slideShow.animate({left: -parseInt(slideShow.children().first().css('width')) * counter + 'px'}, 1000); // not good
            //counter = counter === numberOfSlides ? 0 : counter;
            if (counter === numberOfSlides) {
                counter = 0;
                slideShow.animate({left: '0px'}, 0);
                //$("ul.slideshow").css('left', '0');
            }
            slide(numberOfSlides);
        }, 4000);
    }
})(jQuery);