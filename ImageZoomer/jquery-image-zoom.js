(function($) {
	$.fn.imageZoom = function(method){
		if (methods[method]) {
			return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
		} else if (typeof method === 'object' || !method) {
			return methods.init.apply(this, arguments);
		} else {
			$.error('Method ' +  method + ' does not exist');
		}
	};

	var settings = {
		zoomIn: false,
		zoomOut: false,
		draggable: true,
		mousewheel: true,
		dblclick: true
	};

	var methods = {
		init: function (options) {
			$.extend(settings, options);
			setupCSS.apply(this);
			setupBinds.apply(this);
		},
		zoomIn: function () {
			console.log('wow');

		},
		zoomOut: function () {

		},
		mouseWheel: function (delta) {
			var width = this.width();
			var height = this.height();
			this.width(width + 10 * delta);
			this.height(height + 10 * delta);
		}
	};

	function setupCSS() {
		var parent = this.parent();
		if (parent.css('position') == 'static') {
			parent.css('position', 'relative');
		}
		this.css({
			'position': 'absolute',
			'top': 0,
			'left': 0,
			'width': parent.width(), /*todo: image can have another size*/
			'height': parent.height()
		});
		if (settings.draggable) {
			this.css({
				'cursor': 'move'
			});
		}
	}

	function recalculate() {
		var params = Array.slice(arguments);
		console.log(params[0]);
		console.log(params[1]);
		if (params[0] <= 0) {
			this.css('left', 0 + 'px');
		}
		if (params[1] <= 0) {
			this.css('top', 0 + 'px')
		}
	}

	function setupBinds() {
		var context = this;
		if (settings.zoomIn) {
			settings.zoomIn.on('mousedown', context, function(event){
				event.data.imageZoom('zoomIn');
			})
		}
		if (settings.zoomOut) {

		}
		if (settings.mousewheel && typeof this.mousewheel === 'function') {
			this.parent().on('mousewheel', function(event, delta) {
				event.preventDefault();
				$(this).find('img').imageZoom('mouseWheel', delta);
			});
		}
		if (settings.dblclick) {
			this.on('dblclick', function() {
				methods.zoomIn();
			});
		}
		if (settings.draggable && typeof this.draggable === 'function') {
			this.draggable({
				/*snap: '#pan',
				snapTolerance: $('#pan').width(),*/
				/*refreshPositions: true,*/
				/*start: function(event, ui) {
					ui.helper.data().draggable.options.snapTolerance = ui.helper.width();
				},*/

				stop: function(event, ui) {
					$this = ui.helper;
					this.x > 0 ? $this.css('left', 0 + 'px') : this.x;
					this.y > 0 ? $this.css('top', 0 + 'px') : this.y;
					var right = $this.css('right');
					var bottom = $this.css('bottom');
					parseInt(right, 10) > 0 ? $this.css('right', right + 'px') : right;
					parseInt(bottom, 10) > 0 ? $this.css('bottom', bottom + 'px') : bottom;
					//recalculate.apply(context, [event.target.x, event.target.y]);
				}
			})
		}
	}

})(jQuery);