/*
Copyright DHTMLX LTD. http://www.dhtmlx.com
To use this component please contact sales@dhtmlx.com to obtain license
*/
Scheduler.plugin(function(a){(function(){a.config.fix_tab_position=!0;a.config.use_select_menu_space=!0;a.config.hour_size_px=44;a.xy.nav_height=59;a.xy.bar_height=24;a.config.wide_form=!0;a.xy.lightbox_additional_height=90;a.config.displayed_event_color="#ff4a4a";a.config.displayed_event_text_color="#ffef80";a.templates.event_bar_date=function(d){return"\u2022 <b>"+a.templates.event_date(d)+"</b> "};a.attachEvent("onLightbox",function(){for(var d=a.getLightbox(),e=d.getElementsByTagName("div"),c=
0;c<e.length;c++){var f=e[c];if(f.className=="dhx_close_icon"){f.onclick=function(){a.endLightbox(!1,d)};break}}});a._lightbox_template="<div class='dhx_cal_ltitle'><span class='dhx_mark'>&nbsp;</span><span class='dhx_time'></span><span class='dhx_title'></span><div class='dhx_close_icon'></div></div><div class='dhx_cal_larea'></div>";a.attachEvent("onTemplatesReady",function(){var d=a.date.date_to_str("%d"),e=a.templates.month_day;a.templates.month_day=function(b){if(this._mode=="month"){var c=d(b);
b.getDate()==1&&(c=a.locale.date.month_full[b.getMonth()]+" "+c);+b==+a.date.date_part(new Date)&&(c=a.locale.labels.dhx_cal_today_button+" "+c);return c}else return e.call(this,b)};if(a.config.fix_tab_position)for(var c=a._els.dhx_cal_navline[0].getElementsByTagName("div"),f=[],g=211,h=0;h<c.length;h++){var b=c[h],i=b.getAttribute("name");if(i)switch(b.style.right="auto",i){case "day_tab":b.style.left="14px";b.className+=" dhx_cal_tab_first";break;case "week_tab":b.style.left="75px";break;case "month_tab":b.style.left=
"136px";b.className+=" dhx_cal_tab_last";break;default:b.style.left=g+"px",b.className+=" dhx_cal_tab_standalone",g=g+14+b.offsetWidth}}})})()});