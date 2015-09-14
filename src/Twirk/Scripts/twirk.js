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
		var ucv = cv.toUpperCase();

		if (ucv != 'U' && ucv != 'B' && ucv != 'R' && ucv != 'L' && ucv != '1' && ucv != '2' && ucv != '3') {
			if (cv % 10 != 0 || cv < 10 || cv > 840) {
				alert('Invalid card priority: ' + cv);
				return false;
			}
		}
	}

	return true;
}

var TILE_EDGE_SIZE = 50;

$('#tiledMapDiv').on('click', function (e) {
	var x = Math.floor((e.pageX - this.offsetLeft) / TILE_EDGE_SIZE);
	var y = Math.floor((e.pageY - this.offsetTop) / TILE_EDGE_SIZE);


	// We are placing the robot for move calculation
	if (typeof results == 'undefined' || results == null) {
		var currentPosition = document.getElementById("robotPosition").value;
		var clickedPosition = x + "," + y;

		if (currentPosition == clickedPosition) {
			setOrientation(parseInt(document.getElementById("robotOrientation").value) + 1);
		} else {
			setRobot(x, y);
		}
	}
	else {
		// We are trying to look at results
		var resultsDiv = $("#results-permutations");
		resultsDiv.empty();

		for (var j = 5; j > 0; --j) {
			var inited = false;
			var listId = "results-register-" + j;
			for (var i = 0; i < results.length; ++i) {
				if (results[i][j - 1].Position.X == x && results[i][j - 1].Position.Y == y) {
					if (!inited) {
						inited = true;
						resultsDiv.append("<div><h3>Register " + j + "</h3><ul id='" + listId + "'></ul></div>");
					}

					var cards = "";
					for (var k = 0; k < j; ++k) {
						var damage = results[i][k].Damage > 0 ? " (+" + results[i][k].Damage + " damage)" : "";
						cards += cardType(results[i][k].Card) + damage + ' ; ';
					}
					cards += "Facing == " + facing(results[i][j - 1].Facing);

					for (var q = j + 1; q <= 5; ++q) {
						var futureDamage = results[i][q - 1].Damage > 0 ? " (+" + results[i][q - 1].Damage + " damage)" : "";
						cards += "<span class='grayedOutRegister'> ; " + cardType(results[i][q - 1].Card) + futureDamage + "</span>";
					}

					var registerList = $("#" + listId);
					registerList.append("<li class=\"permutation-result\">" + cards + "</li>");
				}
			}
		}
	}
});

$(".permutation-result").hover(
	function () { $(".resultRobot").hide(); },
	function () { $(".resultRobot").show(); }
);

function setOrientation(x) {
	// rotate
	var orientation = x % 4;
	document.getElementById("robotOrientation").value = orientation;
	var angle = orientation * 90;
	$("#robot").css('transform', 'rotate(' + angle + 'deg)');
}

function setRobot(x, y) {
	var clickedPosition = x + "," + y;
	var robot = document.getElementById("robot");
	document.getElementById("robotPosition").value = clickedPosition;
	robot.style.left = (x * TILE_EDGE_SIZE) + "px";
	robot.style.top = (y * TILE_EDGE_SIZE) + "px";
}

function showResults() {

	$(".resultRobot").remove();

	if (typeof results == 'undefined' || results == null)
		return;

	for (var i = 0; i < results.length; ++i) {
		var left = (results[i][4].Position.X * TILE_EDGE_SIZE) + "px";
		var top = (results[i][4].Position.Y * TILE_EDGE_SIZE) + "px";
		var rotate = results[i][4].Facing * 90;
		$("#tiledMapDiv").append("<div class=\"resultRobot robot\" style=\"left:" + left + ";top:" + top + ";transform:rotate(" + rotate + "deg);\"></div>");
	}
}

function cleanResults() {
	results = null;
	showResults();
	return false;
}

function cardType(x) {
	if (x == 0)
		return "UTurn";
	else if (x == 1)
		return "RotateLeft";
	else if (x == 2)
		return "RotateRight";
	else if (x == 3)
		return "BackUp";
	else if (x == 4)
		return "Move1";
	else if (x == 5)
		return "Move2";
	else
		return "Move3";
}

function facing(f) {
	if (f == 0)
		return "Up";
	else if (f == 1)
		return "Right";
	else if (f == 2)
		return "Down";
	else
		return "Left";
}