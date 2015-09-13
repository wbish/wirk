tileset_width = 8;
tile_size = 50;
board_width = 12;
board_height = 24;

// Layout is for Maps.MapLayouts.ScottRallyMap
board_backgrounds =
[
	14,  0, 32, 45, 45, 43,  0,  0,  0,  0,  0, 14,
	29, 17, 44,  0,  0,  0, 32, 45, 45,  5,  0,  0,
	 0, 20, 44,  0, 38, 45, 43, 46, 39, 20,  0,  0,
	 0, 20, 44,  0, 44,  0, 38,  0, 18, 25,  0,  0,
	 0, 20, 66, 45, 43, 38, 39,  0, 20,  0,  0,  0,
	 0, 20, 44,  0,  0, 39,  0,  0, 84, 29, 83, 29,
	21, 80, 44, 14,  5,  6,  5,  6, 20,  5, 20,  0,
	 0, 20, 44,  0,  0,  0,  0,  0, 20,  0, 20,  0,
	 0, 20, 38, 37, 37, 35, 18, 21, 25,  0, 20,  0,
	 0, 20,  0,  0,  0, 44, 20,  0,  0,  0, 20,  0,
	21, 25,  0,  0,  0, 44, 20,  0,  0,  0, 20,  0,
	14,  0,  0,  0,  0, 44, 20,  0,  0,  0, 20, 14,

	14, 28,  0,  0,  0, 28, 36,  0,  0,  0,  0, 14,
	 0, 28,  0,  0,  0, 28, 36,  0,  0,  0, 16, 29,
	 0, 28,  0,  0,  0, 28, 36,  0,  0,  0, 28,  0,
	 0, 28,  0, 16, 29, 27, 42, 45, 45, 38, 28,  0,
	 0, 28,  0, 28,  0,  0,  0,  0,  0, 36, 28,  0,
	 0, 28,  5, 28,  6,  5,  6,  5, 14, 36, 82, 29,
	21, 81, 21, 93,  0,  0, 39,  0,  0, 36, 28,  0,
	 0,  0,  0, 28,  0, 39, 38, 34, 37, 64, 28,  0,
	 0,  0, 16, 27,  0, 38,  0, 36,  0, 36, 28,  0,
	 0,  0, 28, 39, 46, 34, 37, 38,  0, 36, 28,  0,
	 0,  0,  5, 37, 37, 41,  0,  0,  0, 36, 24, 21,
	14,  0,  0,  0,  0,  0, 34, 37, 37, 41,  0, 14,
];
board_doodads =
[
	-1, -1, 31, -1, 31, -1, -1, 31, -1, 31, -1, -1,
	-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
	23, 12, -1, -1, -1, 15, -1, -1, -1, 10, 15,  7,
	-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
	23, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  7,
	-1, 12, -1, -1, -1, -1, -1, -1, -1,  1, -1, -1,
	-1, -1, 10, -1, -1, -1, -1, -1,  4, -1,  2, -1,
   129,114,114,121, -1, -1, -1,113,114,114,114,137,
	-1, -1, -1, 15, -1, -1, -1, 11, -1, -1, -1, -1,
	23, -1,116,117,117,117,117,117,117,140, -1,  7,
	-1, -1, -1, -1, -1, 12, -1, -1, -1, -1, -1, -1,
	-1, -1, 15, -1, 15, -1, -1, 15, -1, 15, -1, -1,

	-1, -1, 31, -1, 31, -1, -1, 31, -1, 31, -1, -1,
	-1, -1, -1, -1, -1, -1, 10, -1, -1, -1, -1, -1,
	23, -1,132,117,117,117,117,117,117,124, -1,  7,
	-1, -1, -1, -1,  9, -1, -1, -1, -1, -1, -1, -1,
   129,114,114,114,121, -1, -1, -1,113,114,114,137,
	-1,  4, -1,  2, -1, -1, -1, -1, 12, -1, -1, -1,
	-1, -1,  3, -1, -1, -1, -1, -1, -1, 10, -1, -1,
	23, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  7,
	-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
	23, -1, 12, -1, -1, -1, -1, -1, -1, 10, -1,  7,
	-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
	-1, -1, 15, -1, 15, -1, -1, 15, -1, 15, -1, -1,
];

// TODO: This will render using inline "innerHTML" buildup. Fix that.
function renderTiledMap(mapDiv) {
	var html = "";
	for (var y = 0; y < board_height; y++) {
		for (var x = 0; x < board_width; x++) {
			var tileIndex = board_backgrounds[x + y * board_width];
			var tileX = -(tileIndex % tileset_width) * tile_size;
			var tileY = -Math.floor(tileIndex / tileset_width) * tile_size;

			var backgroundDiv = document.createElement("div");
			backgroundDiv.style.width = tile_size + "px";
			backgroundDiv.style.height = tile_size + "px";
			backgroundDiv.style.background = "url('images/roborally-standard-50dpi-flat-v2.png') " + tileX + "px " + tileY + "px";
			backgroundDiv.style.float = "left";

			var doodadIndex = board_doodads[x + y * board_width];
			if (doodadIndex != -1) {
				var doodadX = -(doodadIndex % tileset_width) * tile_size;
				var doodadY = -Math.floor(doodadIndex / tileset_width) * tile_size;

				var doodadDiv = document.createElement("div");
				doodadDiv.style.width = tile_size + "px";
				doodadDiv.style.height = tile_size + "px";
				doodadDiv.style.background = "url('images/roborally-standard-50dpi-flat-v2.png') " + doodadX + "px " + doodadY + "px";
				backgroundDiv.appendChild(doodadDiv);
			}
			mapDiv.appendChild(backgroundDiv);
		}
	}

	var clearDiv = document.createElement("div");
	clearDiv.style.clear = "left";
	mapDiv.appendChild(clearDiv);
}
