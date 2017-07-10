<%@ Page Language="C#" Inherits="SiteServer.Restriction.PageOptions" %>
<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <link href="assets/plugin-utils/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugin-utils/css/plugin-utils.css" rel="stylesheet" type="text/css" />
    <link href="assets/plugin-utils/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <div style="padding: 20px 0;">

        <div class="container">
            <form id="form" runat="server" class="form-horizontal">

                <div class="row">
                    <div class="card-box">
                        <div class="row">
                            <div class="col-lg-10">
                                <h4 class="m-t-0 header-title"><b>后台访问限制配置</b></h4>
                                <p class="text-muted font-13 m-b-30">
                                    在此配置管理后台的访问限制选项
                                </p>
                            </div>
                        </div>

                        <asp:Literal id="LtlMessage" runat="server" />

                        <div class="row">

                            <div class="form-group">
                                <label class="col-sm-3 control-label">访问限制选项</label>
                                <div class="col-sm-6">
                                    <asp:RadioButtonList ID="RblRestrictionType" RepeatDirection="Vertical" runat="server"></asp:RadioButtonList>
                                </div>
                                <div class="col-sm-3">
                                    <span v-bind:style="{ display: accessKeyId ? 'none' : '' }" style="color: red; display: inline;"> *</span>
                                </div>
                            </div>

                            <div class="form-group m-b-0">
                                <div class="col-sm-offset-3 col-sm-9">
                                    <asp:Button class="btn btn-primary" id="Submit" text="确 定" onclick="Submit_OnClick" runat="server" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </form>
        </div>
    </div>
</body>

</html>