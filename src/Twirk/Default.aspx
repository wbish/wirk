<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiRK.TwirkIt.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Twirk It!</title>
	<script type='text/javascript' src='Scripts/jquery-2.1.4.js'></script>
	<script type="text/javascript">
		var results = null;
	</script>
	<style>
		.robot 
		{
			width: 48px;
			height: 48px;
			background-size: 100%;
			position:absolute;
		}

		.startRobot
		{
			background:url('Images/RobotArrow.png');
		}

		.resultRobot
		{
			background:url('Images/ResultArrow.png');
		}

		#map
		{
			background: url('Images/ScottRallyMap.png'); 
			background-size: 100%; 
			width: 576px; 
			height: 1152px;
			position:relative;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
		<h1>Twirk It!</h1>
		<h2>William's RoboRally Calculator (WiRK)</h2>
		<div>
			<script type="text/javascript">
				function ValidateSimulate() {
					var cards = document.getElementById('cards').value;

					if (cards == "") {
						alert('You need to tell me what cards you have first.');
						return false;
					}

					var cardInts = cards.split(",");

					if (cardInts.length < 5) {
						alert('You should have atleast 5 cards');
						return false;
					}

					for (var i = 0; i < cardInts.length; i++) {
						var cv = cardInts[i];

						if (cv % 10 != 0 || cv < 10 || cv > 840) {
							alert('Invalid card priority: ' + cv);
							return false;
						}
					}

					return true;
				}
			</script>

			<asp:Button runat="server" ID="btnRunSimulations" OnClientClick="return ValidateSimulate();" OnClick="btnRunSimulations_OnClick" Text="Run Simulations!" />
			<p>Enter comma seperated list of card priorities</p>
			<input type="text" id="cards" name="Cards" value="<%=ViewState["Cards"] %>" />
			<input type="hidden" id="robotPosition" name="RobotPosition" value="<%=ViewState["PosX"]%>,<%=ViewState["PosY"]%>" />
			<input type="hidden" id="robotOrientation" name="RobotOrientation" value="<%=ViewState["Facing"]%>" />
		</div>
		<div>
			<p>Click the board to set your robot position and direction</p>
			<div id="map">
				<div id="robot" class="startRobot robot"></div>
			</div>
		</div>
		<div>
			<p>Enjoy TwirkIt? <a href="https://github.com/wbish/roborally-wirk">Make it better!</a></p>
			<p><a href="javascript:alert('William gets to see all your shiznit');">EULA</a></p>
		</div>
		<script type="text/javascript">
			var TILE_EDGE_SIZE = 48;

			$('#map').on('click', function (e) {
				var x = Math.floor((e.pageX - this.offsetLeft) / TILE_EDGE_SIZE);
				var y = Math.floor((e.pageY - this.offsetTop) / TILE_EDGE_SIZE);

				var currentPosition = document.getElementById("robotPosition").value;
				var clickedPosition = x + "," + y;

				if (currentPosition == clickedPosition)
				{
					setOrientation(parseInt(document.getElementById("robotOrientation").value) + 1);
				}
				else
				{
					setRobot(x, y);
				}
			});

			function setOrientation(x)
			{
				// rotate
				var orientation = x % 4;
				document.getElementById("robotOrientation").value = orientation;
				var angle = orientation * 90;
				$("#robot").css('transform', 'rotate(' + angle + 'deg)');
			}

			function setRobot(x,y)
			{
				var clickedPosition = x + "," + y;
				var robot = document.getElementById("robot");
				document.getElementById("robotPosition").value = clickedPosition;
				robot.style.left = (x * TILE_EDGE_SIZE) + "px";
				robot.style.top = (y * TILE_EDGE_SIZE) + "px";
			}

			function showResults() {
				if (results == null)
					return;

				for (var i = 0; i < results.length; ++i) 
				{
					var left = (results[i][4].Position.X * TILE_EDGE_SIZE) + "px";
					var top = (results[i][4].Position.Y * TILE_EDGE_SIZE) + "px";
					var rotate = results[i][4].Facing * 90;
					$("#map").append("<div class=\"resultRobot robot\" style=\"left:" + left + ";top:" + top + ";transform:rotate("+rotate+"deg);\"></div>");
				}
			}

			setOrientation(<%=ViewState["Facing"]%>);
			setRobot(<%=ViewState["PosX"]%>, <%=ViewState["PosY"]%>);
			showResults();
		</script>
	</form>
</body>
</html>
