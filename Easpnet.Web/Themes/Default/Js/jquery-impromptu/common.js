/*
 * ---------------------------
 * functions for the examples
 * ---------------------------
 */
function mycallbackfunc(v,m){
	$.prompt('i clicked ' + v);
}

function mycallbackform(v,m){
	$.prompt(v +' ' + m.children('#alertName').val());
}

function mysubmitfunc(v,m){
	an = m.children('#alertName');
	if(an.val() == ""){
		an.css("border","solid #ff0000 1px");
		return false;
	}
	return true;
}

jQuery.fn.extend({
	myanimation: function(speed){
		var t = $(this);
		if(t.css("display") == "none") 
			t.show(speed,function(){ t.hide(speed,function(){ t.show(speed); }); });
		else t.hide(speed,function(){ t.show(speed,function(){ t.hide(speed); }); });
	}
});

var txt = 'Please enter your name:<br /><input type="text" id="alertName" name="myname" value="name here" />';
var txt2 = 'Try submitting an empty field:<br /><input type="text" id="alertName" name="myname" value="" />';	

var brown_theme_text = '<h3>Example 13</h3><p>Save these settings?</p><img src="images/help.gif" alt="help" class="helpImg" />';