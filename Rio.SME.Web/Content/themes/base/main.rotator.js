
var trigged=[],scrollTimeout;
jQuery(function($){
	
	//Trigger rotate
	// --------------------
	$('#header .thumb').on('rotate',function(){
		var $this = $(this);
		$this.addClass('active');
		var tout =getRandomInt(3,10)*1000;
		setTimeout(function(){
			$this.removeClass('active');
		},tout)
	});
	var rotateCycle = setInterval(function(){
		var thumbs = $('#header .thumb:not(.active)');
		$(thumbs[getRandomInt(0,thumbs.length)]).trigger('rotate');
	},2000);

	function triggerEvent(elem,fn,offset){
		var top = elem.offset().top;
		if((top-offset)<$(window).scrollTop()){
			fn(elem);
		}
	}


	$('#navbar .nav a , #header a.nav-item , #navbar .brand,#btn_up').click(function(e){
		e.preventDefault();
		var des = $(this).attr('href');
		if($('.navbar .nav-collapse').hasClass('in')){
			$('.navbar .btn-navbar').trigger('click');
		}
		goToSectionID(des);
	})

		// cache container
		var $container = $('.isotope');
		// initialize isotope
		$container.isotope({
		  // options...
		});

	
	if (/*@cc_on!@*/false) {  
   		$("html").addClass("ie10");  
   		
	}	 
	// if ($.browser.msie && $.browser.version == 10) {
	// 	  $("html").addClass("ie10");
	// 	}
})


function getRandomInt (min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

