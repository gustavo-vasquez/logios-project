//<![CDATA[
			function wrs_urlencode(clearString) {
				var output = '';
				var x = 0;
				clearString = clearString.toString();
				var regex = /(^[a-zA-Z0-9_.]*)/;
				
				var clearString_length = ((typeof clearString.length) == 'function') ? clearString.length() : clearString.length;

				while (x < clearString_length) {
					var match = regex.exec(clearString.substr(x));
					if (match != null && match.length > 1 && match[1] != '') {
						output += match[1];
						x += match[1].length;
					}
					else {
						var charCode = clearString.charCodeAt(x);
						var hexVal = charCode.toString(16);
						output += '%' + ( hexVal.length < 2 ? '0' : '' ) + hexVal.toUpperCase();
						++x;
					}
				}
				
				return output;
			}

			function wrs_mathmlEntities(mathml) {
				var toReturn = '';
				
				for (var i = 0; i < mathml.length; ++i) {
					//parsing > 128 characters
					if (mathml.charCodeAt(i) > 128) {
						toReturn += '&#' + mathml.charCodeAt(i) + ';';
					}
					else {
						toReturn += mathml.charAt(i);
					}
				}
				
				return toReturn;
			}

			function openResource(url, mathml) {
				wnd = window.open(url + '?mml=' + wrs_urlencode(wrs_mathmlEntities(mathml)) + '&backgroundColor=%23fff',"new_window","width=350,height=200,location=0,status=0,toolbar=0,top=100,left=500");
				wnd.focus();
			}

			
			function setLanguage() {
				var i, str;
				str=""+location;
				i=str.lastIndexOf("/index.html",i);
				if (str[i-3]=="/") {
					// .../xx/demo.html
					str = str.substring(i-2,i)
					setCookie("lang",str,1);
				}
				
			}

			function changeLanguage(lang, page) {
				if (lang.length>0) {
					location.href="../"+lang+"/"+page;
				}
			}
			
			function getMathML(latex) {
				var req = new XMLHttpRequest();
				req.open("POST","/editor/demo/latex2mathml", false);
				req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
				var params = "latex="+encodeURIComponent(latex);
				req.setRequestHeader("Content-length", params.length);
				req.setRequestHeader("Connection", "close");
				req.send(params);
				if (req.status != 200)  {
					return "Error generating MathML.";
				}
				return req.responseText;
			}
			
			function getLaTeX(mathml, callback) {
				var req = new XMLHttpRequest();
				req.open("POST","/editor/demo/mathml2latex", false);
				req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
				var params = "mml="+encodeURIComponent(mathml);
				req.setRequestHeader("Content-length", params.length);
				req.setRequestHeader("Connection", "close");
				
				req.onreadystatechange = function () {
					if (req.readyState == 4) {
						if (req.status != 200)  {
							callback("Error generating LaTeX.");
						}
						else {
							callback(req.responseText);
						}
					}
				}
				
				req.send(params);
			}
			
			function getAccessible(mathml, callback, lang) {
				var req = new XMLHttpRequest();
				req.open("POST","/editor/demo/mathml2accessible", true);
				req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
				var params = "mml="+encodeURIComponent(mathml)+"&lang="+lang;
				req.setRequestHeader("Content-length", params.length);
				req.setRequestHeader("Connection", "close");
				
				req.onreadystatechange = function () {
					if (req.readyState == 4) {
						if (req.status != 200)  {
							callback("Error generating accessible text.");
						}
						else {
							callback(req.responseText);
						}
					}
				}
				
				req.send(params);
			}
			
			function getParameter(param, deft)
			{
				var i, str;
				str=""+location;
				i=str.indexOf(param+"=");
				if (i>=0)
				{
					str=str.substr(i+param.length+1);
					i=str.indexOf("&");
					if (i>=0) str=str.substring(0,i);
					str=str.replace(/\+/g," ");
					return decodeURIComponent(str);
				}
				else
				{
					return deft;
				}
			}
			function getCookie(c_name)
			{
				var i,x,y,ARRcookies=document.cookie.split(";");
				for (i=0;i<ARRcookies.length;i++)
				{
					x=ARRcookies[i].substr(0,ARRcookies[i].indexOf("="));
					y=ARRcookies[i].substr(ARRcookies[i].indexOf("=")+1);
					x=x.replace(/^\s+|\s+$/g,"");
					if (x==c_name)
					{
						return unescape(y);
					}
				}
			}
			function setCookie(c_name,value,exdays,path)
			{
				var exdate=new Date();
				exdate.setDate(exdate.getDate() + exdays);
				var c_value=escape(value) + ((exdays==null) ? "" : "; expires="+exdate.toUTCString());
				c_value += ";path=/";
				document.cookie=c_name + "=" + c_value;
			}
			function changeToolbar(toolbar, instance)
			{
				com.wiris.jsEditor.JsEditor.getInstance(document.getElementById(instance)).setParams({'toolbar': toolbar});
			}
			
			function disablePARCCButtons() {
				var parcbuttons = document.getElementsByClassName("active");
				for(var i = 0; i < parcbuttons.length; i++) {
					var parcbutton = parcbuttons[i];
					parcbutton.className = parcbutton.className.replace(' active', ' ');
			    }
			}
			//]]>