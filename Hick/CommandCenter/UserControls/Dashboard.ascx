 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="Hick.CommandCenter.UserControls.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="~/Content/style.css" rel="stylesheet" />

    <script src="../../Scripts/HighCharts/highcharts.js" type="text/javascript"></script>
    <script src="../../Scripts/HighCharts/highcharts-more.js" type="text/javascript"></script>
    <script src="../../Scripts/HighCharts/solid-gauge.js" type="text/javascript"></script>
        <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body>
    <div class="patsearch_heading">
        Dashboard
        <img src='<%=Page.ResolveUrl("~/Images/popup_close.png") %>' id="popup_close" class="pull-right"
            style="cursor: pointer; margin-top: -5px;" alt="close" />
    </div>
    <div class="container ccm">
        <div class="col-lg-12 col-md-12">
            <div class="col-lg-6 col-md-6">
                <div class="dash_left" style="height:453px;">
                    <div class="dash_head">
                        <b>Total CCM Patients</b>
                    </div>
                    <div class="dash_body_left">
                        <asp:Chart ID="ChartCCM" runat="server" Height="150px" Width="295px">
                            <Legends>
                                <asp:Legend Alignment="Near" Docking="Right" IsTextAutoFit="false" Name="Default"
                                    LegendStyle="Column" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" ChartType="Doughnut" ToolTip="#VALX Count: #VALY">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>
                    <div style="width: 300px; height: 150px; margin-top: 35px; float: left;">
                        <div id="container-speed" style="width: 290px; height: 200px; float: left">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="dash_right_top" >
                    <div class="dash_head">
                        <b>Conditions</b>
                    </div>
                    <div class="dash_body_top">
                        <asp:Chart ID="Chart1" runat="server" Height="180px" Width="300px">
                            <Legends>
                                <asp:Legend Alignment="Near" Docking="Right" IsTextAutoFit="false" Name="Default"
                                    LegendStyle="Column" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" ChartType="Pie" ToolTip="Condition: #VALX Count: #VALY">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                        <%-- <asp:Chart ID="Chart1" runat="server" Height="300px" Width="400px">
                            <Titles>
                                <asp:Title ShadowOffset="3" Name="Title1" />
                            </Titles>
                            <Legends>
                                <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                    LegendStyle="Row" />
                            </Legends>
                            <Series>
                                <asp:Series Name="Default" />
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                            </ChartAreas>
                        </asp:Chart>--%>
                    </div>
                </div>
                <div class="dash_right_bttm">
                    <div class="dash_head">
                        <b>Billing</b>
                    </div>
                    <div class="dash_body_bttm" style="width: 100%;">
                        <div id="div_billinggraph" style="min-width: 150px; height: 200px; margin: 0 auto;">
                        </div>
                        <%--  <asp:Chart ID="ChartBillng" runat="server" Height="100px" Width="140px" Visible="false">
                            <Series>
                                <asp:Series Name="Default" ChartType="Bar" ToolTip="Service: #VALX Count: #VALY"
                                    YValueType="Int32">
                                </asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1">
                                </asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none;">
        <asp:HiddenField ID="hdn_Completed" runat="server" />
        <asp:HiddenField ID="hdn_notcompleted" runat="server" />
        <asp:HiddenField ID="hdn_notimerlog" runat="server" />
        <asp:HiddenField ID="hdn_Complpercentage" runat="server" />
    </div>
</body>
<script type="text/javascript">
    var completedPer = $("#<%=hdn_Complpercentage.ClientID%>").val();
    $(function () {

        var gaugeOptions = {

            chart: {
                type: 'solidgauge'
            },

            title: null,

            pane: {
                center: ['50%', '85%'],
                size: '100%',
                startAngle: -70,
                endAngle: 70,
                background: {
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || '#EEE',
                    innerRadius: '60%',
                    outerRadius: '100%',
                    shape: 'arc'
                }
            },

            tooltip: {
                enabled: false
            },

            // the value axis
            yAxis: {
                stops: [
                [0.1, '#55BF3B'], // green
                [0.5, '#55BF3B'], // yellow
                [0.9, '#55BF3B'] // red
            ],
                lineWidth: 0,
                minorTickInterval: null,
                tickPixelInterval: 400,
                tickWidth: 0,
                title: {
                    y: -70
                },
                labels: {
                    y: 16
                }
            },

            plotOptions: {
                solidgauge: {
                    dataLabels: {
                        y: 5,
                        borderWidth: 0,
                        useHTML: true
                    }
                }
            }
        };

        // The speed gauge
        $('#container-speed').highcharts(Highcharts.merge(gaugeOptions, {
            yAxis: {
                min: 0,
                max: 100,
                title: {
                    text: '<b>CCM Patients Completed for the month</b>'
                }
            },

            credits: {
                enabled: false
            },

            series: [{
                name: 'CCM Patients Completed for the month',
                data: [parseFloat(completedPer)],
                dataLabels: {
                    format: '<div style="text-align:center"><span style="font-size:15px;color:' +
                    ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black') + '">{y}</span><br/>' +
                       '<span style="font-size:12px;color:silver">%</span></div>'
                },
                tooltip: {
                    valueSuffix: '%'
                }
            }]

        }));

    });
</script>
<script type="text/javascript">

    var completed = $("#<%=hdn_Completed.ClientID%>").val();

    var notcompleted = document.getElementById("<%=hdn_notcompleted.ClientID%>").value;
    var notimerlog = document.getElementById("<%=hdn_notimerlog.ClientID%>").value;
    var link = '<%=this.link%>';

    $(function () {
       
        var categoryLinks = {
            '> 20m': window.location.origin + link+'PLComplete.aspx',
            '&lt; 20m': window.location.origin + link + 'PLNotComplete.aspx'
        };
        $('#div_billinggraph').highcharts({
            chart: {
                type: 'column'
            },
            plotOptions: {
                series: {
                    colorByPoint: true
                }
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'category',
                labels: {
                    formatter: function () {
                        return '<a href="' + categoryLinks[this.value] + '">' +
                            this.value + '</a>';
                    },
                    rotation: -45,
                    style: {
                        fontSize: '13px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                    
                }

            },
            yAxis: {
                min: 0,
                title: {
                    text: '<b>Total Patients</b>'
                }
            },
            legend: {
                enabled: false
            },
            tooltip: {
                pointFormat: 'Patients: <b>{point.y}</b>'
            },
            series: [{
                name: 'Patients',
                data:
                [{
                    name: '> 20m',
                    color: '#51B44A',
                    y: parseFloat(completed)

                }, {
                    name: '&lt; 20m',
                    color: '#F05622',
                    y: parseFloat(notcompleted)
                }, {
                    name: 'None',
                    color: '#3E6DA5',
                    y: parseFloat(notimerlog)
                }],

                dataLabels: {
                    enabled: false,
                    rotation: -90,
                    color: '#FFFFFF',
                    align: 'right',
                    format: '', // one decimal
                    y: 10, // 10 pixels down from the top
                    style: {
                        fontSize: '10px',
                        fontFamily: 'Verdana, sans-serif'
                    }
                }
            }]
        });
    });
    
</script>

<script type="text/javascript">
    $("#div_command").css("display", "block");
    $("#command_leftpart").css("display", "block");

    $("#commandcenter").css("display", "block");


    $("#popup_close").click(function () {
        parent.popup_close();
    });
</script>
