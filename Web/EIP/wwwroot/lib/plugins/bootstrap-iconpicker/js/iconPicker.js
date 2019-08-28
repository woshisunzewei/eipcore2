/*
 * Bootstrap 3.3.6 IconPicker - jQuery plugin for Icon selection
 *
 * Copyright (c) 20013 A. K. M. Rezaul Karim<titosust@gmail.com>
 * Modifications (c) 20015 Paden Clayton<fasttracksites.com>
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Project home:
 *   https://github.com/titosust/Bootstrap-icon-picker
 *
 * Version:  1.0.1
 *
 */

(function($) {

    $.fn.iconPicker = function( options ) {
        var mouseOver=false;
        var $popup=null;
        var icons = ['search', 'times-circle', 'address-book', 'address-book-o', 'address-card', 'address-card-o', 'bandcamp', 'bath', 'bathtub', 'drivers-license', 'drivers-license-o', 'eercast', 'envelope-open', 'envelope-open-o', 'etsy', 'free-code-camp', 'grav', 'handshake-o', 'id-badge', 'id-card', 'id-card-o', 'imdb', 'linode', 'meetup', 'microchip', 'podcast', 'quora', 'ravelry', 's', 'shower', 'snowflake-o', 'superpowers', 'telegram', 'thermometer', 'thermometer-', 'thermometer-empty', 'thermometer-full', 'thermometer-half', 'thermometer-quarter', 'thermometer-three-quarters', 'times-rectangle', 'times-rectangle-o', 'user-circle', 'user-circle-o', 'user-o', 'vcard', 'vcard-o', 'window-close', 'window-close-o', 'window-maximize', 'window-minimize', 'window-restore', 'wpexplorer', 'adjust', 'american-sign-language-interpreting', 'anchor', 'archive', 'area-chart', 'arrows', 'arrows-h', 'arrows-v', 'asl-interpreting', 'assistive-listening-systems', 'asterisk', 'at', 'audio-description', 'automobile', 'balance-scale', 'ban', 'bank', 'bar-chart', 'bar-chart-o', 'barcode', 'bars', 'battery', 'battery-', 'battery-empty', 'battery-full', 'battery-half', 'battery-quarter', 'battery-three-quarters', 'bed', 'beer', 'bell', 'bell-o', 'bell-slash', 'bell-slash-o', 'bicycle', 'binoculars', 'birthday-cake', 'blind', 'bluetooth', 'bluetooth-b', 'bolt', 'bomb', 'book', 'bookmark', 'bookmark-o', 'braille', 'briefcase', 'bug', 'building', 'building-o', 'bullhorn', 'bullseye', 'bus', 'cab', 'calculator', 'calendar', 'calendar-check-o', 'calendar-minus-o', 'calendar-o', 'calendar-plus-o', 'calendar-times-o', 'camera', 'camera-retro', 'car', 'caret-square-o-down', 'caret-square-o-left', 'caret-square-o-right', 'caret-square-o-up', 'cart-arrow-down', 'cart-plus', 'cc', 'certificate', 'check', 'check-circle', 'check-circle-o', 'check-square', 'check-square-o', 'child', 'circle', 'circle-o', 'circle-o-notch', 'circle-thin', 'clock-o', 'clone', 'close', 'cloud', 'cloud-download', 'cloud-upload', 'code', 'code-fork', 'coffee', 'cog', 'cogs', 'comment', 'comment-o', 'commenting', 'commenting-o', 'comments', 'comments-o', 'compass', 'copyright', 'creative-commons', 'credit-card', 'credit-card-alt', 'crop', 'crosshairs', 'cube', 'cubes', 'cutlery', 'dashboard', 'database', 'deaf', 'deafness', 'desktop', 'diamond', 'dot-circle-o', 'download', 'edit', 'ellipsis-h', 'ellipsis-v', 'envelope', 'envelope-o', 'envelope-square', 'eraser', 'exchange', 'exclamation', 'exclamation-circle', 'exclamation-triangle', 'external-link', 'external-link-square', 'eye', 'eye-slash', 'eyedropper', 'fax', 'feed', 'female', 'fighter-jet', 'file-archive-o', 'file-audio-o', 'file-code-o', 'file-excel-o', 'file-image-o', 'file-movie-o', 'file-pdf-o', 'file-photo-o', 'file-picture-o', 'file-powerpoint-o', 'file-sound-o', 'file-video-o', 'file-word-o', 'file-zip-o', 'film', 'filter', 'fire', 'fire-extinguisher', 'flag', 'flag-checkered', 'flag-o', 'flash', 'flask', 'folder', 'folder-o', 'folder-open', 'folder-open-o', 'frown-o', 'futbol-o', 'gamepad', 'gavel', 'gear', 'gears', 'gift', 'glass', 'globe', 'graduation-cap', 'group', 'hand-grab-o', 'hand-lizard-o', 'hand-paper-o', 'hand-peace-o', 'hand-pointer-o', 'hand-rock-o', 'hand-scissors-o', 'hand-spock-o', 'hand-stop-o', 'hard-of-hearing', 'hashtag', 'hdd-o', 'headphones', 'heart', 'heart-o', 'heartbeat', 'history', 'home', 'hotel', 'hourglass', 'hourglass-', 'hourglass-end', 'hourglass-half', 'hourglass-o', 'hourglass-start', 'i-cursor', 'image', 'inbox', 'industry', 'info', 'info-circle', 'institution', 'key', 'keyboard-o', 'language', 'laptop', 'leaf', 'legal', 'lemon-o', 'level-down', 'level-up', 'life-bouy', 'life-buoy', 'life-ring', 'life-saver', 'lightbulb-o', 'line-chart', 'location-arrow', 'lock', 'low-vision', 'magic', 'magnet', 'mail-forward', 'mail-reply', 'mail-reply-all', 'male', 'map', 'map-marker', 'map-o', 'map-pin', 'map-signs', 'meh-o', 'microphone', 'microphone-slash', 'minus', 'minus-circle', 'minus-square', 'minus-square-o', 'mobile', 'mobile-phone', 'money', 'moon-o', 'mortar-board', 'motorcycle', 'mouse-pointer', 'music', 'navicon', 'newspaper-o', 'object-group', 'object-ungroup', 'paint-brush', 'paper-plane', 'paper-plane-o', 'paw', 'pencil', 'pencil-square', 'pencil-square-o', 'percent', 'phone', 'phone-square', 'photo', 'picture-o', 'pie-chart', 'plane', 'plug', 'plus', 'plus-circle', 'plus-square', 'plus-square-o', 'power-off', 'print', 'puzzle-piece', 'qrcode', 'question', 'question-circle', 'question-circle-o', 'quote-left', 'quote-right', 'random', 'recycle', 'refresh', 'registered', 'remove', 'reorder', 'reply', 'reply-all', 'retweet', 'road', 'rocket', 'rss', 'rss-square', 'search-minus', 'search-plus', 'send', 'send-o', 'server', 'share', 'share-alt', 'share-alt-square', 'share-square', 'share-square-o', 'shield', 'ship', 'shopping-bag', 'shopping-basket', 'shopping-cart', 'sign-in', 'sign-language', 'sign-out', 'signal', 'signing', 'sitemap', 'sliders', 'smile-o', 'soccer-ball-o', 'sort', 'sort-alpha-asc', 'sort-alpha-desc', 'sort-amount-asc', 'sort-amount-desc', 'sort-asc', 'sort-desc', 'sort-down', 'sort-numeric-asc', 'sort-numeric-desc', 'sort-up', 'space-shuttle', 'spinner', 'spoon', 'square', 'square-o', 'star', 'star-half', 'star-half-empty', 'star-half-full', 'star-half-o', 'star-o', 'sticky-note', 'sticky-note-o', 'street-view', 'suitcase', 'sun-o', 'support', 'tablet', 'tachometer', 'tag', 'tags', 'tasks', 'taxi', 'television', 'terminal', 'thumb-tack', 'thumbs-down', 'thumbs-o-down', 'thumbs-o-up', 'thumbs-up', 'ticket', 'times', 'times-circle-o', 'tint', 'toggle-down', 'toggle-left', 'toggle-off', 'toggle-on', 'toggle-right', 'toggle-up', 'trademark', 'trash', 'trash-o', 'tree', 'trophy', 'truck', 'tty', 'tv', 'umbrella', 'universal-access', 'university', 'unlock', 'unlock-alt', 'unsorted', 'upload', 'user', 'user-plus', 'user-secret', 'user-times', 'users', 'video-camera', 'volume-control-phone', 'volume-down', 'volume-off', 'volume-up', 'warning', 'wheelchair', 'wheelchair-alt', 'wifi', 'wrench', 'hand-o-down', 'hand-o-left', 'hand-o-right', 'hand-o-up', 'ambulance', 'subway', 'train', 'genderless', 'intersex', 'mars', 'mars-double', 'mars-stroke', 'mars-stroke-h', 'mars-stroke-v', 'mercury', 'neuter', 'transgender', 'transgender-alt', 'venus', 'venus-double', 'venus-mars', 'file', 'file-o', 'file-text', 'file-text-o', 'cc-amex', 'cc-diners-club', 'cc-discover', 'cc-jcb', 'cc-mastercard', 'cc-paypal', 'cc-stripe', 'cc-visa', 'google-wallet', 'paypal', 'bitcoin', 'btc', 'cny', 'dollar', 'eur', 'euro', 'gbp', 'gg', 'gg-circle', 'ils', 'inr', 'jpy', 'krw', 'rmb', 'rouble', 'rub', 'ruble', 'rupee', 'shekel', 'sheqel', 'try', 'turkish-lira', 'usd', 'won', 'yen', 'align-center', 'align-justify', 'align-left', 'align-right', 'bold', 'chain', 'chain-broken', 'clipboard', 'columns', 'copy', 'cut', 'dedent', 'files-o', 'floppy-o', 'font', 'header', 'indent', 'italic', 'link', 'list', 'list-alt', 'list-ol', 'list-ul', 'outdent', 'paperclip', 'paragraph', 'paste', 'repeat', 'rotate-left', 'rotate-right', 'save', 'scissors', 'strikethrough', 'subscript', 'superscript', 'table', 'text-height', 'text-width', 'th', 'th-large', 'th-list', 'underline', 'undo', 'unlink', 'angle-double-down', 'angle-double-left', 'angle-double-right', 'angle-double-up', 'angle-down', 'angle-left', 'angle-right', 'angle-up', 'arrow-circle-down', 'arrow-circle-left', 'arrow-circle-o-down', 'arrow-circle-o-left', 'arrow-circle-o-right', 'arrow-circle-o-up', 'arrow-circle-right', 'arrow-circle-up', 'arrow-down', 'arrow-left', 'arrow-right', 'arrow-up', 'arrows-alt', 'caret-down', 'caret-left', 'caret-right', 'caret-up', 'chevron-circle-down', 'chevron-circle-left', 'chevron-circle-right', 'chevron-circle-up', 'chevron-down', 'chevron-left', 'chevron-right', 'chevron-up', 'long-arrow-down', 'long-arrow-left', 'long-arrow-right', 'long-arrow-up', 'backward', 'compress', 'eject', 'expand', 'fast-backward', 'fast-forward', 'forward', 'pause', 'pause-circle', 'pause-circle-o', 'play', 'play-circle', 'play-circle-o', 'step-backward', 'step-forward', 'stop', 'stop-circle', 'stop-circle-o', 'youtube-play', 'adn', 'amazon', 'android', 'angellist', 'apple', 'behance', 'behance-square', 'bitbucket', 'bitbucket-square', 'black-tie', 'buysellads', 'chrome', 'codepen', 'codiepie', 'connectdevelop', 'contao', 'css', 'dashcube', 'delicious', 'deviantart', 'digg', 'dribbble', 'dropbox', 'drupal', 'edge', 'empire', 'envira', 'expeditedssl', 'fa', 'facebook', 'facebook-f', 'facebook-official', 'facebook-square', 'firefox', 'first-order', 'flickr', 'font-awesome', 'fonticons', 'fort-awesome', 'forumbee', 'foursquare', 'ge', 'get-pocket', 'git', 'git-square', 'github', 'github-alt', 'github-square', 'gitlab', 'gittip', 'glide', 'glide-g', 'google', 'google-plus', 'google-plus-circle', 'google-plus-official', 'google-plus-square', 'gratipay', 'hacker-news', 'houzz', 'html', 'instagram', 'internet-explorer', 'ioxhost', 'joomla', 'jsfiddle', 'lastfm', 'lastfm-square', 'leanpub', 'linkedin', 'linkedin-square', 'linux', 'maxcdn', 'meanpath', 'medium', 'mixcloud', 'modx', 'odnoklassniki', 'odnoklassniki-square', 'opencart', 'openid', 'opera', 'optin-monster', 'pagelines', 'pied-piper', 'pied-piper-alt', 'pied-piper-pp', 'pinterest', 'pinterest-p', 'pinterest-square', 'product-hunt', 'qq', 'ra', 'rebel', 'reddit', 'reddit-alien', 'reddit-square', 'renren', 'resistance', 'safari', 'scribd', 'sellsy', 'shirtsinbulk', 'simplybuilt', 'skyatlas', 'skype', 'slack', 'slideshare', 'snapchat', 'snapchat-ghost', 'snapchat-square', 'soundcloud', 'spotify', 'stack-exchange', 'stack-overflow', 'steam', 'steam-square', 'stumbleupon', 'stumbleupon-circle', 'tencent-weibo', 'themeisle', 'trello', 'tripadvisor', 'tumblr', 'tumblr-square', 'twitch', 'twitter', 'twitter-square', 'usb', 'viacoin', 'viadeo', 'viadeo-square', 'vimeo', 'vimeo-square', 'vine', 'vk', 'wechat', 'weibo', 'weixin', 'whatsapp', 'wikipedia-w', 'windows', 'wordpress', 'wpbeginner', 'wpforms', 'xing', 'xing-square', 'y-combinator', 'y-combinator-square', 'yahoo', 'yc', 'yc-square', 'yelp', 'yoast', 'youtube', 'youtube-square', 'h-square', 'hospital-o', 'medkit', 'stethoscope', 'user-md'];
        var settings = $.extend({
        	
        }, options);
        return this.each( function() {
        	element=this;
            if(!settings.buttonOnly && $(this).data("iconPicker")==undefined ){
            	$this=$(this).addClass("form-control");
            	$wraper=$("<div/>",{class:"input-group"});
            	$this.wrap($wraper);
            	$button = $("<span id=\"iconPickerBtn\" class=\"input-group-addon pointer\"><i class=\"fa fa-image\"></i></span>");
            	$this.after($button);
            	(function(ele){
	            	$button.click(function(){
			       		createUI(ele);
			       		showList(ele,icons);
	            	});
	            })($this);

            	$(this).data("iconPicker",{attached:true});
            }
        
            function createUI($element) {
                var editformOffset = $("#editform").offset();
	            $popup = $('<div/>', {
	                css: {
	                    'top': $element.offset().top + $element.outerHeight() - editformOffset.top +20,
	                    'left': $element.offset().left - editformOffset.left
	                },
	                id: "iconPickerContent",
	                class: 'icon-popup'
	            });

	        	$popup.html('<div class="ip-control"> \
						          <ul> \
						            <li><a href="javascript:;" class="btn" data-dir="-1"><span class="glyphicon  glyphicon-fast-backward"></span></a></li> \
						            <li><input type="text" class="ip-search fa fa-search" placeholder="查询" /></li> \
						            <li><a href="javascript:;"  class="btn" data-dir="1"><span class="glyphicon  glyphicon-fast-forward"></span></a></li> \
						          </ul> \
						      </div> \
						     <div class="icon-list"> </div> \
					         ').appendTo("#editform");
	        	
	        	
	        	$popup.addClass('dropdown-menu').show();
				$popup.mouseenter(function() {  mouseOver=true;  }).mouseleave(function() { mouseOver=false;  });

	        	var lastVal="", start_index=0,per_page=30,end_index=start_index+per_page;
	        	$(".ip-control .btn",$popup).click(function(e){
	                e.stopPropagation();
	                var dir=$(this).attr("data-dir");
	                start_index=start_index+per_page*dir;
	                start_index=start_index<0?0:start_index;
	                if(start_index+per_page<=805){
	                  $.each($(".icon-list>ul li"),function(i){
	                      if(i>=start_index && i<start_index+per_page){
	                         $(this).show();
	                      }else{
	                        $(this).hide();
	                      }
	                  });
	                }else{
	                  start_index=775;
	                }
	            });
	        	
	        	$('.ip-control .ip-search',$popup).on("keyup",function(e){
	                if(lastVal!=$(this).val()){
	                    lastVal=$(this).val();
	                    if(lastVal==""){
	                        showList($element,icons);
	                    }else{
	                    	showList($element, $(icons)
							        .map(function(i,v){ 
								            if(v.toLowerCase().indexOf(lastVal.toLowerCase())!=-1){return v} 
								        }).get());
						}
	                    
	                }
	            });  
	        	$(document).mouseup(function (event) {
	        	    if (!(event.target.id === "iconPickerBtn" || event.target.id === "iconPickerContent" || $(event.target).parents("#iconPickerContent").length > 0)) {
	        	        removeInstance();
	        	    }
				});

	        }
	        function removeInstance(){
	        	$(".icon-popup").remove();
	        }
	        function showList($element,arrLis){
	        	$ul=$("<ul>");
	        	
	        	for (var i in arrLis) {
	        	    $ul.append("<li><a href=\"#\" title=" + arrLis[i] + "><span class=\"fa fa-" + arrLis[i] + "\"></span></a></li>");
	        	};

	        	$(".icon-list",$popup).html($ul);
	        	$(".icon-list li a",$popup).click(function(e){
	        		e.preventDefault();
	        		var title=$(this).attr("title");
	        		$element.val("fa fa-" + title);
	        		removeInstance();
	        	});
	        }

        });
    }

}(jQuery));
