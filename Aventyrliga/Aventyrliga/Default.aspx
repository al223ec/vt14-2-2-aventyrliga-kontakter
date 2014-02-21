<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aventyrliga.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ListView ID="ContactListView" runat="server"
                ItemType="Aventyrliga.Model.Contact"
                SelectMethod="ContactListView_GetData"
                InsertMethod="ContactListView_InsertItem"
                UpdateMethod="ContactListView_UpdateItem"
                DeleteMethod="ContactListView_DeleteItem"
                DataKeyNames="ContactID"
                InsertItemPosition="FirstItem">
                <LayoutTemplate>
                    <table>
                        <tr>
                            <th>
                                Förnamn
                            </th>
                            <th>
                                Efternamn
                            </th>
                            <th>
                                Email
                            </th>
                        </tr>
                        <%-- Platshållare för nya rader --%>
                        <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <%-- Mall för nya rader. --%>
                    <tr>
                        <td>
                            <%#: Item %>
                        </td>
                        <td>
                            <%#: Item.FirstName %>
                        </td>
                        <td>
                            <%#: Item.LastName %>
                        </td>
                        <td>
                            <%#: Item.EmailAddress %>
                        </td>
                        <td class="command">
                            <%-- "Kommandknappar" för att ta bort och redigera kunduppgifter. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="Ta bort" CausesValidation="false" />
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                        </td>
                    </tr>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <%-- Detta visas då kunduppgifter saknas i databasen. --%>
                    <table class="grid">
                        <tr>
                            <td>
                                Uppgifter saknas saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <%-- Mall för rad i tabellen för att lägga till nya kunduppgifter. Visas bara om InsertItemPosition 
                     har värdet FirstItemPosition eller LasItemPosition.--%>
                    <tr>
                        <td>
                            <asp:TextBox ID="Name" runat="server" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Address" runat="server" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="PostalCode" runat="server" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        
                        <td>
                            <%-- "Kommandknappar" för att lägga till en ny kunduppgift och rensa texfälten. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Insert" Text="Lägg till" />
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <%-- Mall för rad i tabellen för att redigera kunduppgifter. --%>
                    <tr>
                        <td>
                            <asp:TextBox ID="Name" runat="server" Text='<%# BindItem.FirstName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="Address" runat="server" Text='<%# BindItem.LastName %>' />
                        </td>
                        <td>
                            <asp:TextBox ID="PostalCode" runat="server" Text='<%# BindItem.EmailAddress %>' />
                        </td>
                        
                        <td>
                            <%-- "Kommandknappar" för att uppdatera en kunduppgift och avbryta. Kommandonamnen är VIKTIGA! --%>
                            <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Text="Spara" />
                            <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                        </td>
                    </tr>
                </EditItemTemplate>
            </asp:ListView>
    </div>
    </form>
</body>
</html>
