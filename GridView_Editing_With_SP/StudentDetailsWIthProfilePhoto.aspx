<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDetailsWIthProfilePhoto.aspx.cs" Inherits="GridView_Editing_With_SP.StudentDetailsWIthProfilePhoto" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store Image into Database</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table  align="center" style="background-color: khaki">
                <caption>Student Data Management</caption>
                <tr>
                    <td>Student Id:</td>
                    <td>
                        <asp:TextBox ID="txtId" runat="server" /></td>
                    <td rowspan="4">
                        <asp:Image ID="imgPhoto" runat="server" Height="200px" Width="200px" BorderStyle="Groove" />
                    </td>
                </tr>
                <tr>
                    <td>Student Name:</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" /></td>
                </tr>
                <tr>
                    <td>Student Class:</td>
                    <td>
                        <asp:TextBox ID="txtClass" runat="server" /></td>
                </tr>
                <tr>
                    <td>Annual Fees:</td>
                    <td>
                        <asp:TextBox ID="txtFees" runat="server" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnSelect" runat="server" Text="Select" Width="100px" OnClick="btnSelect_Click" />
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" Width="100px" OnClick="btnInsert_Click" />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="100px" OnClick="btnDelete_Click" />
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Image" Width="270px" OnClick="btnUpload_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnReset" runat="server" Text="Reset All" Width="478px" OnClick="btnReset_Click" /></td>
                </tr>
            </table>
            <asp:Label ID="lblMsgs" runat="server" ForeColor="Red" />

        </div>
    </form>
</body>
</html>
