mergeInto(LibraryManager.library, {
	logtopage: function (testCases_Pointer) {
		let clickButton = function () {
			navigator.clipboard.writeText(window.ResultsTable);
		}
		let downloadFile = function () {
			const elem = window.document.createElement('a');
			  let textFileAsBlob = new Blob([window.ResultsTable], { type:"text/plain" });
			elem.href = window.URL.createObjectURL(textFileAsBlob);
			elem.download = "AdroitProfilerLogs.csv";
			document.body.appendChild(elem);
			elem.click();
			document.body.removeChild(elem);
		}

		let testCases = Pointer_stringify(testCases_Pointer);
		//log to page code
		if (window.ResultsTable == undefined) {
			console.log("Start Building Results Table");

			document.body.insertAdjacentHTML("beforeend","<button id='copy-button'>Copy CSV</button><button id='download-button'>Download CSV</button>")
			let button = document.getElementById("copy-button");
			button.onclick = clickButton;
			let downloadbutton = document.getElementById("download-button");
			downloadbutton.onclick = downloadFile;
			window.ResultsTable = "";

			console.log("Finished Building Results Table");
		}
		(function BuildResultsTable() {
			if (window.ResultsTable == null || window.ResultsTable == undefined) {
				console.error("Could not build results table");
				return;
			}
			window.ResultsTable = testCases;
			console.log("logtopage | Updated HTML");
		})();
	}
});