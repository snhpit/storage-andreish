(function($) {
	$.fn.zoom = function(options) {
		return this.each(function() {
			if ((typeof options === "object" || !options) && this.tagName === 'IMG') {
				this.style.opacity = 0;
				new Zoom(this, options);
			}
		})
	};

	var Zoom = function(element, options) {
		this.init(element, options);
	};

	Zoom.prototype = {
		options: {
			draggable: true,
			mousewheel: true,
			initHeight: null,
			initWidth: null,
			element: null,
			$element: null,
			$parent: null,
			minWidth: null,
			minHeight: null,
			animateDuration: 200
		},

		domOptions: {
			$zoomIn: null,
			$zoomOut: null
		},

		init: function(element, options) {
			this.setOptions(element, options);
			this.buildButtons();
			this.setDomOptions();
			this.setCSS();
			this.bindEvents();
		},

		setOptions: function(element, options) { /* перетаскивание параметров из метода в метод */ /* зачем data ??? */
			$.extend(true, this.options, options);
			this.options.element = element;
			var $elem = this.options.$element = $(element);
			this.options.initHeight = $elem.height(); // e.target.naturalHeight
			this.options.initWidth = $elem.width();
			this.options.$parent = $elem.parent(); // e.target.parentNode // client height width
			var parentHeight = this.options.$parent.height();
			var parentWidth = this.options.$parent.width();
			var alignFactor =  this.options.initHeight > this.options.initWidth ? this.options.initHeight / parentHeight : this.options.initWidth / parentWidth;
			this.options.minWidth = this.options.width = Math.round(this.options.initWidth / alignFactor);
			this.options.minHeight = this.options.height = Math.round(this.options.initHeight / alignFactor);
			this.options.position = {
				top: this.options.minHeight < this.options.minWidth ? Math.round((this.options.$parent.height() - this.options.minHeight) / 2) : 0,
				left: this.options.minHeight > this.options.minWidth ? Math.round((this.options.$parent.width() - this.options.minWidth) / 2) : 0
			};
			var offset = $elem.offset();
			this.options.offset = {
				top: Math.round(offset.top) + this.options.position.top,
				left: Math.round(offset.left) + this.options.position.left
			};
			this.options.initOffset = {
				top: Math.round(offset.top) + this.options.position.top,
				left: Math.round(offset.left) + this.options.position.left
			};
			this.options.ratio = this.options.minHeight < this.options.minWidth ? this.options.minHeight / this.options.minWidth : this.options.minWidth / this.options.minHeight;
		},

		setDomOptions: function() {
			this.domOptions.$zoomIn = $('#zoomIn');
			this.domOptions.$zoomOut = $('#zoomOut');
		},

		setCSS: function() {
			this.options.$element.css({
				'position': 'absolute',
				'opacity': 1
			});
			this.options.$parent.css({
				'position': 'relative',
				'overflow': 'hidden'
			});

			this.fitElement();

			if (this.options.draggable) {
				this.options.$element.css({
					'cursor': 'move'
				});
			}
		},

		fitElement: function() {
			this.options.width = this.options.minWidth;
			this.options.height = this.options.minHeight;

			this.options.$element.css({
				'width': this.options.minWidth,
				'height': this.options.minHeight
			});
			this.options.$element.offset({
				'top': this.options.initOffset.top,
				'left': this.options.initOffset.left
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

			this.domOptions.$zoomIn.on('mousedown.zoom', function(e) {
				that.mouseDown("zoomIn", e);
				return false;
			}).on('mouseout.zoom mouseup.zoom', function(e) {
					that.mouseUp(e);
					return false;
				});

			this.domOptions.$zoomOut.on('mousedown.zoom', function(e) {
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

			if (this.options.draggable) {
				this.options.$element.on('mousedown.zoom', function(e) {
					if (e.which === 1) {
						that.mouseDown('mouseDrag', e);
						return false;
					}
				});
				$(document).on('mouseup.zoom', function(e) {
					that.mouseDrag(e);
					return false;
				});
			}

			document.ondragstart = function() { return false }
			document.body.onselectstart = function() { return false }
		},

		mouseDown: function(action, e) {
			var that = this;
			this[action](e);

			if (action === "mouseDrag") { return; }
			this.options.mousedownInterval = window.setInterval(function() {
				that[action](e);
			}, this.options.animateDuration);
		},

		mouseUp: function(e) {
			window.clearInterval(this.options.mousedownInterval);
		},

		zoomIn: function(e) {
			var xFactor = this.options.width - this.options.width * this.options.ratio;
			var yFactor = this.options.height - this.options.height * this.options.ratio;

			if (!this.validateSize(xFactor, 'zoomIn') || !this.validatePosition()) { return; }

			this.options.height = Math.round(this.options.height + yFactor);
			this.options.width = Math.round(this.options.width + xFactor);
			this.options.offset.top = Math.round(this.options.offset.top - yFactor / 2);
			this.options.offset.left = Math.round(this.options.offset.left - xFactor / 2);

			this.applyPosition();
		},

		zoomOut: function(e) {
			var xFactor = this.options.width * (1 / this.options.ratio) - this.options.width;
			var yFactor = this.options.height * (1 / this.options.ratio) - this.options.height;

			if (!this.validateSize(xFactor, 'zoomOut') || !this.validatePosition()) { return; }

			this.options.height = Math.round(this.options.height - yFactor);
			this.options.width = Math.round(this.options.width - xFactor);
			this.options.offset.top = Math.round(this.options.offset.top + yFactor / 2);
			this.options.offset.left = Math.round(this.options.offset.left + xFactor / 2);

			this.applyPosition();
		},

		mouseWheel: function(delta, e) {
			delta > 0 ? this.zoomIn(e) : this.zoomOut(e);
		},

		mouseDrag: function(e) {
			var that = this;

			if (e.type === "mousedown") {
				this.options.pos = {};
				that.options.mouseOffsetX = e.pageX;
				that.options.mouseOffsetY = e.pageY;
				$(document).on('mousemove.zoom', function(e) {
					that.mouseDrag(e);
					return false;
				});
			}

			if (e.type === 'mousemove') {
				var x = this.options.mouseOffsetX - e.pageX;
				var y = this.options.mouseOffsetY - e.pageY;
				if (Math.abs(x) < 2 && Math.abs(y) < 2) { return; }

				this.options.pos.x = this.options.offset.left - x;
				this.options.pos.y = this.options.offset.top - y;

				var shiftX = this.options.pos.x < 0 ? -this.options.position.left : this.options.position.left;
				var shiftY = this.options.pos.y < 0 ? -this.options.position.top : this.options.position.top;

				if (this.options.pos.x + this.options.position.left >= this.options.initOffset.left) {
					this.options.pos.x = this.options.initOffset.left;
				}
				if (this.options.pos.x + shiftX + this.options.width <= this.options.initOffset.left + this.options.minWidth) {
					this.options.pos.x = this.options.initOffset.left - (this.options.width - this.options.minWidth);
				}
				if (this.options.pos.y + this.options.position.top >= this.options.initOffset.top) {
					this.options.pos.y = this.options.initOffset.top;
				}
				if (this.options.pos.y + shiftY + this.options.height <= this.options.initOffset.top + this.options.minHeight) {
					this.options.pos.y = this.options.initOffset.top - (this.options.height - this.options.minHeight);
				}

				console.log(this.options.pos.x + " X " + this.options.pos.y + " Y");

				this.options.$element.offset({
					'left': this.options.pos.x,
					'top': this.options.pos.y
				});

				//this.validatePosition();
				//this.applyPosition();
			}

			if (e.type === 'mouseup') {
				$(document).off('mousemove.zoom');
				this.options.offset.left = this.options.pos.x;
				this.options.offset.top = this.options.pos.y;
			}
		},

		validateSize: function(xFactor, action) {
			if (xFactor + this.options.width > this.options.initWidth && action === "zoomIn") {
				this.zoomButtonsCss('$' + action, false);
				return false;
			}
			if (this.options.width - xFactor < this.options.minWidth && action === "zoomOut") {
				this.fitElement();
				this.zoomButtonsCss('$' + action, false);
				return false;
			}
			this.zoomButtonsCss('$zoomOut', true);
			this.zoomButtonsCss('$zoomIn', true);
			return true;
		},

		validatePosition: function() {
			return true;
		},

		applyPosition: function() {
			this.options.$element.css({
				'height': this.options.height,
				'width': this.options.width
			});
			this.options.$element.offset({
				'top': this.options.offset.top,
				'left': this.options.offset.left
			})
		},

		zoomButtonsCss: function(button, enable) {
			enable ? this.domOptions[button].css({ 'opacity': 1 }) : this.domOptions[button].css({ 'opacity': 0.5 });
		}
	};
})(jQuery);