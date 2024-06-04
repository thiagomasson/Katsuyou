<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Katsuyou.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            var textInput = document.getElementById('txtAnswer');
            wanakana.bind(textInput)
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    var textInput = document.getElementById('txtAnswer');
                    wanakana.bind(textInput)
                }
            });
        };
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="udpVerb" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSend" EventName="click" />
        </Triggers>
        <ContentTemplate>
            <div class="row margin-top-16-important">
                <div class="col-3 box border center">
                    Current streak:
                <asp:Label ID="lblStreak" runat="server" Text="0"></asp:Label>
                    Max streak:
                <asp:Label ID="lblMaxStreak" runat="server" Text="0"></asp:Label>
                </div>
            <div class="row">
                <!-- Dummy Col -->
                <div class="col-3">
                    </div>
                <!-- Col 1 -->
                <div class="col-6 box border">
                    <div class="center">
                        <asp:Label ID="lblID" runat="server" Text="ID" Visible="false"></asp:Label>
                        <asp:Label ID="lblType" runat="server" Text="Type"></asp:Label>
                        <br />
                        <br />
                        <ruby><asp:Label ID="lblVerb" runat="server" Text="Verb" Font-Size="32"></asp:Label><rt><asp:Label ID="lblFurigana" runat="server" Text="かんじ" Font-Size="16" Visible="false"></asp:Label></rt></ruby>
                        <br />
                        <asp:Label ID="lblTranslation" runat="server" Text="Translation"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="lblConjugations" runat="server" Text="Conjugations" Font-Size="12"></asp:Label>
                        <input type="text" id="txtAnswer" runat="server" style="margin: 8px 0px; padding: 8px" clientidmode="Static" placeholder="Type conjugated form..." />
                        <script type="text/javascript">
                            var textInput = document.getElementById('txtAnswer');
                            wanakana.bind(textInput)
                        </script>
                        <asp:Button ID="btnSend" CssClass="send-button" runat="server" OnClick="btnSend_Click" Text="Check" />
                        <br />
                        <br />
                        <div id="divAnswer" visible="false" runat="server">
                            <asp:Label ID="lblCorrection" runat="server"></asp:Label>
                        </div>
                        <asp:Button ID="btnGiveUp" OnClick="btnGiveUp_Click" runat="server" Text="Give Up" Visible="false" />
                        <asp:Label ID="lblConjugated" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <!-- Col 2 -->
                <div class="col-3">
                    <div class="center" runat="server">
                        <asp:Image ID="imgVerb" Width="100%" CssClass="box border" ImageUrl="https://via.placeholder.com/100?text=No+Image" runat="server" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
