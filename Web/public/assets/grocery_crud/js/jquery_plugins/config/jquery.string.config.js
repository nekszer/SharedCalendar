$(function(){
	$('.input-maxlenght').maxlength({
		threshold: $(this).attr("maxlenght"),
		warningClass: "badge badge-primary",
		limitReachedClass: "badge badge-success"
	});
});