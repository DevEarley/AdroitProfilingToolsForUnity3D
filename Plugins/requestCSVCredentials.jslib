mergeInto(LibraryManager.library, {
	request_CSV_credentials_from_web: function () {
		console.log("FROM JSLib | request_CSV_credentials_from_web");
		window.adroit_send_CSV_credentials_to_Unity();
	}
});