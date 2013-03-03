/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a.__recurring_template='<div class="dhx_form_repeat"> <form> <div class="dhx_repeat_left"> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="day" />\u0397\u03bc\u03b5\u03c1\u03b7\u03c3\u03af\u03c9\u03c2</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="week"/>\u0395\u03b2\u03b4\u03bf\u03bc\u03b1\u03b4\u03b9\u03b1\u03af\u03c9\u03c2</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="month" checked />\u039c\u03b7\u03bd\u03b9\u03b1\u03af\u03c9\u03c2</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="year" />\u0395\u03c4\u03b7\u03c3\u03af\u03c9\u03c2</label> </div> <div class="dhx_repeat_divider"></div> <div class="dhx_repeat_center"> <div style="display:none;" id="dhx_repeat_day"> <label><input class="dhx_repeat_radio" type="radio" name="day_type" value="d"/>\u039a\u03ac\u03b8\u03b5</label><input class="dhx_repeat_text" type="text" name="day_count" value="1" />\u03b7\u03bc\u03ad\u03c1\u03b1<br /> <label><input class="dhx_repeat_radio" type="radio" name="day_type" checked value="w"/>\u039a\u03ac\u03b8\u03b5 \u03b5\u03c1\u03b3\u03ac\u03c3\u03b9\u03bc\u03b7</label> </div> <div style="display:none;" id="dhx_repeat_week"> \u0395\u03c0\u03b1\u03bd\u03ac\u03bb\u03b7\u03c8\u03b7 \u03ba\u03ac\u03b8\u03b5<input class="dhx_repeat_text" type="text" name="week_count" value="1" />\u03b5\u03b2\u03b4\u03bf\u03bc\u03ac\u03b4\u03b1 \u03c4\u03b9\u03c2 \u03b5\u03c0\u03cc\u03bc\u03b5\u03bd\u03b5\u03c2 \u03b7\u03bc\u03ad\u03c1\u03b5\u03c2:<br /> <table class="dhx_repeat_days"> <tr> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="1" />\u0394\u03b5\u03c5\u03c4\u03ad\u03c1\u03b1</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="4" />\u03a0\u03ad\u03bc\u03c0\u03c4\u03b7</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="2" />\u03a4\u03c1\u03af\u03c4\u03b7</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="5" />\u03a0\u03b1\u03c1\u03b1\u03c3\u03ba\u03b5\u03c5\u03ae</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="3" />\u03a4\u03b5\u03c4\u03ac\u03c1\u03c4\u03b7</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="6" />\u03a3\u03ac\u03b2\u03b2\u03b1\u03c4\u03bf</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="0" />\u039a\u03c5\u03c1\u03b9\u03b1\u03ba\u03ae</label><br /><br /> </td> </tr> </table> </div> <div id="dhx_repeat_month"> <label><input class="dhx_repeat_radio" type="radio" name="month_type" value="d"/>\u0395\u03c0\u03b1\u03bd\u03ac\u03bb\u03b7\u03c8\u03b7</label><input class="dhx_repeat_text" type="text" name="month_day" value="1" />\u03b7\u03bc\u03ad\u03c1\u03b1 \u03ba\u03ac\u03b8\u03b5<input class="dhx_repeat_text" type="text" name="month_count" value="1" />\u03bc\u03ae\u03bd\u03b1<br /> <label><input class="dhx_repeat_radio" type="radio" name="month_type" checked value="w"/>\u03a4\u03b7\u03bd</label><input class="dhx_repeat_text" type="text" name="month_week2" value="1" /><select name="month_day2"><option value="1" selected >\u0394\u03b5\u03c5\u03c4\u03ad\u03c1\u03b1<option value="2">\u03a4\u03c1\u03af\u03c4\u03b7<option value="3">\u03a4\u03b5\u03c4\u03ac\u03c1\u03c4\u03b7<option value="4">\u03a0\u03ad\u03bc\u03c0\u03c4\u03b7<option value="5">\u03a0\u03b1\u03c1\u03b1\u03c3\u03ba\u03b5\u03c5\u03ae<option value="6">\u03a3\u03ac\u03b2\u03b2\u03b1\u03c4\u03bf<option value="0">\u039a\u03c5\u03c1\u03b9\u03b1\u03ba\u03ae</select>\u03ba\u03ac\u03b8\u03b5<input class="dhx_repeat_text" type="text" name="month_count2" value="1" />\u03bc\u03ae\u03bd\u03b1<br /> </div> <div style="display:none;" id="dhx_repeat_year"> <label><input class="dhx_repeat_radio" type="radio" name="year_type" value="d"/>\u039a\u03ac\u03b8\u03b5</label><input class="dhx_repeat_text" type="text" name="year_day" value="1" />\u03b7\u03bc\u03ad\u03c1\u03b1<select name="year_month"><option value="0" selected >\u0399\u03b1\u03bd\u03bf\u03c5\u03ac\u03c1\u03b9\u03bf\u03c2<option value="1">\u03a6\u03b5\u03b2\u03c1\u03bf\u03c5\u03ac\u03c1\u03b9\u03bf\u03c2<option value="2">\u039c\u03ac\u03c1\u03c4\u03b9\u03bf\u03c2<option value="3">\u0391\u03c0\u03c1\u03af\u03bb\u03b9\u03bf\u03c2<option value="4">\u039c\u03ac\u03ca\u03bf\u03c2<option value="5">\u0399\u03bf\u03cd\u03bd\u03b9\u03bf\u03c2<option value="6">\u0399\u03bf\u03cd\u03bb\u03b9\u03bf\u03c2<option value="7">\u0391\u03cd\u03b3\u03bf\u03c5\u03c3\u03c4\u03bf\u03c2<option value="8">\u03a3\u03b5\u03c0\u03c4\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2<option value="9">\u039f\u03ba\u03c4\u03ce\u03b2\u03c1\u03b9\u03bf\u03c2<option value="10">\u039d\u03bf\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2<option value="11">\u0394\u03b5\u03ba\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2</select>\u03bc\u03ae\u03bd\u03b1<br /> <label><input class="dhx_repeat_radio" type="radio" name="year_type" checked value="w"/>\u03a4\u03b7\u03bd</label><input class="dhx_repeat_text" type="text" name="year_week2" value="1" /><select name="year_day2"><option value="1" selected >\u0394\u03b5\u03c5\u03c4\u03ad\u03c1\u03b1<option value="2">\u03a4\u03c1\u03af\u03c4\u03b7<option value="3">\u03a4\u03b5\u03c4\u03ac\u03c1\u03c4\u03b7<option value="4">\u03a0\u03ad\u03bc\u03c0\u03c4\u03b7<option value="5">\u03a0\u03b1\u03c1\u03b1\u03c3\u03ba\u03b5\u03c5\u03ae<option value="6">\u03a3\u03ac\u03b2\u03b2\u03b1\u03c4\u03bf<option value="7">\u039a\u03c5\u03c1\u03b9\u03b1\u03ba\u03ae</select>\u03c4\u03bf\u03c5<select name="year_month2"><option value="0" selected >\u0399\u03b1\u03bd\u03bf\u03c5\u03ac\u03c1\u03b9\u03bf\u03c2<option value="1">\u03a6\u03b5\u03b2\u03c1\u03bf\u03c5\u03ac\u03c1\u03b9\u03bf\u03c2<option value="2">\u039c\u03ac\u03c1\u03c4\u03b9\u03bf\u03c2<option value="3">\u0391\u03c0\u03c1\u03af\u03bb\u03b9\u03bf\u03c2<option value="4">\u039c\u03ac\u03ca\u03bf\u03c2<option value="5">\u0399\u03bf\u03cd\u03bd\u03b9\u03bf\u03c2<option value="6">\u0399\u03bf\u03cd\u03bb\u03b9\u03bf\u03c2<option value="7">\u0391\u03cd\u03b3\u03bf\u03c5\u03c3\u03c4\u03bf\u03c2<option value="8">\u03a3\u03b5\u03c0\u03c4\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2<option value="9">\u039f\u03ba\u03c4\u03ce\u03b2\u03c1\u03b9\u03bf\u03c2<option value="10">\u039d\u03bf\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2<option value="11">\u0394\u03b5\u03ba\u03ad\u03bc\u03b2\u03c1\u03b9\u03bf\u03c2</select><br /> </div> </div> <div class="dhx_repeat_divider"></div> <div class="dhx_repeat_right"> <label><input class="dhx_repeat_radio" type="radio" name="end" checked/>\u03a7\u03c9\u03c1\u03af\u03c2 \u03b7\u03bc\u03b5\u03c1\u03bf\u03bc\u03b7\u03bd\u03af\u03b1 \u03bb\u03ae\u03be\u03b5\u03c9\u03c2</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="end" />\u039c\u03b5\u03c4\u03ac \u03b1\u03c0\u03cc</label><input class="dhx_repeat_text" type="text" name="occurences_count" value="1" />\u03b5\u03c0\u03b1\u03bd\u03b1\u03bb\u03ae\u03c8\u03b5\u03b9\u03c2<br /> <label><input class="dhx_repeat_radio" type="radio" name="end" />\u039b\u03ae\u03b3\u03b5\u03b9 \u03c4\u03b7\u03bd</label><input class="dhx_repeat_date" type="text" name="date_of_end" value="'+
a.config.repeat_date_of_end+'" /><br /> </div> </form> </div> <div style="clear:both"> </div>'});