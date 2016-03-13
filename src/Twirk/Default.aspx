<%@ Page Language="C#" Title="Twirk It!" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WiRK.TwirkIt.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->

	<meta name="language" content="english" />
	<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" integrity="sha384-1q8mTJOASx8j1Au+a5WDVnPi2lkFfwwEAa8hDDdjZlpLegxhjVME1fgjWPGmkzs7" crossorigin="anonymous" />
	<link rel="stylesheet" href="Styles/Twirk.css" />
</head>
<body>

	<div class="container">
		<header>
			<nav class="navbar navbar-inverse navbar-fixed-top">
				<div class="container">
					<div class="navbar-header">
						<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
							<span class="sr-only">Toggle navigation</span>
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
							<span class="icon-bar"></span>
						</button>
						<a class="navbar-brand" href="/">Twirk It!</a>
					</div>
					<div id="navbar" class="navbar-collapse collapse">
						<ul class="nav navbar-nav">
							<li><a href="#" onclick="javascript:alert('You agree I can look at your cards and use that information in any way I wish.');">EULA</a></li>
						</ul>
						<ul class="nav navbar-nav navbar-right">
							<li>
								<button type="button" class="btn btn-success navbar-btn" onclick="window.location='https://github.com/wbish/wirk'" role="link">Github</button>
							</li>
						</ul>
					</div>
				</div>
			</nav>
		</header>
		<div class="row">
			<div class="col-lg-7">
				<ol>
					<li class="lead">Click the board to set your initial robot position and orientation.</li>
					<li class="lead">Enter list of card priorities or codes.</li>
					<li class="lead">Click Run Simulations. Then click a blue arrow to reveal ways to get there.</li>
				</ol>
			</div>
			<div class="col-lg-5">
				<input type="text" class="form-control" placeholder="Enter your cards" id="cards" name="Cards" value="" />
				<input type="hidden" id="robotPosition" name="RobotPosition" value="0,0" />
				<input type="hidden" id="robotOrientation" name="RobotOrientation" value="0" />

				<p class="help-block">(UTurn = U; BackUp = B, RotateRight = R, RotateLeft = L; Move1 = 1, Move2 = 2, Move3 = 3).</p>
				<p class="help-block">For example: <strong>310,20,830,740,600</strong> or <strong>L,B,3,2,1</strong></p>

				<input type="submit" class="btn btn-primary" onclick="return RunSimulations();" value="Run Simulations" />
			</div>
		</div>

		<hr />

		<div class="row">
			<div class="col-lg-7">
				<div id="tiledMapDiv" style="width: 600px; position: relative;">
					<div id="robot" class="startRobot robot"></div>
				</div>
			</div>
			<div class="col-lg-5">
				<h2>Results</h2>
				<p>
					<input class="btn btn-warning" type="button" onclick="return cleanResults();" value="Clear Results" />
				</p>
				<div id="results-permutations"></div>
			</div>
		</div>

		<hr />

		<div class="footer text-center">
			<span class="text-muted">2016 &copy; William Bishop</span>
		</div>

	</div>

	<div class="modal fade" id="LoadingImageModal" tabindex="-1" role="dialog" aria-labelledby="LoadingModalLabel" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<h4 class="modal-title">Twirking...</h4>
				</div>
				<div class="modal-body">
					<img src="Images/doggy.gif" alt="Twirking..." class="center-block img-responsive" />
				</div>
			</div>
		</div>
	</div>

	<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
	<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js" integrity="sha384-0mSbJDEHialfmuBBQP6A4Qrprq5OVfW37PRR3j5ELqxss1yVqOtnepnHVP9aJ7xS" crossorigin="anonymous"></script>
	<script type="text/javascript" src="Scripts/tiledmap.js"></script>
	<script type="text/javascript" src="Scripts/twirk.js"></script>
	<script type="text/javascript">
		var map = <%=ViewState["Map"]%>;
		var mapDiv = document.getElementById("tiledMapDiv");

		renderTiledMap(mapDiv, map);

		setOrientation(0);
		setRobot(0,0);
		showResults();

		$(document).ready(function () { 
			$('#cards').bind("keypress", function(e) {
				if (e.keyCode === 13) {
					RunSimulations();
				}
			});
		});
	</script>
</body>
</html>
