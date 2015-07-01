<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiRK.TwirkIt.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Twirk It!</title>
	<script type='text/javascript' src='Scripts/jquery-2.1.4.js'></script>
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
						alert('need atleast 5 cards');
						return false;
					}

					for (var i = 0; i < cardInts.length; i++) {
						var cv = cardInts[i];

						if (cv % 10 != 0 || cv < 10 || cv > 840) {
							alert('Invalid Card priority: ' + cv);
							return false;
						}
					}

					return true;
				}
			</script>

			<asp:Button runat="server" ID="btnRunSimulations" OnClientClick="return ValidateSimulate();" OnClick="btnRunSimulations_OnClick" Text="Run Simulations!" />
			<p>Enter comma seperated list of card priorities</p>
			<input type="text" id="cards" name="Cards" value="" />
			<input type="hidden" id="robotPosition" name="RobotPosition" value="0,0" />
			<input type="hidden" id="robotOrientation" name="RobotOrientation" value="1" />
		</div>
		<div>
			<p>Click the board to set your robot position and direction</p>
			<div id="map" style="background: url('Images/ScottRallyMap.png'); background-size: 100%; width: 576px; height: 1152px">
				<div id="robot" style="background: url('Images/Arrow-100.png'); background-size: 100%; width: 48px; height: 48px; position: relative;"></div>
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

				var robot = document.getElementById("robot");

				var currentPosition = document.getElementById("robotPosition").value;
				var clickedPosition = x + "," + y;
				if (currentPosition == clickedPosition)
				{
					// rotate
					var orientation = (parseInt(document.getElementById("robotOrientation").value) + 1) % 4;
					document.getElementById("robotOrientation").value = orientation;
					var angle = orientation * 90;
					$("#robot").css('transform', 'rotate(' + angle + 'deg)');
				}
				else
				{
					// move
					document.getElementById("robotPosition").value = clickedPosition;
					robot.style.left = (x * TILE_EDGE_SIZE) + "px";
					robot.style.top = (y * TILE_EDGE_SIZE) + "px";
				}
			});
		</script>
	</form>
</body>
</html>
