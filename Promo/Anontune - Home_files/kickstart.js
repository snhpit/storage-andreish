jQuery(document).ready(function($){

	$('ul.slideshow').wrap('<div class="slideshow-wrap"><div class="slideshow-inner"></div></div>')
	.each(function(){
		var wrap = $(this).parents('.slideshow-wrap');
		var inner = $(this).parents('.slideshow-inner');

		var swidth = $(this).attr('width');
		var sheight = $(this).attr('height');
		if(swidth != undefined && sheight != undefined){wrap.width(swidth); inner.height(sheight);}
		$(this).width('999em').attr('width','').attr('height','');
	
		$(this).find('li:first').addClass('current');
		$(this).delay(2000).animate({alpha:1}, function(){
			KSslideshow($(this), null);
		});

		var items = $(this).find('li');
		wrap.append('<ul class="slideshow-buttons"></ul>');
		items.each(function(index){
			wrap.find('.slideshow-buttons')
			.append('<li><a href="#slideshow-'+index+'" rel="'+index+'">'+(index+1)+'</a></li>');
		});

		wrap.find('.slideshow-buttons')
		.append('<li class="slideshow-stop"><a href="#slideshow-stop">Stop</a></li>');
		wrap.find('.slideshow-stop a').click(function(e){
			e.preventDefault();
            var next;
            if (!wrap.hasClass('paused')) {
                next = wrap.find('.slideshow li.current');
            }
            else {
                next = null;
            }
			var slideshow = $(this).parents('.slideshow-wrap').find('ul.slideshow');
			KSslideshow(slideshow, next);
		});

		$('.slideshow-buttons li:first').addClass('current');
		wrap.find('.slideshow-buttons li').not('.slideshow-stop').find('a').click(function(e){
			e.preventDefault();
			var slideshow = wrap.find('ul.slideshow');
			var link = $(e.currentTarget).attr('rel');
			var next = slideshow.find('li').eq(link);
			KSslideshow(slideshow, next);
		});
	});

	function KSslideshow(slideshow, next){
		var wrap = slideshow.parents('.slideshow-wrap');
		var inner = slideshow.parents('.slideshow-inner');
		var current = slideshow.find('li.current');
		var nav = slideshow.parents('.slideshow-wrap').find('.slideshow-buttons li');
		var sstop = nav.filter('.slideshow-stop');	
		
		if(next == null) {
			next = current.next();		
			if(next.length < 1) { next = slideshow.find('li:first'); }
			wrap.removeClass('paused');
			sstop.find('a').html('Stop');
		}else{
			wrap.addClass('paused');
			sstop.find('a').html('Play');
		}
		
		var scrollEffect = inner.scrollTo(next, 1200);
		current.removeClass('current');
		next.addClass('current');
		nav.removeClass('current').eq(next.index()).addClass('current');
		slideshow.delay(8000).animate({alpha:1}, function(){
			if(wrap.hasClass('paused') == false){ KSslideshow(slideshow, null);  }
		});
	}
});


