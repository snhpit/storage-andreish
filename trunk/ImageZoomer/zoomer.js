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
			/*zoomIn: true,
			zoomOut: true,*/  /**/
			draggable: true,
			mousewheel: true,
			initHeight: null,
			initWidth: null,
			element: null,
			$element: null,
			$parent: null,
			minElemWidth: null,
			minElemHeight: null,
			mousedownInterval: null,
			animateDuration: 200
		},

		init: function(element, options) {
			this.setOptions(element, options);
			this.setCSS();
			this.buildButtons();
			this.bindEvents();
		},

		setOptions: function(element, options) { /* перетаскивание параметров из метода в метод */
			$.extend(true, this.options, options);
			this.options.element = element;
			var $elem = this.options.$element = $(element);
			this.options.initHeight = $elem.height();
			this.options.initWidth = $elem.width();
			this.options.$parent = $elem.parent();
			var parentHeight = this.options.$parent.height();
			var parentWidth = this.options.$parent.width();
			var factor =  this.options.initHeight > this.options.initWidth ? this.options.initHeight / parentHeight : this.options.initWidth / parentWidth;
			this.options.minElemWidth = Math.round(this.options.initWidth / factor);
			this.options.minElemHeight = Math.round(this.options.initHeight / factor);
			this.options.pos = {
				top: this.options.minElemHeight < this.options.minElemWidth ? Math.round((this.options.$parent.height() - this.options.minElemHeight) / 2) : 0,
				left: this.options.minElemHeight > this.options.minElemWidth ? Math.round((this.options.$parent.width() - this.options.minElemWidth) / 2) : 0
			};
			var offset = $elem.offset();
			this.options.offset = {
				top: offset.top + this.options.pos.top,
				left: offset.left + this.options.pos.left
			}
		},

		setCSS: function() {
			this.options.$parent.css({ 'position': 'relative' });
			this.fitElement();
			if (this.options.draggable) {
				this.options.$element.css({
					'cursor': 'move'
				});
			}
		},

		fitElement: function() {
			this.options.$element.css({
				'position': 'absolute',
				'top': this.options.pos.top,
				'left': this.options.pos.left,
				'width': this.options.minElemWidth,
				'height': this.options.minElemHeight
			});
		},

		buildButtons: function() {
			this.options.$parent.append($('<div class="noSel" style="position: absolute; right: 3px; bottom: 3px; width: 39px; height: 21px; z-index: 20; opacity: 0.8; background-color: #666666; border-radius: 5px 5px 5px 5px" >' +
				'<div id="zoomOut" class="noSel" style="float: right; margin: 3px; width: 15px; height: 15px; background: url(' + "./ui-small-icons.png" + ') no-repeat scroll 0 -120px transparent; border-radius: 5px 5px 5px 5px; opacity: 0.5"></div>' +
				'<div id="zoomIn" class="noSel" style="margin: 3px; width: 15px; height: 15px; background: url(' + "./ui-small-icons.png" + ') no-repeat scroll 0 -90px transparent; border-radius: 5px 5px 5px 5px;"></div>' +
				'</div>'))
		},

		bindEvents: function() {
			var that = this;

			$('#zoomIn').on('mousedown.zoom', function(e) {
				that.mouseDown("zoomIn", e);
				return false;
			}).on('mouseout.zoom mouseup.zoom', function(e) {
					that.mouseUp(e);
					return false;
				});

			$('#zoomOut').on('mousedown.zoom', function(e) {
				that.mouseDown("zoomOut", e);
				return false;
			}).on('mouseout.zoom mouseup.zoom', function(e) {
					that.mouseUp(e);
					return false;
				});

			if (this.options.mousewheel && typeof this.options.$element.mousewheel === 'function') {
				this.options.$parent.on('mousewheel.zoom', function(e, delta) {
					that.mouseWheel(delta);
					return false;
				});
			}
		},

		mouseDown: function(action, e) {
			this[action](e);
			this.options.mousedownInterval = window.setInterval(function(that, action) {
				that[action](e);
			}, this.options.animateDuration, this, action);
		},

		mouseUp: function(e) {
			window.clearInterval(this.options.mousedownInterval);
		},

		zoomIn: function(e) {
			console.log("zoomIn");
		},

		zoomOut: function(e) {
			console.log("zoomOut");
		},

		mouseWheel: function(delta, e) {
			delta > 0 ? this.zoomIn(e) : this.zoomOut(e);
		},

		mouseDrag: function() {

		}
	};
})(jQuery);