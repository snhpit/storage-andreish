(function($) {
	$.fn.zoom = function(options) {
		return this.each(function() {
			if ((typeof options === "object" || !options) && this.tagName === 'IMG') {
				new Zoom(this, options);
			}
		})
	};

	var Zoom = function(element, options) {
		this.init(element, options);
	};

	Zoom.prototype = {
		options: {
			zoomIn: true,
			zoomOut: true,
			draggable: true,
			mousewheel: true,
			initOffset: null,
			initPos: null,
			initHeight: null,
			initWidth: null,
			element: null,
			$element: null,
			$parent: null
		},

		init: function(element, options) {
			this.setOptions(element, options);
			this.setCSS();
		},

		setOptions: function(element, options) {
			/* перетаскивание параметров из метода в метод */
			$.extend(true, this.options, options);
			this.options.element = element;
			var $elem = this.options.$element = $(element);
			this.options.initOffsetLeft = $elem.offset();
			this.options.initPos = $elem.position();
			this.options.initHeight = $elem.height();
			this.options.initWidth = $elem.width();
			this.options.$parent = $elem.parent();
		},

		setCSS: function() {
			var $elem = this.options.$element;
			var $parent = this.options.$parent;
			this.options.$parent.css({ 'position': 'relative' });
			$elem.css({
				'position': 'absolute',
				'top': 0,
				'left': 0,
				'width': $parent.width(), /*todo: image can have another size*/
				'height': $parent.height()
			});
			if (this.options.draggable) {
				$elem.css({
					'cursor': 'move'
				});
			}
		}
	}
})(jQuery);