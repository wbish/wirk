<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiRK.TwirkIt.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Twirk It!</title>
	<script type='text/javascript' src='Scripts/jquery-2.1.4.js'></script>
	<%-- ReSharper disable CssBrowserCompatibility --%>
	<style>
		.robot {
			width: 48px;
			height: 48px;
			background-size: 100%;
			position: absolute;
		}

		.startRobot {
			background: url('Images/RobotArrow.png');
		}

		.resultRobot {
			background: url('Images/ResultArrow.png');
		}

		.grayedOutRegister {
			color: lightgray;
		}
	</style>
	<%-- ReSharper restore CssBrowserCompatibility --%>
	<script type="text/javascript" src="Scripts/tiledmap.js"></script>
</head>
<body>
	<form id="form1" runat="server">
		<h1>Twirk It!</h1>
		<h2>William's Robots Calculator (WiRK)</h2>
		<div>
			<asp:Button runat="server" ID="btnRunSimulations" OnClientClick="return ValidateSimulate();" OnClick="btnRunSimulations_OnClick" Text="Run Simulations!" />
			<asp:Button runat="server" ID="btnCleanResults" OnClientClick="return cleanResults();" Text="Clean Results" />

			<p>Enter comma seperated list of card priorities or move card abbreviations (UTurn = U; BackUp = B, RotateRight = R, RotateLeft = L; Move1 = 1, Move2 = 2, Move3 = 3). For example: 310,20,830,740,600 or L,B,3,2,1</p>
			<input type="text" id="cards" name="Cards" value="<%=ViewState["Cards"] %>" />
			<input type="hidden" id="robotPosition" name="RobotPosition" value="<%=ViewState["PosX"]%>,<%=ViewState["PosY"]%>" />
			<input type="hidden" id="robotOrientation" name="RobotOrientation" value="<%=ViewState["Facing"]%>" />
		</div>

		<div>
			<p>Click the board to set your robot position and direction.</p>
			<p>Once you have calculated your moves, click a blue arrow to reveal ways to get there.</p>
		</div>

		<table>
			<tr>
				<td>
					<div>
						<div id="tiledMapDiv" style="width: 600px; position: relative;">
							<div id="robot" class="startRobot robot"></div>
						</div>
					</div>
				</td>
				<td style="vertical-align: top">
					<div>
						<h2>Results</h2>
						<div id="results-permutations"></div>
					</div>
				</td>
			</tr>
		</table>
		
		<div>
			<img src="Images/doggy.gif" alt="Twirky the Dog" />
		</div>

		<div>
			<p>Enjoy TwirkIt? <a href="https://github.com/wbish/wirk">Make it better!</a></p>
			<p><a href="javascript:alert('I promise not to look at your cards. Maybe.');">EULA</a></p>
		</div>

		<%-- ReSharper disable once AssignToImplicitGlobalInFunctionScope --%>
		<script type="text/javascript" src="Scripts/twirk.js"></script>
		<script type="text/javascript">
			var map = <%=ViewState["Map"]%>;

			renderTiledMap(document.getElementById("tiledMapDiv"), map);

			setOrientation(<%=ViewState["Facing"]%>);
			setRobot(<%=ViewState["PosX"]%>, <%=ViewState["PosY"]%>);
			showResults();
		</script>
	</form>
</body>
</html>
