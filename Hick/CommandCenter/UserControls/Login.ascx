<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Login.ascx.cs"
    Inherits="Hick.CommandCenter.UserControls.CCPatientList" %>
<div class="patsearch_border">
    <div class="row ">
                <div class="col-xs-2 col-sm-2 col-md-3 col-lg-3 border">
                </div>
                <!--changed mobile columns(xs) to 12-->
                <div class="col-xs-12  col-sm-8 col-md-6 col-lg-6 border">
                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12 border">
                        <div class="border align ">
                            <div class="form-group ">
                                <label for="usrnme" class="fontweight">
                                    Username:</label>
                                <input type="text" class="form-control textarea" onclick="func()" runat="server"
                                    id="txtusername" />
                            </div>
                            <div class="form-group">
                                <label for="pwd" class="fontweight">
                                    Password:</label>
                                <input type="password" class="form-control textarea" onclick="func()" runat="server"
                                    id="txtpassword" />
                            </div>
                            <div class="checkbox">
                                <ul id="menu" class="padding">
                                    <li>
                                        <label>
                                            <input type="checkbox" />
                                            Remember me</label></li>
                                    <li class="Remem"><a href="#" class="linkcolor">FORGOT PASSWORD?</a></li>
                                </ul>
                            </div>                                                        
                            <asp:Button ID="Button1" runat="server" OnClick="btn_click" class="btn btn-default textareaSubmit"
                               Text="Sign in"></asp:Button>
                            <br>
                            <p class="CreateAlign">
                                or <a href="#" class="Create">Create an Account</a>
                            </p>
                        </div>
                    </div>
                </div>
                <div class="col-xs-2 col-sm-2 col-md-3 col-lg-3 border">
                </div>
                <!--<div class="  col-xs-2 col-sm-2 col-md-3 col-lg-3 border2 paddingSteth"><div class="footer border1 "><img class="imgsize img-responsive"  src="imgs\stethoscope.jpg"></div> </div>-->
            </div>
</div>
<script type="text/javascript">
    $("#div_command").css("display", "block");
    $("#command_leftpart").css("display", "block");

    $("#commandcenter").css("display","block");
</script>
