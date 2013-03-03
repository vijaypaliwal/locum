/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){a._wa={};a.xy.week_agenda_scale_height=20;a.templates.week_agenda_event_text=function(c,k,f){return a.templates.event_date(c)+" "+f.text};a.date.week_agenda_start=a.date.week_start;a.date.week_agenda_end=function(c){return a.date.add(c,7,"day")};a.date.add_week_agenda=function(c,k){return a.date.add(c,k*7,"day")};a.attachEvent("onSchedulerReady",function(){var c=a.templates;if(!c.week_agenda_date)c.week_agenda_date=c.week_date});(function(){var c=a.date.date_to_str("%l, %F %d");
a.templates.week_agenda_scale_date=function(a){return c(a)}})();a.attachEvent("onTemplatesReady",function(){a.attachEvent("onSchedulerResize",function(){return this._mode=="week_agenda"?(this.week_agenda_view(!0),!1):!0});var c=a.render_data;a.render_data=function(d){if(this._mode=="week_agenda")a.week_agenda_view(!0);else return c.apply(this,arguments)};var k=function(){a._cols=[];var d=parseInt(a._els.dhx_cal_data[0].style.width);a._cols.push(Math.floor(d/2));a._cols.push(d-a._cols[0]-1);a._colsS=
{0:[],1:[]};for(var b=parseInt(a._els.dhx_cal_data[0].style.height),n=0;n<3;n++)a._colsS[0].push(Math.floor(b/(3-a._colsS[0].length))),b-=a._colsS[0][n];a._colsS[1].push(a._colsS[0][0]);a._colsS[1].push(a._colsS[0][1]);b=a._colsS[0][a._colsS[0].length-1];a._colsS[1].push(Math.floor(b/2));a._colsS[1].push(b-a._colsS[1][a._colsS[1].length-1])},f=function(){k();a._els.dhx_cal_data[0].innerHTML="";a._rendered=[];for(var d="",b=0;b<2;b++){var n=a._cols[b],c="dhx_wa_column";b==1&&(c+=" dhx_wa_column_last");
d+="<div class='"+c+"' style='width: "+n+"px;'>";for(var g=0;g<a._colsS[b].length;g++){var j=a.xy.week_agenda_scale_height-2,v=a._colsS[b][g]-j-2,l=Math.min(6,g*2+b);d+="<div class='dhx_wa_day_cont'><div style='height:"+j+"px; line-height:"+j+"px;' class='dhx_wa_scale_bar'></div><div style='height:"+v+"px;' class='dhx_wa_day_data' day='"+l+"'></div></div>"}d+="</div>"}a._els.dhx_cal_date[0].innerHTML=a.templates[a._mode+"_date"](a._min_date,a._max_date,a._mode);a._els.dhx_cal_data[0].innerHTML=d;
for(var m=a._els.dhx_cal_data[0].getElementsByTagName("div"),p=[],b=0;b<m.length;b++)m[b].className=="dhx_wa_day_cont"&&p.push(m[b]);a._wa._selected_divs=[];for(var f=a.get_visible_events(),i=a.date.week_start(a._date),o=a.date.add(i,1,"day"),b=0;b<7;b++){p[b]._date=i;var w=p[b].childNodes[0],x=p[b].childNodes[1];w.innerHTML=a.templates.week_agenda_scale_date(i);for(var q=[],s=0;s<f.length;s++){var t=f[s];t.start_date<o&&t.end_date>i&&q.push(t)}q.sort(function(a,b){return a.start_date.valueOf()==
b.start_date.valueOf()?a.id>b.id?1:-1:a.start_date>b.start_date?1:-1});for(g=0;g<q.length;g++){var e=q[g],h=document.createElement("div");a._rendered.push(h);var u=a.templates.event_class(e.start_date,e.end_date,e);h.className="dhx_wa_ev_body"+(u?" "+u:"");if(e._text_style)h.style.cssText=e._text_style;if(e.color)h.style.background=e.color;if(e.textColor)h.style.color=e.textColor;if(a._select_id&&e.id==a._select_id&&(a.config.week_agenda_select||a.config.week_agenda_select===void 0))h.className+=
" dhx_cal_event_selected",a._wa._selected_divs.push(h);var r="";e._timed||(r="middle",e.start_date.valueOf()>=i.valueOf()&&e.start_date.valueOf()<=o.valueOf()&&(r="start"),e.end_date.valueOf()>=i.valueOf()&&e.end_date.valueOf()<=o.valueOf()&&(r="end"));h.innerHTML=a.templates.week_agenda_event_text(e.start_date,e.end_date,e,i,r);h.setAttribute("event_id",e.id);x.appendChild(h)}i=a.date.add(i,1,"day");o=a.date.add(o,1,"day")}};a.week_agenda_view=function(d){a._min_date=a.date.week_start(a._date);a._max_date=
a.date.add(a._min_date,1,"week");a.set_sizes();if(d)a._table_view=a._allow_dnd=!0,a._wa._prev_data_border=a._els.dhx_cal_data[0].style.borderTop,a._els.dhx_cal_data[0].style.borderTop=0,a._els.dhx_cal_data[0].style.overflowY="hidden",a._els.dhx_cal_date[0].innerHTML="",a._els.dhx_cal_data[0].style.top=parseInt(a._els.dhx_cal_data[0].style.top)-20-1+"px",a._els.dhx_cal_data[0].style.height=parseInt(a._els.dhx_cal_data[0].style.height)+20+1+"px",a._els.dhx_cal_header[0].style.display="none",f();else{a._table_view=
a._allow_dnd=!1;if(a._wa._prev_data_border)a._els.dhx_cal_data[0].style.borderTop=a._wa._prev_data_border;a._els.dhx_cal_data[0].style.overflowY="auto";a._els.dhx_cal_data[0].style.top=parseInt(a._els.dhx_cal_data[0].style.top)+20+"px";a._els.dhx_cal_data[0].style.height=parseInt(a._els.dhx_cal_data[0].style.height)-20+"px";a._els.dhx_cal_header[0].style.display="block"}};a.mouse_week_agenda=function(d){for(var b=d.ev,c=b.srcElement||b.target;c.parentNode;){if(c._date)var f=c._date;c=c.parentNode}if(!f)return d;
d.x=0;var g=f.valueOf()-a._min_date.valueOf();d.y=Math.ceil(g/6E4/this.config.time_step);if(this._drag_mode=="move"){this._drag_event._dhx_changed=!0;this._select_id=this._drag_id;for(var j=0;j<a._rendered.length;j++)if(a._drag_id==this._rendered[j].getAttribute("event_id"))var k=this._rendered[j];if(!a._wa._dnd){var l=k.cloneNode(!0);this._wa._dnd=l;l.className=k.className;l.id="dhx_wa_dnd";l.className+=" dhx_wa_dnd";document.body.appendChild(l)}var m=document.getElementById("dhx_wa_dnd");m.style.top=
(b.pageY||b.clientY)+20+"px";m.style.left=(b.pageX||b.clientX)+20+"px"}return d};a.attachEvent("onBeforeEventChanged",function(){if(this._mode=="week_agenda"&&this._drag_mode=="move"){var d=document.getElementById("dhx_wa_dnd");d.parentNode.removeChild(d);a._wa._dnd=!1}return!0});a.attachEvent("onEventSave",function(a,b,c){if(c&&this._mode=="week_agenda")this._select_id=a;return!0});a._wa._selected_divs=[];a.attachEvent("onClick",function(c){if(this._mode=="week_agenda"&&(a.config.week_agenda_select||
a.config.week_agenda_select===void 0)){if(a._wa._selected_divs)for(var b=0;b<this._wa._selected_divs.length;b++){var f=this._wa._selected_divs[b];f.className=f.className.replace(/ dhx_cal_event_selected/,"")}this.for_rendered(c,function(b){b.className+=" dhx_cal_event_selected";a._wa._selected_divs.push(b)});a.select(c);return!1}return!0})})});