<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aventyrliga.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="~/Content/style.css" rel="stylesheet" />
    <title>Anton Ledström, 2-2 - Kontakter</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="MainPanel" runat="server">
            <%-- Header --%>
            <p class="heading">2-2 - Kontakter</p>
            <h1>Äventyrliga kontakter </h1>
            <asp:Panel ID="OutputPanel" runat="server" Visible="false" CssClass="success">
                <h4>
                    <asp:Literal ID="HeaderOutputLiteral" runat="server" /></h4>
                <p>
                    <asp:Literal ID="OutputLiteral" runat="server" />
                </p>
                <p>
                    <asp:Button ID="Button" runat="server" Text="Stäng" CausesValidation="false" />
                </p>
            </asp:Panel>
            <%-- Validering --%>
            <asp:ValidationSummary ID="ValidationSummary" CssClass="error" runat="server" />
            <asp:ListView ID="ContactListView" runat="server" ItemType="Aventyrliga.Model.Contact" SelectMethod="ContactListView_GetData"
                InsertMethod="ContactListView_InsertItem" UpdateMethod="ContactListView_UpdateItem" DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="contactID" InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>Förnamn
                            </th>
                            <th>Efternamn
                            </th>
                            <th>Email
                            </th>
                            <th>Ta bort/Redigera
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                    <asp:DataPager ID="DataPager" runat="server" PageSize="20">
                        <Fields>
                            <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" << "
                                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                            <asp:NumericPagerField />
                            <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" >> "
                                ShowNextPageButton="False" ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <%#: Item.FirstName %>
                        </td>
                        <td>
                            <%#: Item.LastName %>
                        </td>
                        <td>
                            <%#: Item.EmailAddress %>
                        </td>
                        <td>
                            <%-- "Commandknappar" --%>
                            <asp:LinkButton runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false"
                                OnClientClick='<%# String.Format("return confirm(\"Är du säker att du vill ta bort {0} {1} {2}?\")", Item.FirstName, Item.LastName, Item.EmailAddress) %>' />
                            <asp:LinkButton runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då contact saknas i databasen. --%>
                    <table>
                        <tr>
                            <td>Uppgifter saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter.--%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" MaxLength="50" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" MaxLength="50" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Emailaddress" runat="server" MaxLength="50" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        <td>
                            <%-- "Commandknappar" --%>
                            <asp:LinkButton runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="FirstName" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="LastName" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="En emailadress måste anges" ControlToValidate="Emailaddress" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="Emailadressen verkar inte vara giltig" ControlToValidate="Emailaddress" CssClass="error" Display="Static" ValidationGroup="ValidationSummary"
                            ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$">
                            <%-- http://msdn.microsoft.com/en-us/library/ff650303.aspx --%>
                        </asp:RegularExpressionValidator>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Redigera. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="FirstName" runat="server" MaxLength="50" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="LastName" runat="server" MaxLength="50" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Emailaddress" runat="server" MaxLength="50" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        <td>
                            <%-- "Commandknappar" --%>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />

                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Ett förnamn måste anges" ControlToValidate="FirstName" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="Ett efternamn måste anges" ControlToValidate="LastName" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="En emailadress måste anges" ControlToValidate="Emailaddress" CssClass="error" Display="Dynamic" ValidationGroup="ValidationSummary"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Emailadressen verkar inte vara giltig" ControlToValidate="Emailaddress" CssClass="error" Display="Static" ValidationGroup="ValidationSummary"
                                ValidationExpression="^(?(&quot;&quot;)(&quot;&quot;.+?&quot;&quot;@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$">
                            <%-- http://msdn.microsoft.com/en-us/library/ff650303.aspx --%>
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
        </asp:Panel>
    </form>
</body>
</html>
