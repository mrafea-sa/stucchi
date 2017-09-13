<%@ Control %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>

<link rel="stylesheet" href="/resources/colorpicker/css/colorpicker.css" type="text/css" />
<link rel="stylesheet" media="screen" type="text/css" href="/resources/colorpicker/css/layout.css" />

<script type="text/javascript" src="/resources/colorpicker/js/colorpicker.js"></script>
<script type="text/javascript" src="/resources/colorpicker/js/eye.js"></script>
<script type="text/javascript" src="/resources/colorpicker/js/utils.js"></script>
<script type="text/javascript" src="/resources/colorpicker/js/layout.js?ver=1.0.2"></script>

<asp:Label ID="titleLabel" runat="server" CssClass="sfTxtLbl" />
<asp:TextBox ID="fieldBox" runat="server" CssClass="sfTxt" />
<sf:SitefinityLabel id="descriptionLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfDescription" />
<sf:SitefinityLabel id="exampleLabel" runat="server" WrapperTagName="div" HideIfNoText="true" CssClass="sfExample" />

<script type="text/javascript">
	$('#<%=fieldBox.ClientID%>').ColorPicker({
	onSubmit: function(hsb, hex, rgb, el) {
		$(el).val("#"+hex);
		$(el).ColorPickerHide();
	},
	onBeforeShow: function () {
		$(this).ColorPickerSetColor(this.value);
	}
	})
	.bind('keyup', function(){
		$(this).ColorPickerSetColor(this.value);
	});
</script>