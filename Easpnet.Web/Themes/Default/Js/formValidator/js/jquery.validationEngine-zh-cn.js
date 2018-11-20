

(function($) {
	$.fn.validationEngineLanguage = function() {};
	$.validationEngineLanguage = {
		newLang: function() {
			$.validationEngineLanguage.allRules = 	{"required":{    			// Add your regex rules here, you can take telephone as an example
						"regex":"none",
						"alertText":"* 必须填写",
						"alertTextCheckboxMultiple":"* 请选择",
						"alertTextCheckboxe":"* 必须选中该复选框"},
					"length":{
						"regex":"none",
						"alertText":"* 必须为 ",
						"alertText2":" - ",
						"alertText3": " 个字符"},
					"maxCheckbox":{
						"regex":"none",
						"alertText":"* Checks allowed Exceeded"},	
					"minCheckbox":{
						"regex":"none",
						"alertText":"* 请至少选择 ",
						"alertText2":" 项"},	
					"confirm":{
						"regex":"none",
						"alertText":"* 输入不一致"},		
					"telephone":{
						"regex":"/^[0-9\-\(\)\ ]+$/",
						"alertText":"* 电话号码格式错误"},	
					"email":{
						"regex":"/^[a-zA-Z0-9_\.\-]+\@([a-zA-Z0-9\-]+\.)+[a-zA-Z0-9]{2,4}$/",
						"alertText":"* 您输入的Email格式有误"},	
					"date":{
                         "regex":"/^[0-9]{4}\-\[0-9]{1,2}\-\[0-9]{1,2}$/",
                         "alertText":"* 日期格式错误, 必须为 YYYY-MM-DD (如: 2010-12-12)"},
					"onlyNumber":{
						"regex":"/^[0-9\ ]+$/",
						"alertText":"* 只能输入数字"},	
					"noSpecialCaracters":{
						"regex":"/^[0-9a-zA-Z_]+$/",
						"alertText":"* 不能输入特殊字符，必须为数字或字母或下划线"},	
					"ajaxUser":{
						"file":"/ajax.ashx?_m=Member&action=validate_user_name",
						"extraData":"name=eric",
						"alertTextOk":"* This user is available",	
						"alertTextLoad":"* Loading, please wait",
						"alertText":"* This user is already taken"},	
					"ajaxName":{
						"file":"validateUser.php",
						"alertText":"* This name is already taken",
						"alertTextOk":"* This name is available",	
						"alertTextLoad":"* Loading, please wait"},		
					"onlyLetter":{
						"regex":"/^[a-zA-Z\ \']+$/",
						"alertText":"* 只能输入字母"},
					"validate2fields":{
    					"nname":"validate2fields",
    					"alertText":"* You must have a firstname and a lastname"}	
					}	
					
		}
	}
})(jQuery);

$(document).ready(function() {	
	$.validationEngineLanguage.newLang()
});