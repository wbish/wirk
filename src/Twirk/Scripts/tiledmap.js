tileset_width = 8;
tile_size = 50;

function renderTiledMap(mapDiv, map)
{
	var board_width = map['columns'];
	var board_height = map['rows'];
	var board_backgrounds = map['map'];
	var board_doodads = map['doodads'];

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
