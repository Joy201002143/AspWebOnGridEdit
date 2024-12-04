
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


        </div>
    </main>

</asp:Content>
