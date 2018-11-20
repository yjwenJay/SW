cd D:\workspace\UI\UI_soulou\dwz-demo\bin

REM -------------- start package javascript --------------

type ..\javascripts\dwz.core.js > dwzESC.js
type ..\javascripts\dwz.util.date.js >> dwzESC.js
type ..\javascripts\dwz.validate.method.js >> dwzESC.js
type ..\javascripts\dwz.barDrag.js >> dwzESC.js
type ..\javascripts\dwz.drag.js >> dwzESC.js
type ..\javascripts\dwz.tree.js >> dwzESC.js
type ..\javascripts\dwz.accordion.js >> dwzESC.js
type ..\javascripts\dwz.ui.js >> dwzESC.js
type ..\javascripts\dwz.theme.js >> dwzESC.js
type ..\javascripts\dwz.switchEnv.js >> dwzESC.js

type ..\javascripts\dwz.alertMsg.js >> dwzESC.js
type ..\javascripts\dwz.contextmenu.js >> dwzESC.js
type ..\javascripts\dwz.navTab.js >> dwzESC.js
type ..\javascripts\dwz.tab.js >> dwzESC.js
type ..\javascripts\dwz.resize.js >> dwzESC.js
type ..\javascripts\dwz.jDialog.js >> dwzESC.js
type ..\javascripts\dwz.dialogDrag.js >> dwzESC.js
type ..\javascripts\dwz.cssTable.js >> dwzESC.js
type ..\javascripts\dwz.stable.js >> dwzESC.js
type ..\javascripts\dwz.taskBar.js >> dwzESC.js
type ..\javascripts\dwz.ajax.js >> dwzESC.js
type ..\javascripts\dwz.pagination.js >> dwzESC.js
type ..\javascripts\dwz.datepicker.js >> dwzESC.js
type ..\javascripts\dwz.effects.js >> dwzESC.js
type ..\javascripts\dwz.panel.js >> dwzESC.js
type ..\javascripts\dwz.checkbox.js >> dwzESC.js
type ..\javascripts\dwz.combox.js >> dwzESC.js
type ..\javascripts\dwz.history.js >> dwzESC.js

cscript ESC.wsf -l 1 -ow dwzESC1.js dwzESC.js
cscript ESC.wsf -l 2 -ow dwzESC2.js dwzESC1.js
cscript ESC.wsf -l 3 -ow dwzESC3.js dwzESC2.js

type dwzESC2.js > dwz.min.js
#gzip -f dwz.min.js
#copy dwz.min.js.gz dwz.min.gzjs /y

del dwzESC*.js
del dwz.min.js.gz