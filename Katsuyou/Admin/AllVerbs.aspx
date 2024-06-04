<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AllVerbs.aspx.cs" Inherits="Katsuyou.Admin.AllVerbs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Conteudo" runat="server">
    <div>
        <div class="center">
            <asp:Button ID="btnNew" CssClass="send-button margin-top-16-important" OnClick="btnNew_Click" Width="50%" runat="server" Text="New" />
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpImage" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridViewVerbs" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="stage" runat="server" CssClass="margin-top-16-important" Style="border: 1px solid #000000;" data-ajax="false">
                    <asp:GridView ID="GridViewVerbs" AllowPaging="true" PageSize="50" OnPageIndexChanging="GridViewVerbs_PageIndexChanging" CellPadding="4" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="GridViewVerbs_SelectedIndexChanged" OnRowEditing="GridViewVerbs_RowEditing" OnRowUpdating="GridViewVerbs_RowUpdating" OnRowCancelingEdit="GridViewVerbs_RowCancelingEdit" runat="server">
                        <HeaderStyle BackColor="#26a69a" />
                        <PagerSettings Mode="NumericFirstLast" FirstPageText="First" PreviousPageText="Previous" NextPageText="Next" LastPageText="Last" />
                        <PagerStyle BackColor="#fce438" Font-Bold="true" ForeColor="#212121" />
                        <RowStyle BackColor="#313131" />
                        <RowStyle BackColor="#212121" />
                        <AlternatingRowStyle BackColor="#313131" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnSelect" runat="server" Text="Select" CommandName="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Save" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("VerbID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Verb">
                                <ItemTemplate>
                                    <asp:Label ID="lblVerb" runat="server" Text='<%#Eval("DictionaryForm") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtVerb" runat="server" Text='<%#Eval("DictionaryForm") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Translation">
                                <ItemTemplate>
                                    <asp:Label ID="lblTranslation" runat="server" Text='<%#Eval("English") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTranslation" runat="server" Text='<%#Eval("English") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" SelectedValue='<%#Eval("Type") %>'>
                                        <asp:ListItem Text="Godan" Value="Godan"></asp:ListItem>
                                        <asp:ListItem Text="Ichidan" Value="Ichidan"></asp:ListItem>
                                        <asp:ListItem Text="Desu" Value="Desu"></asp:ListItem>
                                        <asp:ListItem Text="Suru" Value="Suru"></asp:ListItem>
                                        <asp:ListItem Text="Kuru" Value="Kuru"></asp:ListItem>
                                        <asp:ListItem Text="Keigo" Value="Keigo"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server" SelectedValue='<%#Eval("Status") %>'>
                                        <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Inactive" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Content>
