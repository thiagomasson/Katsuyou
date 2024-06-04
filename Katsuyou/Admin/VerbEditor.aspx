<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerbEditor.aspx.cs" Inherits="Katsuyou.Admin.VerbEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <div class="row margin-top-16-important">
        <!-- Col 1 -->
        <div class="col-6">
            <div class="box border">
                <h3>Verb Editor</h3>
                <asp:Label ID="lblAlert" ForeColor="Red" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblID" runat="server" Visible="false"></asp:Label>
                <label>Dictionary Form</label>
                <input type="text" ID="txtVerb" runat="server" clientidmode="Static" />
                <script type="text/javascript">
                    var textInput = document.getElementById('txtVerb');
                    wanakana.bind(textInput)
                </script>
                <label>Translation</label>
                <asp:TextBox ID="txtTranslation" runat="server"></asp:TextBox>
                <label>Type</label>
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Text="Godan" Value="Godan"></asp:ListItem>
                    <asp:ListItem Text="Ichidan" Value="Ichidan"></asp:ListItem>
                    <asp:ListItem Text="Desu" Value="Desu"></asp:ListItem>
                    <asp:ListItem Text="Suru" Value="Suru"></asp:ListItem>
                    <asp:ListItem Text="Kuru" Value="Kuru"></asp:ListItem>
                    <asp:ListItem Text="Keigo" Value="Keigo"></asp:ListItem>
                </asp:DropDownList>
                <label>Honorific Type</label>
                <asp:DropDownList ID="ddlHonorificType" runat="server">
                    <asp:ListItem Text="None" Value="None"></asp:ListItem>
                    <asp:ListItem Text="Respectful" Value="Respectful"></asp:ListItem>
                    <asp:ListItem Text="Humble" Value="Humble"></asp:ListItem>
                </asp:DropDownList>
                <label>Status</label>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                </asp:DropDownList>
                <label>Image URL</label>
                <div class="row">
                    <!-- Col 1 -->
                    <div class="col-6">
                        <asp:TextBox ID="txtImage" runat="server"></asp:TextBox>
                        <div class="center">
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" />
                        </div>
                    </div>
                    <div class="col-6">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:UpdatePanel ID="udpImage" runat="server" UpdateMode="Conditional">
                            <triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="click" />
                            </triggers>
                            <contenttemplate>
                                <asp:Panel ID="stage" runat="server" CssClass="containment-wrapper" Style="border: 1px solid #000000;" data-ajax="false">
                                    <div id="divImagePreview" class="center" runat="server">
                                        <asp:Image ID="imgVerb" Width="100%" Heigh="200px" ImageUrl="https://via.placeholder.com/100?text=No+Image" runat="server" />
                                    </div>
                                </asp:Panel>
                            </contenttemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="center">
                    <asp:Button ID="btnSave" CssClass="send-button" OnClick="btnSave_Click" runat="server" Text="Save" Width="300" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
