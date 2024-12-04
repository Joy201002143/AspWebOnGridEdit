<%@ Page Title="Employee Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OnGridEdit._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">Employee Management</h3>
            </div>
            <div class="card-body">
                <asp:Panel ID="Panel1" runat="server" CssClass="mb-4">
                    <asp:GridView
                        ID="GridView1"
                        runat="server"
                        AutoGenerateColumns="False"
                        OnRowDataBound="GridView1_RowDataBound"
                        CssClass="table table-bordered table-striped"
                        HeaderStyle-CssClass="table-dark"
                        RowStyle-CssClass="align-middle">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee ID" Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtID" runat="server" Text='<%# Eval("EMPID") %>' Visible="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" Text='<%# Eval("Designation") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Joining Salary">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" Text='<%# Eval("Salary") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <sdiv class="d-flex justify-content-between mt-4">
                        <asp:Button ID="btnAdd" runat="server" Text="Add Row" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Save Data" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <asp:Button
                            ID="btnDelete"
                            runat="server"
                            Text="Delete Selected"
                            CssClass="btn btn-danger"
                            OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Are you sure you want to delete?');" />
                    </sdiv>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
