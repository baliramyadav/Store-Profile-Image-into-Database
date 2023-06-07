<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="GridView_Editing_With_SP.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Details</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h3>Customer Details | GridView Editing with Stored Procedure: </h3>
            <hr />
            <table class="table" align="center" border="1">
                <caption>Customer Details</caption>
                <tr>
                    <td align="right">Customer Id:</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">Customer Name:</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">Customer Balance:</td>
                    <td>
                        <asp:TextBox ID="txtBalance" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">Customer City:</td>
                    <td>
                        <asp:TextBox ID="txtCity" runat="server" /></td>
                </tr>
                <tr>
                    <td align="right">Customer Status:</td>
                    <td>
                        <asp:CheckBox ID="cbStatus" runat="server" /></td>
                </tr>

                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnInsert" CssClass="btn btn-success" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                    </td>
                </tr>
            </table>


            <div class="table" width="60%">
                <h3>Customer List</h3>
                <hr />
                <asp:GridView ID="GridView1" runat="server" CssClass="table" DataKeyNames="Custid" AutoGenerateColumns="false" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" OnRowDeleting="GridView1_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Custid" HeaderText="Custid" ReadOnly="true" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Balance" HeaderText="Balance" />
                        <asp:BoundField DataField="City" HeaderText="City" />
                        <asp:CheckBoxField DataField="Status" HeaderText="Status" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="edit">Edit</asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="delete" OnClientClick="return confirm('Are you sure of deleting the current record?')">Delete</asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="btnUpdate" runat="server" CommandName="update">Update</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" CommandName="cancel">Cancel</asp:LinkButton>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </form>
   
</body>
</html>
